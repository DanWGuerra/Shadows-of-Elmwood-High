using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public float moveRadius = 10f;
    public float detectionRadius = 15f;
    public float fieldOfViewAngle = 120f;
    public float searchTime = 3f; // Time to wait at last known position
    public LayerMask obstacleLayer;

    private Vector3 lastKnownPosition;
    private bool isChasing = false;
    private bool isSearching = false;
    private bool isWaiting = false;

    private void Start()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
        MoveToRandomPoint();
    }

    private void Update()
    {
        if (CanSeePlayer())
        {
            isChasing = true;
            isSearching = false;
            isWaiting = false;
            StopAllCoroutines(); // Stop any waiting coroutine
            lastKnownPosition = player.position;
            agent.SetDestination(player.position);
        }
        else if (isChasing)
        {
            isChasing = false;
            isSearching = true;
            agent.SetDestination(lastKnownPosition);
        }
        else if (isSearching && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && !isWaiting)
        {
            // AI reaches last known position, wait before patrolling
            isSearching = false;
            isWaiting = true;
            StartCoroutine(WaitBeforePatrolling());
        }
        else if (!isChasing && !isSearching && !isWaiting && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            MoveToRandomPoint();
        }

        if (isChasing)
        {
            agent.SetDestination(player.position);
        }
    }

    private IEnumerator WaitBeforePatrolling()
    {
        yield return new WaitForSeconds(searchTime);
        isWaiting = false;
        MoveToRandomPoint();
    }

    private void MoveToRandomPoint()
    {
        Vector3 randomPoint = Random.insideUnitSphere * moveRadius;
        randomPoint += transform.position;

        if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, moveRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    private bool CanSeePlayer()
    {
        if (player == null) return false;

        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > detectionRadius) return false;

        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
        if (angleToPlayer > fieldOfViewAngle / 2) return false;

        if (Physics.Raycast(transform.position + Vector3.up, directionToPlayer, distanceToPlayer, obstacleLayer))
        {
            return false;
        }

        return true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, moveRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        // Draw the Field of View (FOV)
        Gizmos.color = Color.blue;
        Vector3 forward = transform.forward * detectionRadius;
        Vector3 leftLimit = Quaternion.Euler(0, -fieldOfViewAngle / 2, 0) * forward;
        Vector3 rightLimit = Quaternion.Euler(0, fieldOfViewAngle / 2, 0) * forward;

        Gizmos.DrawLine(transform.position, transform.position + leftLimit);
        Gizmos.DrawLine(transform.position, transform.position + rightLimit);
    }
}
