using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_WhiteLady_S_deathState : BaseState
{
    M_WhiteLady m_WhiteLady;

    public M_WhiteLady_S_deathState(StateMachine stateMachine, string animBoolName, EnemyEntity enemyEntity, M_WhiteLady m_WhiteLady)
    : base(animBoolName, stateMachine)
    {
        this.animBoolName = animBoolName;
        this.m_WhiteLady = m_WhiteLady;
    }

    public override void Enter(StateMachine stateMachine)
    {
        base.Enter(stateMachine);
        Debug.Log("Hello From Kapre Death state");
        m_WhiteLady.PlayAnim(animBoolName);
        m_WhiteLady.animationHandler.OnDeathFinish += m_WhiteLady.DestroyGameObject;
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
