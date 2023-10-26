using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_WhiteLady_S_attackState : BaseState
{
    M_WhiteLady m_WhiteLady;

    public M_WhiteLady_S_attackState(StateMachine stateMachine, string animBoolName, EnemyEntity enemyEntity, M_WhiteLady m_WhiteLady)
    : base(animBoolName, stateMachine)
    {
        this.animBoolName = animBoolName;
        this.m_WhiteLady = m_WhiteLady;
    }

    #region override Methods

    public override void Enter(StateMachine stateMachine)
    {
        base.Enter(stateMachine);
        Debug.Log("Hello From Kapre attack state");
        m_WhiteLady.PlayAnim(animBoolName);
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
        m_WhiteLady.isInFront = m_WhiteLady.OnEnemyFrontCheck();
    }

    public override void OnTriggerEnter(StateMachine stateMachine, Collider collider)
    {
        base.OnTriggerEnter(stateMachine, collider);
        DamageListener(collider);
    }

    public override void OnTriggerExit(StateMachine stateMachine, Collider collider)
    {
        base.OnTriggerExit(stateMachine, collider);
        m_WhiteLady.enemyAttackFinished = true;
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

        if (!m_WhiteLady.enemyAttackFinished)
        {
            return;
        }

        if (collider.gameObject.CompareTag("Body"))
        {
            Debug.Log("Hit!!");
            tower.Damage(m_WhiteLady.enemiesData.dmgValue);
            Debug.Log(collider.name);
            m_WhiteLady.enemyAttackFinished = false;
        }
    }

    void ToDeathState()
    {
        if (m_WhiteLady.currentHealth <= 0f)
        {
            m_WhiteLady.stateMachine.ChangeState(m_WhiteLady.deathState);
        }
    }

    #region Checks

    private void CheckFront()
    {
        if (!m_WhiteLady.isInFront)
        {
            m_WhiteLady.stateMachine.ChangeState(m_WhiteLady.movingState);
        }
    }

    #endregion


    #endregion

}
