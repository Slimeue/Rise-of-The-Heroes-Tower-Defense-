using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Kapre_S_IdleState : BaseState
{

    float idleTime;

    M_Kapre m_Kapre;

    public M_Kapre_S_IdleState(StateMachine stateMachine, string animBoolName, EnemyEntity enemyEntity, M_Kapre m_Kapre)
    : base(animBoolName, stateMachine)
    {
        this.animBoolName = animBoolName;
        this.m_Kapre = m_Kapre;
    }

    #region override Methods
    public override void Enter(StateMachine stateMachine)
    {
        base.Enter(stateMachine);
        idleTime = Random.Range(1f, 5f);
        Debug.Log("Hello From Kapre idle state");
        m_Kapre.PlayAnim(animBoolName);
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
            m_Kapre.stateMachine.ChangeState(m_Kapre.movingState);
        }
    }

    private void AttackTransition()
    {
        if (!m_Kapre.isInFront)
        {
            return;
        }
        m_Kapre.stateMachine.ChangeState(m_Kapre.attackState);
    }

    void ToDeathState()
    {
        if (m_Kapre.currentHealth <= 0f)
        {
            m_Kapre.stateMachine.ChangeState(m_Kapre.deathState);
        }
    }


    #endregion



}
