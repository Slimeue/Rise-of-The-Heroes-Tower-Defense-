using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Enemy1 : MeleeEnemyEntity
{


    public M_Enemy1_S_IdleState idleState { get; private set; }
    public M_Enemy1_S_MovingState movingState { get; private set; }
    public M_Enemy1_S_AttackState attackState { get; private set; }


    public override void Awake()
    {
        base.Awake();

        idleState = new M_Enemy1_S_IdleState(stateMachine, "Idle", this, this);
        movingState = new M_Enemy1_S_MovingState(stateMachine, "walk", this, this);
        attackState = new M_Enemy1_S_AttackState(stateMachine, "attack", this, this);

        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        stateMachine.Initialize(idleState);
    }

    private void OnTriggerEnter(Collider other)
    {
        baseState = stateMachine.currentState;
        baseState.OnTriggerEnter(stateMachine, other);
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
