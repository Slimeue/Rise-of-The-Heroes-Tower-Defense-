using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_Sandayo_skill : CharacterBaseState
{

    Sandayo r_Sandayo;

    public R_Sandayo_skill(CharacterStateMachine characterStateMachine, string animBoolName, CharEntity charEntity, Sandayo r_Sandayo)
    : base(animBoolName, characterStateMachine)
    {
        this.r_Sandayo = r_Sandayo;
        this.animBoolName = animBoolName;
    }

    #region Override Methods 

    public override void Enter()
    {
        base.Enter();
        r_Sandayo.PlayAnim(animBoolName);
        Debug.Log("Skill Activated");
        r_Sandayo.skillFinished = true;
        r_Sandayo.animationHandler.OnSkillActivated += r_Sandayo.SkillAttack;
        r_Sandayo.animationHandler.OnSkillFinished += SkillFinished;

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        r_Sandayo.animationHandler.OnSkillActivated -= r_Sandayo.SkillAttack;
        r_Sandayo.animationHandler.OnSkillFinished -= SkillFinished;
    }

    public override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    #endregion

    void SkillFinished()
    {
        r_Sandayo.characterStateMachine.ChangeState(r_Sandayo.attackState);
    }



}
