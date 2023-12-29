using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_DelPIlar_S_idleState : CharacterBaseState
{
    DelPilar m_DelPilar;
    float healInterval = 5f;
    float timeLastHeal = 0f;
    float healPercentageAmount = 5f;

    public M_DelPIlar_S_idleState(CharacterStateMachine characterStateMachine, string animBoolName, CharEntity charEntity, DelPilar m_DelPilar)
    : base(animBoolName, characterStateMachine)
    {
        this.animBoolName = animBoolName;
        this.m_DelPilar = m_DelPilar;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        ToDeathState();
        PassiveHeal();
        m_DelPilar.PlayAnim(animBoolName);

    }

    public override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);
    }

    public override void OnTriggerExit(Collider collider)
    {
        base.OnTriggerExit(collider);
    }

    public override void DoChecks()
    {
        base.DoChecks();
        InRangeCheck();
    }

    public override void Exit()
    {
        base.Exit();
    }


    #region METHODS

    private void InRangeCheck()
    {
        if (m_DelPilar._inRange)
        {
            characterStateMachine.ChangeState(m_DelPilar.attackState);
        }
    }


    private void ToDeathState()
    {
        if (m_DelPilar.currentHealth <= 0)
        {
            Debug.Log("Hello From to death state");
            characterStateMachine.ChangeState(m_DelPilar.deathState);
        }
    }

    void PassiveHeal()
    {
        timeLastHeal += Time.deltaTime;
        if (timeLastHeal >= healInterval)
        {
            float healValue = (healPercentageAmount * m_DelPilar.maxHp) / 100f;

            m_DelPilar.currentHealth = Mathf.Min(m_DelPilar.maxHp, m_DelPilar.currentHealth + healValue);
            timeLastHeal = 0f;
        }
    }

    #endregion






}
