using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Lumalindaw_Idle : CharacterBaseState
{
    M_Lumalindaw m_Lumalindaw;

    public M_Lumalindaw_Idle(CharacterStateMachine characterStateMachine, string animBoolName, CharEntity charEntity, M_Lumalindaw m_Lumalindaw)
    : base(animBoolName, characterStateMachine)
    {
        this.animBoolName = animBoolName;
        this.m_Lumalindaw = m_Lumalindaw;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Hello From Lumalindaw idle state");
        m_Lumalindaw.PlayAnim(animBoolName);
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

    public override void Exit()
    {
        base.Exit();
    }


    #region METHODS

    private void InRangeCheck()
    {
        if (m_Lumalindaw._inRange)
        {
            characterStateMachine.ChangeState(m_Lumalindaw.attackState);
        }
    }


    private void ToDeathState()
    {
        if (m_Lumalindaw.currentHealth <= 0)
        {
            characterStateMachine.ChangeState(m_Lumalindaw.deathState);
        }
    }

    #endregion

}
