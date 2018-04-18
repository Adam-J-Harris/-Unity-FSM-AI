using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestigateState : IEnemyState {

    private StatePatternEnemy enemy;

    private float searchTimer;

    private Vector3 playersLastKnownPos = new Vector3(0, 0, 0);

    private bool playerIsTagert;
    private bool gotLastKnownPos;

    public InvestigateState()
    {

    }

    public void CreateState(StatePatternEnemy pEnemy)
    {
        enemy = pEnemy;
        enemy.numberOfStates++;

        Debug.Log("Idle State Created " + enemy.numberOfStates);
    }

    public void UpdateState()
    {
        enemy.meshRendererFlag.material.color = Color.magenta;

        Look();

        if (!gotLastKnownPos)
        {
            GetLastPosition();
        }
        else
        {
            Search();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            CheckInvestigationData();
        }

        // IF enemy hit chanage state;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            enemy.currentState = enemy.state_ALERT;
        }
    }

    void CheckInvestigationData()
    {
        float DistanceToPlayer = Vector3.Distance(enemy.transform.position, GameObject.Find("Player").transform.position);

        if (DistanceToPlayer < (enemy.soundRange + enemy.viewRange))
        {
            gotLastKnownPos = false;
            playerIsTagert = false;

            searchTimer = 0f;
        }
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

                gotLastKnownPos = false;
                playerIsTagert = false;

                Reset();

                enemy.currentState = enemy.state_ENGAGE;
            }
        }
    }

    private void GetLastPosition()
    {
        if (!playerIsTagert)
        {
            playersLastKnownPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            enemy.navMeshAgent.updateRotation = true;
            playerIsTagert = true;
        }
        else
        {
            enemy.navMeshAgent.destination = playersLastKnownPos;
            enemy.navMeshAgent.isStopped = false;

            if (enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance)
            {
                enemy.navMeshAgent.isStopped = true;
                gotLastKnownPos = true;
            }
        }
    }

    private void Search()
    {
        enemy.navMeshAgent.isStopped = true;
        enemy.transform.Rotate(0, enemy.searchingTurnSpeed * Time.deltaTime, 0);

        searchTimer += Time.deltaTime;

        if (searchTimer >= enemy.seachingDuration)
        {
            gotLastKnownPos = false;
            playerIsTagert = false;

            Reset();

            enemy.currentState = enemy.state_PATROL;
        }
    }

    private void Reset()
    {
        enemy.navMeshAgent.updatePosition = true;
        enemy.navMeshAgent.updateRotation = true;
    }

    void IEnemyState.DrawGizmos()
    {

    }
}
