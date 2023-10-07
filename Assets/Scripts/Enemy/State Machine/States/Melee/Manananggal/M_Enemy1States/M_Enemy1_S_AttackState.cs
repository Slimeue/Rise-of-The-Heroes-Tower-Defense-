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


    #region override Methods
    public override void Enter(StateMachine stateMachine)
    {
        base.Enter(stateMachine);
        Debug.Log("Hello From Mananaggal AttackState");
        m_Enemy1SM.PlayAnim(animBoolName);

    }

    public override void DoChecks()
    {
        base.DoChecks();
        m_Enemy1SM.isInFront = m_Enemy1SM.OnEnemyFrontCheck();

    }


    public override void Exit(StateMachine stateMachine)
    {
        base.Exit(stateMachine);
    }


    public override void LogicUpdate(StateMachine stateMachine)
    {
        base.LogicUpdate(stateMachine);
        CheckFront(stateMachine);
        ToDeathState();
    }

    public override void OnTriggerEnter(StateMachine stateMachine, Collider collider)
    {
        base.OnTriggerEnter(stateMachine, collider);
    }

    public override void OnTriggerExit(StateMachine stateMachine, Collider collider)
    {
        base.OnTriggerExit(stateMachine, collider);
        DamageListener(collider);
    }

    #endregion

    #region METHODS

    void ToDeathState()
    {
        if (m_Enemy1SM.currentHealth <= 0f)
        {
            m_Enemy1SM.stateMachine.ChangeState(m_Enemy1SM.deathState);
        }
    }

    private void DamageListener(Collider collider)
    {
        IDamageable tower = collider.GetComponentInParent<IDamageable>();
        if (collider.gameObject.CompareTag("Body"))
        {
            tower.Damage(m_Enemy1SM.enemiesData.dmgValue);
        }
    }

    #region Checks
    private void CheckFront(StateMachine stateMachine)
    {
        if (!m_Enemy1SM.isInFront)
        {
            stateMachine.ChangeState(m_Enemy1SM.movingState);
        }
    }
    #endregion

    #endregion

}
