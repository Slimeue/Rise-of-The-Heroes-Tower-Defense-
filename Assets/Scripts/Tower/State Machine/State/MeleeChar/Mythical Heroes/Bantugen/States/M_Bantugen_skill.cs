using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Bantugen_skill : CharacterBaseState
{
    M_Bantugen m_Bantugen;

    public M_Bantugen_skill(CharacterStateMachine characterStateMachine, string animBoolName, CharEntity charEntity, M_Bantugen m_Bantugen)
    : base(animBoolName, characterStateMachine)
    {
        this.animBoolName = animBoolName;
        this.m_Bantugen = m_Bantugen;
    }

    public override void Enter()
    {
        base.Enter();
        m_Bantugen.PlayAnim(animBoolName);
        m_Bantugen.animationHandler.OnSkillActivated += SkillActivate;
        m_Bantugen.animationHandler.OnSkillFinished += SkillFinished;
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
        m_Bantugen.animationHandler.OnSkillActivated -= SkillActivate;
        m_Bantugen.animationHandler.OnSkillFinished -= SkillFinished;
    }

    void SkillActivate()
    {
        m_Bantugen.ability.SetActive(true);
        m_Bantugen.baseArmor += m_Bantugen.baseArmor * 1f;
    }

    public void SkillFinished()
    {
        m_Bantugen.skillFinished = true;
        m_Bantugen.characterStateMachine.ChangeState(m_Bantugen.attackState);
    }

}
