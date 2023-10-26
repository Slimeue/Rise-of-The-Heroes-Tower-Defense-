using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Berberoka_S_AttackState : BaseState
{
    M_Berberoka m_Berberoka;

    public M_Berberoka_S_AttackState(StateMachine stateMachine, string animBoolName, EnemyEntity enemyEntity, M_Berberoka m_Berberoka)
    : base(animBoolName, stateMachine)
    {
        this.animBoolName = animBoolName;
        this.m_Berberoka = m_Berberoka;
    }

    #region override methods

    public override void Enter(StateMachine stateMachine)
    {
        base.Enter(stateMachine);
        m_Berberoka.PlayAnim(animBoolName);
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
        m_Berberoka.isInFront = m_Berberoka.OnEnemyFrontCheck();
    }

    public override void OnTriggerEnter(StateMachine stateMachine, Collider collider)
    {
        base.OnTriggerEnter(stateMachine, collider);
        DamageListener(collider);
    }

    public override void OnTriggerExit(StateMachine stateMachine, Collider collider)
    {
        base.OnTriggerExit(stateMachine, collider);
        m_Berberoka.enemyAttackFinished = true;
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

        if (!m_Berberoka.enemyAttackFinished)
        {
            return;
        }

        if (collider.gameObject.CompareTag("Body"))
        {
            tower.Damage(m_Berberoka.enemiesData.dmgValue);
            m_Berberoka.enemyAttackFinished = false;
        }
    }

    void ToDeathState()
    {
        if (m_Berberoka.currentHealth <= 0f)
        {
            m_Berberoka.stateMachine.ChangeState(m_Berberoka.deathState);
        }
    }

    #region Checks

    private void CheckFront()
    {
        if (!m_Berberoka.isInFront)
        {
            m_Berberoka.stateMachine.ChangeState(m_Berberoka.movingState);
        }
    }

    #endregion

    #endregion

}
