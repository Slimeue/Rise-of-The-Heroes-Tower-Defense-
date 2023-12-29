using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Bungisngis_S_DeathState : BaseState
{
    M_Bungisngis m_Bungisngis;

    public M_Bungisngis_S_DeathState(StateMachine stateMachine, string animBoolName, EnemyEntity enemyEntity, M_Bungisngis m_Bungisngis)
    : base(animBoolName, stateMachine)
    {
        this.animBoolName = animBoolName;
        this.m_Bungisngis = m_Bungisngis;
    }

    public override void Enter(StateMachine stateMachine)
    {
        base.Enter(stateMachine);
        m_Bungisngis.PlayAnim(animBoolName);
        m_Bungisngis.animationHandler.OnDeathFinish += m_Bungisngis.DestroyGameObject;
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
