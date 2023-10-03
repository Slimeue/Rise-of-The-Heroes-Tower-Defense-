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
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void OnTriggerEnter()
    {
        base.OnTriggerEnter();
    }

    public override void Exit()
    {
        base.Exit();
    }

}
