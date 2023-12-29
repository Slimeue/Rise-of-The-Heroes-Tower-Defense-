using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_WhiteLady_S_idleState : BaseState
{
    M_WhiteLady m_WhiteLady;
    float idleTime;
    public M_WhiteLady_S_idleState(StateMachine stateMachine, string animBoolName, EnemyEntity enemyEntity, M_WhiteLady m_WhiteLady)
    : base(animBoolName, stateMachine)
    {
        this.animBoolName = animBoolName;
        this.m_WhiteLady = m_WhiteLady;
    }

    #region override Methods
    public override void Enter(StateMachine stateMachine)
    {
        base.Enter(stateMachine);
        idleTime = Random.Range(1f, 3f);
        m_WhiteLady.PlayAnim(animBoolName);
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
            m_WhiteLady.stateMachine.ChangeState(m_WhiteLady.movingState);
        }
    }

    private void AttackTransition()
    {
        if (!m_WhiteLady.isInFront)
        {
            return;
        }
        m_WhiteLady.stateMachine.ChangeState(m_WhiteLady.attackState);
    }

    void ToDeathState()
    {
        if (m_WhiteLady.currentHealth <= 0f)
        {
            m_WhiteLady.stateMachine.ChangeState(m_WhiteLady.deathState);
        }
    }


    #endregion

}
