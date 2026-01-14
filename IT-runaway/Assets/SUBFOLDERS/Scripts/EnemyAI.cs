using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;

    [Header("CHASE SETTINGS")]
    public float chaseRange = 10f;
    public float stopChaseRange = 15f;

    [Header("SIGHT SETTINGS")]
    public float sightRange = 12f;
    public float fieldOfView = 90f;
    public LayerMask obstructionMask;

    [Header("ROAM SETTINGS")]
    public float roamRadius = 10f;
    public float roamInterval = 3f;

    private float roamTimer;

    private enum State { Roaming, Chasing }
    private State currentState = State.Roaming;

    void Start()
    {
        roamTimer = roamInterval;

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }

        if (agent == null)
            agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (player == null || agent == null) return;

        // speler niet op NavMesh → altijd roamen
        if (!IsPlayerOnNavMesh())
        {
            currentState = State.Roaming;
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        bool canSeePlayer = CanSeePlayer();

        switch (currentState)
        {
            case State.Roaming:
                RoamLogic();

                if (canSeePlayer || distanceToPlayer <= chaseRange)
                {
                    currentState = State.Chasing;
                }
                break;

            case State.Chasing:
                if (!canSeePlayer && distanceToPlayer >= stopChaseRange)
                {
                    currentState = State.Roaming;
                    break;
                }

                ChaseLogic();
                break;
        }
    }

    bool CanSeePlayer()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > sightRange) return false;

        Vector3 dirToPlayer = (player.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, dirToPlayer);
        if (angle > fieldOfView / 2f) return false;

        if (Physics.Raycast(transform.position + Vector3.up, dirToPlayer, out RaycastHit hit, sightRange))
        {
            if (hit.transform == player)
                return true;
        }

        return false;
    }

    bool IsPlayerOnNavMesh()
{
    if (player == null) return false;

    // Raycast naar beneden om de grond onder de speler te vinden
    if (Physics.Raycast(player.position, Vector3.down, out RaycastHit hit, 5f))
    {
        NavMeshHit navHit;
        return NavMesh.SamplePosition(hit.point, out navHit, 0.2f, NavMesh.AllAreas);
    }

    return false;
}


    void RoamLogic()
    {
        roamTimer -= Time.deltaTime;

        if (roamTimer <= 0f && agent.isOnNavMesh)
        {
            Vector3 newPos = RandomNavmeshLocation(roamRadius);
            agent.SetDestination(newPos);
            roamTimer = roamInterval;
        }
    }

    void ChaseLogic()
    {
        if (!IsPlayerOnNavMesh()) return;

        if (agent.isOnNavMesh)
            agent.SetDestination(player.position);
    }

    Vector3 RandomNavmeshLocation(float radius)
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 randomDirection = Random.insideUnitSphere * radius + transform.position;

            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, radius, NavMesh.AllAreas))
                return hit.position;
        }

        return transform.position;
    }
}
