using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_Ponce_State_skillState : CharacterBaseState
{
    R_Ponce r_Ponce;


    float buffValue = 2f;
    float buffDuration = 10f;

    public R_Ponce_State_skillState(CharacterStateMachine characterStateMachine, string animBoolName, CharEntity charEntity, R_Ponce r_Ponce)
    : base(animBoolName, characterStateMachine)
    {
        this.animBoolName = animBoolName;
        this.r_Ponce = r_Ponce;
    }

    public override void Enter()
    {
        base.Enter();
        r_Ponce.PlayAnim(animBoolName);
        r_Ponce.animationHandler.OnSkillActivated += SkillActivated;
        r_Ponce.animationHandler.OnSkillFinished += SkillFinished;

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
        r_Ponce.animationHandler.OnSkillActivated -= SkillActivated;
        r_Ponce.animationHandler.OnSkillFinished -= SkillFinished;

    }

    void SkillActivated()
    {
        r_Ponce.skillHolder.SetActive(true);
        foreach (GameObject tower in r_Ponce.activeTowers)
        {
            IBuffable buffable = tower.GetComponent<IBuffable>();
            buffable.AttackBuff(buffValue, buffDuration);
        }
    }

    void SkillFinished()
    {
        r_Ponce.characterStateMachine.ChangeState(r_Ponce.idleState);
    }
}
