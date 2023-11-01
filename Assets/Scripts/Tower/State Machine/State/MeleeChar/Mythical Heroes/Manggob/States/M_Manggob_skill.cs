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
        m_Manggob.PlayAnim(animBoolName);
        m_Manggob.skillFinished = true;
        m_Manggob.animationHandler.OnSkillActivated += SkillActivated;
        m_Manggob.animationHandler.OnSkillFinished += ToAttackState;
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
        m_Manggob.animationHandler.OnSkillActivated -= SkillActivated;
        m_Manggob.animationHandler.OnSkillFinished -= ToAttackState;
    }

    void SkillActivated()
    {
        m_Manggob.skillHolder.SetActive(true);
    }

    void ToAttackState()
    {
        m_Manggob.characterStateMachine.ChangeState(m_Manggob.attackState);
    }

}
