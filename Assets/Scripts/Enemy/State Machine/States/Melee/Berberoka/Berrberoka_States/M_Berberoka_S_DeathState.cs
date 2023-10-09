using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Berberoka_S_DeathState : BaseState
{

    M_Berberoka m_Berberoka;

    public M_Berberoka_S_DeathState(StateMachine stateMachine, string animBoolName, EnemyEntity enemyEntity, M_Berberoka m_Berberoka)
    : base(animBoolName, stateMachine)
    {
        this.animBoolName = animBoolName;
        this.m_Berberoka = m_Berberoka;
    }

    public override void Enter(StateMachine stateMachine)
    {
        base.Enter(stateMachine);
        m_Berberoka.PlayAnim(animBoolName);
        m_Berberoka.animationHandler.OnDeathFinish += m_Berberoka.DestroyGameObject;
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

    public override void OnTriggerExit(StateMachine stateMachine, Collider collider)
    {
        base.OnTriggerExit(stateMachine, collider);
    }

    public override void Exit(StateMachine stateMachine)
    {
        base.Exit(stateMachine);
    }


}
