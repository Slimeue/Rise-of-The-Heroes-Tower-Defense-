using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_Ponce_State_deathState : CharacterBaseState
{
    R_Ponce r_Ponce;

    public R_Ponce_State_deathState(CharacterStateMachine characterStateMachine, string animBoolName, CharEntity charEntity, R_Ponce r_Ponce)
    : base(animBoolName, characterStateMachine)
    {
        this.animBoolName = animBoolName;
        this.r_Ponce = r_Ponce;
    }

    public override void Enter()
    {
        base.Enter();
        r_Ponce.PlayAnim(animBoolName);
        r_Ponce.animationHandler.OnDeathFinish += r_Ponce.ToRecovery;
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
