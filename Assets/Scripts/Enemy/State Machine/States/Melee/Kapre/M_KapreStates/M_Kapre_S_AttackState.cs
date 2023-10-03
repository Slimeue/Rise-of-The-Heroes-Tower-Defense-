using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Kapre_S_AttackState : BaseState
{
    M_Kapre m_Kapre;

    public M_Kapre_S_AttackState(StateMachine stateMachine, string animBoolName, EnemyEntity enemyEntity, M_Kapre m_Kapre)
    : base(animBoolName, stateMachine)
    {
        this.animBoolName = animBoolName;
        this.m_Kapre = m_Kapre;
    }

    #region override Methods

    public override void Enter(StateMachine stateMachine)
    {
        base.Enter(stateMachine);
        Debug.Log("Hello From Kapre attack state");
        m_Kapre.PlayAnim(animBoolName);
    }

    public override void LogicUpdate(StateMachine stateMachine)
    {
        base.LogicUpdate(stateMachine);
        CheckFront();
    }



    public override void DoChecks()
    {
        base.DoChecks();
        m_Kapre.isInFront = m_Kapre.OnEnemyFrontCheck();
    }

    public override void OnTriggerEnter(StateMachine stateMachine, Collider collider)
    {
        base.OnTriggerEnter(stateMachine, collider);
        DamageListener(collider);
    }



    public override void Exit(StateMachine stateMachine)
    {
        base.Exit(stateMachine);
    }


    #endregion


    #region METHODS

    private void DamageListener(Collider collider)
    {
        IDamageable tower = collider.GetComponent<IDamageable>();

        if (tower != null)
        {
            Debug.Log("Hit!!");
            tower.Damage(m_Kapre.enemiesData.dmgValue);
        }
    }

    #region Checks

    private void CheckFront()
    {
        if (!m_Kapre.isInFront)
        {
            m_Kapre.stateMachine.ChangeState(m_Kapre.movingState);
        }
    }

    #endregion


    #endregion

}
