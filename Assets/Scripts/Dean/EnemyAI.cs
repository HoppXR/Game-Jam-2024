using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float wanderRadius = 5f;
    public float wanderSpeed = 3f;
    public float chaseSpeed = 5f;
    public float attackRange = 1.5f;

    private Transform target;
    private UnityEngine.AI.NavMeshAgent agent;
    private bool isPlayerInRange = false;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform; // Assumes player tag is set to "Player"
        agent.speed = wanderSpeed;
        Wander();
    }

    void Update()
    {
        if (isPlayerInRange)
        {
            ChasePlayer();
            if (Vector3.Distance(transform.position, target.position) <= attackRange)
            {
                AttackPlayer();
            }
        }
        else
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                Wander();
            }
        }
    }

    void Wander()
    {
        Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
        agent.SetDestination(newPos);
    }

    void ChasePlayer()
    {
        agent.speed = chaseSpeed;
        agent.SetDestination(target.position);
    }

    void AttackPlayer()
    {
        // Implement attack logic here, for example:
        Debug.Log("Attacking player!");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * distance;

        randDirection += origin;

        UnityEngine.AI.NavMeshHit navHit;
        UnityEngine.AI.NavMesh.SamplePosition(randDirection, out navHit, distance, layermask);

        return navHit.position;
    }
}
