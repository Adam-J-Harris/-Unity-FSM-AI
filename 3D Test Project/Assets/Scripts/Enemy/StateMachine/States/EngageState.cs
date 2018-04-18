using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngageState : IEnemyState {

    private StatePatternEnemy enemy;

    private float searchLimit = 10f;
    private float searchTimer = 0f;

    private bool foundCover, playerInSight;

    public EngageState()
    {

    }

    void IEnemyState.CreateState(StatePatternEnemy pEnemy)
    {
        enemy = pEnemy;

        enemy.numberOfStates++;

        Debug.Log("Engage State Created " + enemy.numberOfStates);
    }

    void IEnemyState.DrawGizmos()
    {

    }

    void IEnemyState.OnTriggerEnter(Collider other)
    {

    }

    void IEnemyState.UpdateState()
    {
        enemy.meshRendererFlag.material.color = Color.red;

        searchTimer += Time.deltaTime;

        EngageMethod();

        Look();

        Engaging();

        RotateTowards();
    }

    void EngageMethod()
    {
        FindCover();

        if (searchTimer >= searchLimit)
        {
            Reset();         

            enemy.currentState = enemy.state_ALERT;
        }
    }

    void FindCover()
    {
        //Transform[] randomPos = 
    }

    public void Engaging()
    {
        enemy.navMeshAgent.stoppingDistance = 4f;
        enemy.navMeshAgent.destination = enemy.target.position;

        enemy.navMeshAgent.updateRotation = false;
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

                searchTimer = 0f;
            }
        }
    }

    private void Reset()
    {
        enemy.navMeshAgent.updateRotation = true;

        enemy.navMeshAgent.isStopped = true;

        searchLimit = 10f;
    }

    private void RotateTowards()
    {
        Vector3 direction = (enemy.target.transform.position - enemy.transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));

        enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, lookRotation, Time.deltaTime * 10f);
    }
}
