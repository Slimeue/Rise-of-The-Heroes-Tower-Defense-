using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Berberoka_S_IdleState : BaseState
{

    float idleTime;

    M_Berberoka m_Berberoka;

    public M_Berberoka_S_IdleState(StateMachine stateMachine, string animBoolName, EnemyEntity enemyEntity, M_Berberoka m_Berberoka)
    : base(animBoolName, stateMachine)
    {
        this.animBoolName = animBoolName;
        this.m_Berberoka = m_Berberoka;
    }

    #region override Methods
    public override void Enter(StateMachine stateMachine)
    {
        base.Enter(stateMachine);
        idleTime = Random.Range(1f, 5f);
        m_Berberoka.PlayAnim(animBoolName);
    }

    public override void LogicUpdate(StateMachine stateMachine)
    {
        base.LogicUpdate(stateMachine);
        // m_Kapre.anim.Play(animBoolName);
        StopIdle();
        AttackTransition();
        ToDeathState();

    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void OnTriggerEnter(StateMachine stateMachine, Collider collider)
    {
        base.OnTriggerEnter(stateMachine, collider);
    }

    public override void Exit(StateMachine stateMachine)
    {
        base.Exit(stateMachine);
    }

    #endregion



    #region METHODS

    private void StopIdle()
    {
        if (idleTime > 0)
        {
            idleTime -= Time.deltaTime;
        }
        else
        {
            m_Berberoka.stateMachine.ChangeState(m_Berberoka.movingState);
        }
    }

    private void AttackTransition()
    {
        if (!m_Berberoka.isInFront)
        {
            return;
        }
        m_Berberoka.stateMachine.ChangeState(m_Berberoka.attackState);
    }

    void ToDeathState()
    {
        if (m_Berberoka.currentHealth <= 0f)
        {
            m_Berberoka.stateMachine.ChangeState(m_Berberoka.deathState);
        }
    }


    #endregion


}
