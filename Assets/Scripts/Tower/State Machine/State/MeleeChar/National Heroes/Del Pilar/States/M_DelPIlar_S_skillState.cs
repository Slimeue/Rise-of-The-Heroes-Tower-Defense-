using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_DelPIlar_S_skillState : CharacterBaseState
{
    DelPilar m_DelPilar;

    float buffValue = 1.5f;
    float buffDuration = 10f;

    public M_DelPIlar_S_skillState(CharacterStateMachine characterStateMachine, string animBoolName, CharEntity charEntity, DelPilar m_DelPilar)
    : base(animBoolName, characterStateMachine)
    {
        this.animBoolName = animBoolName;
        this.m_DelPilar = m_DelPilar;
    }


    public override void Enter()
    {
        base.Enter();
        m_DelPilar.animationHandler.OnSkillActivated += SkillActivated;
        m_DelPilar.animationHandler.OnSkillFinished += SkillFinished;

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        m_DelPilar.PlayAnim(animBoolName);

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
        m_DelPilar.animationHandler.OnSkillActivated -= SkillActivated;
        m_DelPilar.animationHandler.OnSkillFinished -= SkillFinished;
    }

    void SkillActivated()
    {
        m_DelPilar.skillHolder.SetActive(true);
        foreach (GameObject tower in m_DelPilar.activeTowers)
        {
            IBuffable buffable = tower.GetComponent<IBuffable>();
            buffable.AttackSpeedBuff(buffValue, buffDuration);
        }
    }

    void SkillFinished()
    {
        m_DelPilar.characterStateMachine.ChangeState(m_DelPilar.idleState);
    }

}
