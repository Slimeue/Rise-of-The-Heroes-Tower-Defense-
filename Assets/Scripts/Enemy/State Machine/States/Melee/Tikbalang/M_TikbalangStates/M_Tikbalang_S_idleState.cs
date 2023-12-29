using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Tikbalang_S_idleState : BaseState
{
    M_Tikbalang m_Tikbalang;
    float idleTime;
    public M_Tikbalang_S_idleState(StateMachine stateMachine, string animBoolName, EnemyEntity enemyEntity, M_Tikbalang m_Tikbalang)
    : base(animBoolName, stateMachine)
    {
        this.animBoolName = animBoolName;
        this.m_Tikbalang = m_Tikbalang;
    }

    #region override Methods
    public override void Enter(StateMachine stateMachine)
    {
        base.Enter(stateMachine);
        idleTime = Random.Range(1f, 3f);
        m_Tikbalang.PlayAnim(animBoolName);
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
            m_Tikbalang.stateMachine.ChangeState(m_Tikbalang.movingState);
        }
    }

    private void AttackTransition()
    {
        if (!m_Tikbalang.isInFront)
        {
            return;
        }
        m_Tikbalang.stateMachine.ChangeState(m_Tikbalang.attackState);
    }

    void ToDeathState()
    {
        if (m_Tikbalang.currentHealth <= 0f)
        {
            m_Tikbalang.stateMachine.ChangeState(m_Tikbalang.deathState);
        }
    }


    #endregion

}
