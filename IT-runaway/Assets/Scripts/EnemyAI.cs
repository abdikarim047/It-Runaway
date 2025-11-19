using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;

    [Header("CHASE SETTINGS")]
    public float chaseRange = 10f;
    public float stopChaseRange = 15f;

    [Header("ROAM SETTINGS")]
    public float roamRadius = 10f;
    public float roamInterval = 3f;

    private float roamTimer;
    private enum State { Roaming, Chasing }
    private State currentState = State.Roaming;

    void Start()
    {
        roamTimer = roamInterval;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // 🔥 Check of player op NavMesh staat
        bool playerOnNavmesh = NavMesh.SamplePosition(player.position, out _, 1f, NavMesh.AllAreas);

        switch (currentState)
        {
            case State.Roaming:
                RoamLogic();

                // Begin pas met chasen ALS player op navmesh staat
                if (distanceToPlayer <= chaseRange && playerOnNavmesh)
                {
                    currentState = State.Chasing;
                }
                break;

            case State.Chasing:
                // Stop chasen als player te ver is OF niet op navmesh staat
                if (distanceToPlayer >= stopChaseRange || !playerOnNavmesh)
                {
                    currentState = State.Roaming;
                    break;
                }

                ChaseLogic();
                break;
        }
    }

    void RoamLogic()
    {
        roamTimer -= Time.deltaTime;

        if (roamTimer <= 0f)
        {
            Vector3 newPos = RandomNavmeshLocation(roamRadius);
            agent.SetDestination(newPos);

            roamTimer = roamInterval;
        }
    }

    void ChaseLogic()
    {
        agent.SetDestination(player.position);
    }

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, radius, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return transform.position;
    }
}
