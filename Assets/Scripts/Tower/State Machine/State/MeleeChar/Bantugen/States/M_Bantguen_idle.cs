using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Bantguen_idle : CharacterBaseState
{
    M_Bantugen m_Bantugen;

    public M_Bantguen_idle(CharacterStateMachine characterStateMachine, string animBoolName, CharEntity charEntity, M_Bantugen m_Bantugen)
    : base(animBoolName, characterStateMachine)
    {
        this.animBoolName = animBoolName;
        this.m_Bantugen = m_Bantugen;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Hello From Bantugen Idle State");
        m_Bantugen.PlayAnim(animBoolName);
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
        if (m_Bantugen._inRange)
        {
            characterStateMachine.ChangeState(m_Bantugen.attackState);
        }
    }


    private void ToDeathState()
    {
        if (m_Bantugen.currentHealth <= 0)
        {
            characterStateMachine.ChangeState(m_Bantugen.deathState);
        }
    }

    #endregion

}
