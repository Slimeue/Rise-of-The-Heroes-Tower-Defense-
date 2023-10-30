using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Manggob_skill : CharacterBaseState
{

    Manggob m_Manggob;

    public M_Manggob_skill(CharacterStateMachine characterStateMachine, string animBoolName, CharEntity charEntity, Manggob m_Manggob)
    : base(animBoolName, characterStateMachine)
    {
        this.animBoolName = animBoolName;
        this.m_Manggob = m_Manggob;
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
