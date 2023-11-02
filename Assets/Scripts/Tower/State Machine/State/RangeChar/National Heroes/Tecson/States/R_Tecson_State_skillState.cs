using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_Tecson_State_skillState : CharacterBaseState
{
    R_Tecson r_Tecson;

    public R_Tecson_State_skillState(CharacterStateMachine characterStateMachine, string animBoolName, CharEntity charEntity, R_Tecson r_Tecson)
    : base(animBoolName, characterStateMachine)
    {
        this.animBoolName = animBoolName;
        this.r_Tecson = r_Tecson;
    }

    public override void Enter()
    {
        base.Enter();
        r_Tecson.PlayAnim(animBoolName);
        r_Tecson.animationHandler.OnSkillActivated += r_Tecson.SkillActivated;
        r_Tecson.animationHandler.OnSkillFinished += SkillFinished;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        ToDeathState();
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
        r_Tecson.animationHandler.OnSkillActivated -= r_Tecson.SkillActivated;
        r_Tecson.animationHandler.OnSkillFinished -= SkillFinished;
    }


    void SkillFinished()
    {
        r_Tecson.characterStateMachine.ChangeState(r_Tecson.idleState);
    }

    private void ToDeathState()
    {
        if (r_Tecson.currentHealth <= 0)
        {
            characterStateMachine.ChangeState(r_Tecson.deathState);
        }
    }
}
