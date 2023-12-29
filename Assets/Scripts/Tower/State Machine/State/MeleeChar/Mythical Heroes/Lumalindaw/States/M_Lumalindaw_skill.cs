using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Lumalindaw_skill : CharacterBaseState
{

    M_Lumalindaw m_Lumalindaw;

    public M_Lumalindaw_skill(CharacterStateMachine characterStateMachine, string animBoolName, CharEntity charEntity, M_Lumalindaw m_Lumalindaw)
    : base(animBoolName, characterStateMachine)
    {
        this.m_Lumalindaw = m_Lumalindaw;
        this.animBoolName = animBoolName;
    }

    public override void Enter()
    {
        base.Enter();
        m_Lumalindaw.PlayAnim(animBoolName);
        m_Lumalindaw.skillFinished = true;
        m_Lumalindaw.animationHandler.OnSkillActivated += SkillActivated;
        m_Lumalindaw.animationHandler.OnSkillFinished += ToAttackState;
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

    public override void Exit()
    {
        base.Exit();
        m_Lumalindaw.animationHandler.OnSkillActivated -= SkillActivated;
        m_Lumalindaw.animationHandler.OnSkillFinished -= ToAttackState;
    }


    void SkillActivated()
    {
        m_Lumalindaw.abilityHolder.SetActive(true);
    }

    void ToAttackState()
    {
        m_Lumalindaw.characterStateMachine.ChangeState(m_Lumalindaw.attackState);
    }

}
