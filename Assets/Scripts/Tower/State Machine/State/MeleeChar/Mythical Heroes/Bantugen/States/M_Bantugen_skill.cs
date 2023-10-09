using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Bantugen_skill : CharacterBaseState
{
    M_Bantugen m_Bantugen;

    public M_Bantugen_skill(CharacterStateMachine characterStateMachine, string animBoolName, CharEntity charEntity, M_Bantugen m_Bantugen)
    : base(animBoolName, characterStateMachine)
    {
        this.animBoolName = animBoolName;
        this.m_Bantugen = m_Bantugen;
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
}
