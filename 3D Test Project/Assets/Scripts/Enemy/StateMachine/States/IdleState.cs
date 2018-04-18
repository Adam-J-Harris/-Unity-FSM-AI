using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class IdleState : IEnemyState {

    private StatePatternEnemy enemy;

    public IdleState() {

    }

    public void CreateState(StatePatternEnemy pEnemy)
    {
        enemy = pEnemy;
        enemy.numberOfStates++;

        Debug.Log("Idle State Created " + enemy.numberOfStates);
    }

    public void UpdateState()
    {
        enemy.meshRendererFlag.material.color = Color.gray;

        if(enemy.state_PATROL != null) // ADD to others
        enemy.currentState = enemy.state_PATROL;
    }

    public void OnTriggerEnter(Collider other)
    {

    }

    void IEnemyState.DrawGizmos()
    {

    }
}
