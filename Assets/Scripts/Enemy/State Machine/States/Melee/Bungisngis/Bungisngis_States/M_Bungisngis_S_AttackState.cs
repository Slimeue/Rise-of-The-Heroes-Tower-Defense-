using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Bungisngis_S_AttackState : BaseState
{
    M_Bungisngis m_Bungisngis;

    public M_Bungisngis_S_AttackState(StateMachine stateMachine, string animBoolName, EnemyEntity enemyEntity, M_Bungisngis m_Bungisngis)
    : base(animBoolName, stateMachine)
    {
        this.animBoolName = animBoolName;
        this.m_Bungisngis = m_Bungisngis;
    }

    #region override methods

    public override void Enter(StateMachine stateMachine)
    {
        base.Enter(stateMachine);
        m_Bungisngis.PlayAnim(animBoolName);
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
        m_Bungisngis.isInFront = m_Bungisngis.OnEnemyFrontCheck();
    }

    public override void OnTriggerEnter(StateMachine stateMachine, Collider collider)
    {
        base.OnTriggerEnter(stateMachine, collider);
        DamageListener(collider);
    }

    public override void OnTriggerExit(StateMachine stateMachine, Collider collider)
    {
        base.OnTriggerExit(stateMachine, collider);
        m_Bungisngis.enemyAttackFinished = true;
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

        if (!m_Bungisngis.enemyAttackFinished)
        {
            return;
        }

        if (collider.gameObject.CompareTag("Body"))
        {
            tower.Damage(m_Bungisngis.enemiesData.dmgValue);
            m_Bungisngis.enemyAttackFinished = false;
        }
    }

    void ToDeathState()
    {
        if (m_Bungisngis.currentHealth <= 0f)
        {
            m_Bungisngis.stateMachine.ChangeState(m_Bungisngis.deathState);
        }
    }

    #region Checks

    private void CheckFront()
    {
        if (!m_Bungisngis.isInFront)
        {
            m_Bungisngis.stateMachine.ChangeState(m_Bungisngis.movingState);
        }
    }

    #endregion

    #endregion



}
