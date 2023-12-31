using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_Tecson_State_deathState : CharacterBaseState
{
    R_Tecson r_Tecson;

    public R_Tecson_State_deathState(CharacterStateMachine characterStateMachine, string animBoolName, CharEntity charEntity, R_Tecson r_Tecson)
    : base(animBoolName, characterStateMachine)
    {
        this.animBoolName = animBoolName;
        this.r_Tecson = r_Tecson;
    }

    public override void Enter()
    {
        base.Enter();
        r_Tecson.PlayAnim(animBoolName);
        r_Tecson.animationHandler.OnDeathFinish += r_Tecson.ToRecovery;
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
