using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_DelPIlar_S_deathState : CharacterBaseState
{
    DelPilar m_DelPilar;

    public M_DelPIlar_S_deathState(CharacterStateMachine characterStateMachine, string animBoolName, CharEntity charEntity, DelPilar m_DelPilar)
    : base(animBoolName, characterStateMachine)
    {
        this.animBoolName = animBoolName;
        this.m_DelPilar = m_DelPilar;
    }

    public override void Enter()
    {
        base.Enter();
        m_DelPilar.PlayAnim(animBoolName);
        m_DelPilar.animationHandler.OnDeathFinish += m_DelPilar.ToRecovery;
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
    }




}
