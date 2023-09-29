using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_Sandayo_idle : CharacterBaseState
{

    R_Sandayo r_Sandayo;

    public R_Sandayo_idle(CharacterStateMachine characterStateMachine, string animBoolName, CharEntity charEntity, R_Sandayo r_Sandayo)
    : base(animBoolName, characterStateMachine)
    {
        this.r_Sandayo = r_Sandayo;
        this.animBoolName = animBoolName;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Hello From Sandayo Idle State");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        r_Sandayo.anim.Play(animBoolName);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void OnTriggerEnter()
    {
        base.OnTriggerEnter();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        if (r_Sandayo._inRange)
        {
            characterStateMachine.ChangeState(r_Sandayo.attackState);
        }
    }
}
