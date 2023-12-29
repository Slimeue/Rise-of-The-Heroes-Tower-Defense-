using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Tikbalang_S_deathState : BaseState
{
    M_Tikbalang m_Tikbalang;

    public M_Tikbalang_S_deathState(StateMachine stateMachine, string animBoolName, EnemyEntity enemyEntity, M_Tikbalang m_Tikbalang)
    : base(animBoolName, stateMachine)
    {
        this.animBoolName = animBoolName;
        this.m_Tikbalang = m_Tikbalang;
    }

    public override void Enter(StateMachine stateMachine)
    {
        base.Enter(stateMachine);
        Debug.Log("Hello From Kapre Death state");
        m_Tikbalang.PlayAnim(animBoolName);
        m_Tikbalang.animationHandler.OnDeathFinish += m_Tikbalang.DestroyGameObject;
    }

    public override void LogicUpdate(StateMachine stateMachine)
    {
        base.LogicUpdate(stateMachine);
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void OnTriggerEnter(StateMachine stateMachine, Collider collider)
    {
        base.OnTriggerEnter(stateMachine, collider);
    }

    public override void Exit(StateMachine stateMachine)
    {
        base.Exit(stateMachine);
    }

}
