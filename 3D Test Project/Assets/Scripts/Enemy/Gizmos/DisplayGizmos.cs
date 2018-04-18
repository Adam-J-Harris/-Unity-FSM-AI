using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayGizmos : MonoBehaviour {

    public StatePatternEnemy SPM;

    void Awake() {

    }

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

    private void OnDrawGizmos()
    {
        if (SPM.localWaypoints != null)
        {
            Gizmos.color = Color.green;

            Vector3 waypointSize = new Vector3(0.1f, 0.1f, 0.1f);

            for (int i = 0; i < SPM.localWaypoints.Length; i++)
            {
                if (Application.isPlaying == false)
                    Gizmos.DrawWireCube(SPM.localWaypoints[i] + SPM.transform.position , waypointSize);
            }
        }
    }
}
