using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySM : EnemyEntity
{


    [HideInInspector]
    public S_Idle idleState;
    [HideInInspector]
    public S_Moving movingState;
    [HideInInspector]
    public S_Attack attackState;


    public Transform target;

    BaseState baseState;

    Vector3 point;
    #region MOVEMENT
    public float speed = 10f;
    public int pointIndex = 0;
    public float rotationSpeed = 10f;
    #endregion

    #region Transform

    //public GameObject body;

    #endregion

    #region Checks
    public bool isInFront;


    #endregion

    [SerializeField] BoxCollider attackCollider;


    float detectionRadius = 2f;

    public override void Awake()
    {
        base.Awake();
        target = Waypoints.points[0];
        idleState = new S_Idle(this, "Idle", stateMachine);
        movingState = new S_Moving(this, "walk", stateMachine);
        attackState = new S_Attack(this, "attack", stateMachine);



        anim = GetComponent<Animator>();

    }

    // protected override BaseState GetInitialState()
    // {
    //     return idleState;
    // }

    private void OnTriggerEnter(Collider other)
    {
        //baseState = currentState;
        baseState.OnTriggerEnter(stateMachine, other);
    }

    public bool OnEnemyFrontCheck()
    {
        return Physics.Raycast(transform.position, transform.forward, enemiesData.attackRange, enemiesData.whatIsTower);
    }

    #region 
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector3 dir = transform.forward * enemiesData.attackRange;
        Gizmos.DrawRay(transform.position, dir);
    }
    #endregion




}
