using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Bungisngis_S_IdleState : BaseState
{
    M_Bungisngis m_Bungisngis;

    float idleTime;

    public M_Bungisngis_S_IdleState(StateMachine stateMachine, string animBoolName, EnemyEntity enemyEntity, M_Bungisngis m_Bungisngis)
    : base(animBoolName, stateMachine)
    {
        this.animBoolName = animBoolName;
        this.m_Bungisngis = m_Bungisngis;
    }

    #region override Methods
    public override void Enter(StateMachine stateMachine)
    {
        base.Enter(stateMachine);
        idleTime = Random.Range(1f, 5f);
        m_Bungisngis.PlayAnim(animBoolName);
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
            m_Bungisngis.stateMachine.ChangeState(m_Bungisngis.movingState);
        }
    }

    private void AttackTransition()
    {
        if (!m_Bungisngis.isInFront)
        {
            return;
        }
        m_Bungisngis.stateMachine.ChangeState(m_Bungisngis.attackState);
    }

    void ToDeathState()
    {
        if (m_Bungisngis.currentHealth <= 0f)
        {
            m_Bungisngis.stateMachine.ChangeState(m_Bungisngis.deathState);
        }
    }


    #endregion

}
