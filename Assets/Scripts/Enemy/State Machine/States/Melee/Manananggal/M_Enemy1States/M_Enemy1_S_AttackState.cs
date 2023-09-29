using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Enemy1_S_AttackState : BaseState
{
    M_Enemy1 m_Enemy1SM;

    public M_Enemy1_S_AttackState(StateMachine stateMachine, string animBoolName, EnemyEntity enemyEntity, M_Enemy1 m_Enemy1SM)
       : base(animBoolName, stateMachine)
    {
        this.m_Enemy1SM = m_Enemy1SM;
        this.animBoolName = animBoolName;
    }


    public override void Enter(StateMachine stateMachine)
    {
        base.Enter(stateMachine);
        Debug.Log("Hello From AttackState");
    }

    public override void DoChecks()
    {
        base.DoChecks();
        m_Enemy1SM.isInFront = m_Enemy1SM.OnEnemyFrontCheck();

    }


    public override void Exit(StateMachine stateMachine)
    {
        base.Exit(stateMachine);
        m_Enemy1SM.anim.SetBool(animBoolName, false);
    }


    public override void LogicUpdate(StateMachine stateMachine)
    {
        base.LogicUpdate(stateMachine);
        m_Enemy1SM.anim.SetBool(animBoolName, true);
        if (!m_Enemy1SM.isInFront)
        {
            stateMachine.ChangeState(m_Enemy1SM.movingState);
        }
    }

    public override void OnTriggerEnter(StateMachine stateMachine, Collider collider)
    {
        base.OnTriggerEnter(stateMachine, collider);
        IDamageable tower = collider.GetComponent<IDamageable>();

        if (tower != null)
        {
            Debug.Log("Hit!!");
            tower.Damage(m_Enemy1SM.enemiesData.dmgValue);
        }
    }
}
