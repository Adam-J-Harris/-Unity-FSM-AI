using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState  {

    void CreateState(StatePatternEnemy pEnemy);

    void UpdateState();

    void OnTriggerEnter(Collider other);

    void DrawGizmos();
}
