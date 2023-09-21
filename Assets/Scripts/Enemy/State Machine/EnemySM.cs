using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySM : StateMachine
{


    [HideInInspector]
    public S_Idle idleState;
    [HideInInspector]
    public S_Moving movingState;
    [HideInInspector]
    public Char1_S_Attack attackState;

    public EnemiesData enemiesData;
    public Transform target;

    BaseState baseState;

    Vector3 point;
    #region MOVEMENT
    public float speed = 10f;
    public int pointIndex = 0;
    public float rotationSpeed = 10f;
    #endregion

    #region Transform

    public GameObject body;
    public Animator anim;

    #endregion

    #region Checks
    public bool isInFront;


    #endregion

    [SerializeField] BoxCollider attackCollider;


    float detectionRadius = 2f;

    public void Awake()
    {

        target = Waypoints.points[0];
        idleState = new S_Idle(this, "Idle");
        movingState = new S_Moving(this, "walk");
        attackState = new Char1_S_Attack(this, "attack");



        anim = GetComponent<Animator>();

    }

    protected override BaseState GetInitialState()
    {
        return idleState;
    }

    private void OnTriggerEnter(Collider other)
    {
        baseState = currentState;
        baseState.OnTriggerEnter(this, other);
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
