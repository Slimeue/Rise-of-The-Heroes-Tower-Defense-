using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Valenzuela_S_skillState : CharacterBaseState
{
    M_Valenzuela m_Valenzuela;

    public M_Valenzuela_S_skillState(CharacterStateMachine characterStateMachine, string animBoolName, CharEntity charEntity, M_Valenzuela m_Valenzuela)
    : base(animBoolName, characterStateMachine)
    {
        this.animBoolName = animBoolName;
        this.m_Valenzuela = m_Valenzuela;
    }


    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
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
    }

    public override void Exit()
    {
        base.Exit();
    }
}