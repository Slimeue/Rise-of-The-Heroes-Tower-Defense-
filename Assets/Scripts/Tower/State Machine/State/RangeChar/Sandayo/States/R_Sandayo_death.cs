using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_Sandayo_death : CharacterBaseState
{

    R_Sandayo r_Sandayo;

    public R_Sandayo_death(CharacterStateMachine characterStateMachine, string animBoolName, CharEntity charEntity, R_Sandayo r_Sandayo)
    : base(animBoolName, characterStateMachine)
    {
        this.r_Sandayo = r_Sandayo;
        this.animBoolName = animBoolName;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
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
    }

}
