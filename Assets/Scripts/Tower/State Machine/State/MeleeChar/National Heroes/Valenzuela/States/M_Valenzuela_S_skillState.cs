using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Valenzuela_S_skillState : CharacterBaseState
{
    Valenzuela m_Valenzuela;

    public M_Valenzuela_S_skillState(CharacterStateMachine characterStateMachine, string animBoolName, CharEntity charEntity, Valenzuela m_Valenzuela)
    : base(animBoolName, characterStateMachine)
    {
        this.animBoolName = animBoolName;
        this.m_Valenzuela = m_Valenzuela;
    }


    public override void Enter()
    {
        base.Enter();
        m_Valenzuela.animationHandler.OnSkillActivated += SkillActivated;
        m_Valenzuela.animationHandler.OnSkillFinished += SkillFinished;

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        m_Valenzuela.PlayAnim(animBoolName);

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
        m_Valenzuela.animationHandler.OnSkillActivated -= SkillActivated;
        m_Valenzuela.animationHandler.OnSkillFinished -= SkillFinished;
    }

    void SkillActivated()
    {
        m_Valenzuela.skillHolder.SetActive(true);
        CameraShake.instance.ShakeCamera(2f, 1f);
    }

    void SkillFinished()
    {
        m_Valenzuela.characterStateMachine.ChangeState(m_Valenzuela.idleState);
    }
}
