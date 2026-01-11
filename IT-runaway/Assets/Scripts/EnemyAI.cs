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

        // auto-find player if not assigned
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
            else
                Debug.LogWarning("EnemyAI: No player found in scene!");
        }

        // check if agent is assigned
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
            if (agent == null)
                Debug.LogError("EnemyAI: No NavMeshAgent found on enemy!");
        }
    }

    void Update()
    {
        // safety check: stop if no player
        if (player == null || agent == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        bool canSeePlayer = CanSeePlayer();

        // check if player is on NavMesh
        bool playerOnNavMesh = NavMesh.SamplePosition(player.position, out _, 1.2f, NavMesh.AllAreas);

        switch (currentState)
        {
            case State.Roaming:
                RoamLogic();

                // switch to chase only if player is reachable
                if ((canSeePlayer || distanceToPlayer <= chaseRange) && playerOnNavMesh)
                {
                    currentState = State.Chasing;
                }
                break;

            case State.Chasing:
                // stop chasing if player out of range or not on NavMesh
                if ((!canSeePlayer && distanceToPlayer >= stopChaseRange) || !playerOnNavMesh)
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
        if (player == null) return false;

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > sightRange) return false;

        Vector3 dirToPlayer = (player.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, dirToPlayer);
        if (angle > fieldOfView / 2f) return false;

        if (Physics.Raycast(transform.position + Vector3.up, dirToPlayer, out RaycastHit hit, sightRange, ~obstructionMask))
        {
            if (hit.transform == player)
            {
                return true;
            }
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
        if (agent.isOnNavMesh)
        {
            agent.SetDestination(player.position);
        }
    }

    public Vector3 RandomNavmeshLocation(float radius)
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 randomDirection = Random.insideUnitSphere * radius + transform.position;

            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, radius, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }

        return transform.position;
    }
}
