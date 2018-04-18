using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class AlertState : IEnemyState {

    private StatePatternEnemy enemy;

    private float searchTimer = 0f;

    public AlertState()
    {

    }

    void IEnemyState.CreateState(StatePatternEnemy pEnemy)
    {
        enemy = pEnemy;
        enemy.numberOfStates++;

        Debug.Log("Idle State Created " + enemy.numberOfStates);
    }

    public void UpdateState()
    {
        enemy.meshRendererFlag.material.color = Color.yellow;

        Look();

        Search();
    }

    public void OnTriggerEnter(Collider other)
    {

    }

    private void Look()
    {
        float feildOfViewAngle = 110f;

        Vector3 direction = GameObject.FindGameObjectWithTag("Player").transform.position - enemy.transform.position;
        float angle = Vector3.Angle(direction, enemy.transform.forward);

        if (angle < feildOfViewAngle * 0.5f)
        {
            RaycastHit hit;

            if (Physics.Raycast(enemy.visionRendererFlag.transform.position, direction.normalized, out hit, enemy.viewRange) && hit.collider.CompareTag("Player"))
            {
                enemy.target = hit.transform;

                enemy.currentState = enemy.state_ENGAGE;

                Debug.Log("log 1");
            }
        }
    }

    private void Search()
    {
        enemy.navMeshAgent.isStopped = true;
        enemy.navMeshAgent.updateRotation = false;
        enemy.transform.Rotate(0, enemy.searchingTurnSpeed * Time.deltaTime, 0);

        searchTimer += Time.deltaTime;

        if (searchTimer >= enemy.seachingDuration)
        {
            Debug.Log("log 3");

            enemy.navMeshAgent.updateRotation = true;

            enemy.currentState = enemy.state_INVESTIGATE;
        }
    }

    void IEnemyState.DrawGizmos()
    {

    }
}
