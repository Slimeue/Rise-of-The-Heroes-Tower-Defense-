using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Tikbalang_S_attackState : BaseState
{
    M_Tikbalang m_Tikbalang;

    public M_Tikbalang_S_attackState(StateMachine stateMachine, string animBoolName, EnemyEntity enemyEntity, M_Tikbalang m_Tikbalang)
    : base(animBoolName, stateMachine)
    {
        this.animBoolName = animBoolName;
        this.m_Tikbalang = m_Tikbalang;
    }

    #region override Methods

    public override void Enter(StateMachine stateMachine)
    {
        base.Enter(stateMachine);
        Debug.Log("Hello From Kapre attack state");
        m_Tikbalang.PlayAnim(animBoolName);
    }

    public override void LogicUpdate(StateMachine stateMachine)
    {
        base.LogicUpdate(stateMachine);
        CheckFront();
        ToDeathState();
    }



    public override void DoChecks()
    {
        base.DoChecks();
        m_Tikbalang.isInFront = m_Tikbalang.OnEnemyFrontCheck();
    }

    public override void OnTriggerEnter(StateMachine stateMachine, Collider collider)
    {
        base.OnTriggerEnter(stateMachine, collider);
        DamageListener(collider);
    }

    public override void OnTriggerExit(StateMachine stateMachine, Collider collider)
    {
        base.OnTriggerExit(stateMachine, collider);
        m_Tikbalang.enemyAttackFinished = true;
    }



    public override void Exit(StateMachine stateMachine)
    {
        base.Exit(stateMachine);
    }


    #endregion


    #region METHODS

    private void DamageListener(Collider collider)
    {
        IDamageable tower = collider.GetComponentInParent<IDamageable>();

        if (!m_Tikbalang.enemyAttackFinished)
        {
            return;
        }

        if (collider.gameObject.CompareTag("Body"))
        {
            m_Tikbalang.soundsPlayTrack.Play("Hit");
            tower.Damage(m_Tikbalang.enemiesData.dmgValue);
            m_Tikbalang.enemyAttackFinished = false;
        }
    }

    void ToDeathState()
    {
        if (m_Tikbalang.currentHealth <= 0f)
        {
            m_Tikbalang.stateMachine.ChangeState(m_Tikbalang.deathState);
        }
    }

    #region Checks

    private void CheckFront()
    {
        if (!m_Tikbalang.isInFront)
        {
            m_Tikbalang.stateMachine.ChangeState(m_Tikbalang.movingState);
        }
    }

    #endregion


    #endregion

}
