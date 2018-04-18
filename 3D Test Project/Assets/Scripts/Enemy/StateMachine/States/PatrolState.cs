using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class PatrolState : IEnemyState {

    private StatePatternEnemy enemy;

    private Vector3[] globalWayPoints;

    public float speed;
    public float waitTime;
    public float nextMoveTime;
    public float percentageBetweenWaypoints;
    public float easeAmount;

    public int fromWaypointIndex;
    private int nextWayPoint;

    public bool cyclicArray;

    public PatrolState()
    {
        speed = 2.0f;
        waitTime = 1.0f;
        percentageBetweenWaypoints = 0.0f;

        fromWaypointIndex = 0;
    }

    void IEnemyState.CreateState(StatePatternEnemy pEnemy) {

        enemy = pEnemy;
        enemy.numberOfStates++;

        Debug.Log("Patorl State Created " + enemy.numberOfStates);

        globalWayPoints = new Vector3[enemy.localWaypoints.Length];

        for (int i = 0; i < enemy.localWaypoints.Length; i++)
        {
            globalWayPoints[i] = enemy.localWaypoints[i] + enemy.transform.position;
        }

        cyclicArray = true;
    }

    void IEnemyState.UpdateState() {

        enemy.meshRendererFlag.material.color = Color.green;

        Patrol();

        Look();
    }

    void IEnemyState.OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag("Player"))
        {
            enemy.currentState = enemy.state_ALERT;
        }
    }

    void Patrol()
    {
        enemy.navMeshAgent.destination = globalWayPoints[nextWayPoint];

        enemy.navMeshAgent.updateRotation = true;

        enemy.navMeshAgent.isStopped = false;

        if (enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance && !enemy.navMeshAgent.pathPending)
        {
            nextWayPoint = (nextWayPoint + 1) % globalWayPoints.Length;
        }
    }

    private void Look()
    {
        enemy.feildOfViewAngle = 110f;

        Vector3 direction = GameObject.FindGameObjectWithTag("Player").transform.position - enemy.transform.position;

        float angle = Vector3.Angle(direction, enemy.transform.forward);

        if (angle < enemy.feildOfViewAngle * 0.5f)
        {
            RaycastHit hit;

            if (Physics.Raycast(enemy.visionRendererFlag.transform.position, direction.normalized, out hit, enemy.viewRange) && hit.collider.CompareTag("Player"))
            {
                enemy.target = hit.transform;

                enemy.currentState = enemy.state_ENGAGE;
            }
        }
    }

    void IEnemyState.DrawGizmos()
    {
        if (enemy.localWaypoints != null && Application.isPlaying)
        {
            Gizmos.color = Color.green;

            Vector3 waypointSize = new Vector3(0.1f, 0.1f, 0.1f);

            for (int i = 0; i < enemy.localWaypoints.Length; i++)
            {
                Vector3 globalWaypointPos = (Application.isPlaying) ? globalWayPoints[i] : enemy.localWaypoints[i] + enemy.transform.position;

                Gizmos.DrawWireCube(globalWaypointPos, waypointSize);
            }
        }
    }
}
