using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_Ponce_State_skillState : CharacterBaseState
{
    R_Ponce r_Ponce;

    public R_Ponce_State_skillState(CharacterStateMachine characterStateMachine, string animBoolName, CharEntity charEntity, R_Ponce r_Ponce)
    : base(animBoolName, characterStateMachine)
    {
        this.animBoolName = animBoolName;
        this.r_Ponce = r_Ponce;
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
