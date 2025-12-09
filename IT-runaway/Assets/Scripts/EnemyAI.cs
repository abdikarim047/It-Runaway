using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;

    [Header("CHASE SETTINGS")]
    public float chaseRange = 10f;       // old distance trigger (kept for backup)
    public float stopChaseRange = 15f;

    [Header("SIGHT SETTINGS")]
    public float sightRange = 12f;       // how far the enemy can see
    public float fieldOfView = 90f;      // view cone angle
    public LayerMask obstructionMask;    // walls/obstacles

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
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }
    }

    void Update()
    {
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
        // distance check
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > sightRange) return false;

        // direction to player
        Vector3 dirToPlayer = (player.position - transform.position).normalized;

        // angle check (view cone)
        float angle = Vector3.Angle(transform.forward, dirToPlayer);
        if (angle > fieldOfView / 2f) return false;

        // raycast — check if there's a wall in the way
        if (Physics.Raycast(transform.position + Vector3.up, dirToPlayer, out RaycastHit hit, sightRange))
        {
            if (hit.transform == player)
            {
                return true; // clear line of sight
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
