using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Manggob_idle : CharacterBaseState
{
    M_Manggob m_Manggob;

    public M_Manggob_idle(CharacterStateMachine characterStateMachine, string animBoolName, CharEntity charEntity, M_Manggob m_Manggob)
    : base(animBoolName, characterStateMachine)
    {
        this.animBoolName = animBoolName;
        this.m_Manggob = m_Manggob;
    }

    public override void Enter()
    {
        base.Enter();
        m_Manggob.PlayAnim(animBoolName);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        ToDeathState();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        InRangeCheck();

    }

    public override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);
    }

    public override void OnTriggerExit(Collider collider)
    {
        base.OnTriggerExit(collider);
    }

    public override void Exit()
    {
        base.Exit();
    }

    #region METHODS

    private void InRangeCheck()
    {
        if (m_Manggob._inRange)
        {
            characterStateMachine.ChangeState(m_Manggob.attackState);
        }
    }


    private void ToDeathState()
    {
        if (m_Manggob.currentHealth <= 0)
        {
            characterStateMachine.ChangeState(m_Manggob.deathState);
        }
    }

    #endregion

}
