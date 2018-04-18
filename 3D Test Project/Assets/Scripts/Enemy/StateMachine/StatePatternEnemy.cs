using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]

public class StatePatternEnemy : MonoBehaviour {

    [HideInInspector] public IEnemyState currentState;
    [HideInInspector] public IEnemyState state_IDLE;
    [HideInInspector] public IEnemyState state_PATROL;
    [HideInInspector] public IEnemyState state_ENGAGE;
    [HideInInspector] public IEnemyState state_ALERT;
    [HideInInspector] public IEnemyState state_INVESTIGATE;


    [HideInInspector] public int numberOfStates;

    [SerializeField] private bool Idle;
    [SerializeField] private bool Patrol;
    [SerializeField] private bool Engage;
    [SerializeField] private bool Alert;
    [SerializeField] private bool Investigate;

    public MeshRenderer meshRendererFlag;
    public NavMeshAgent navMeshAgent;

    [HideInInspector] public Transform[] targets;
    [HideInInspector] public Transform target;

    public Transform visionRendererFlag;

    public Vector3[] localWaypoints;

    public float feildOfViewAngle;
    public float viewRange;

    public float searchingTurnSpeed = 0.5f;
    public float seachingDuration = 2f;
    public float soundRange = 10f;

    void Awake() {

        navMeshAgent = GetComponent<NavMeshAgent>();

        if (Idle == true) {
            state_IDLE = new IdleState();
            state_IDLE.CreateState(this);
        }

        if (Patrol == true) {
            state_PATROL = new PatrolState();
            state_PATROL.CreateState(this);
        }

        if (Engage == true)
        {
            state_ENGAGE = new EngageState();
            state_ENGAGE.CreateState(this);
        }

        if (Alert == true)
        {
            state_ALERT = new AlertState();
            state_ALERT.CreateState(this);
        }

        if (Investigate == true)
        {
            state_INVESTIGATE = new InvestigateState();
            state_INVESTIGATE.CreateState(this);
        }
    }

    // Use this for initialization
    void Start () {

        if (state_IDLE == null) {
            Debug.Log("Setup IDLE state");
        }
        else {
            currentState = state_IDLE;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (currentState != null)
            currentState.UpdateState();
	}

    private void OnTriggerEnter(Collider collider) {
        if (currentState != null)
            currentState.OnTriggerEnter(collider);
    }

    private void OnDrawGizmos()
    {
        if (currentState != null)
            currentState.DrawGizmos();
    }
}
