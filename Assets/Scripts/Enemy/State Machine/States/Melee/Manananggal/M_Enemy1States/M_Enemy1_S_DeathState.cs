using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Enemy1_S_DeathState : BaseState
{

    M_Enemy1 m_Enemy1;

    public M_Enemy1_S_DeathState(StateMachine stateMachine, string animBoolName, EnemyEntity enemyEntity, M_Enemy1 m_Enemy1)
    : base(animBoolName, stateMachine)
    {
        this.animBoolName = animBoolName;
        this.m_Enemy1 = m_Enemy1;
    }

    public override void Enter(StateMachine stateMachine)
    {
        base.Enter(stateMachine);
        m_Enemy1.PlayAnim(animBoolName);
        m_Enemy1.animationHandler.OnDeathFinish += m_Enemy1.DestroyGameObject;
    }

    public override void LogicUpdate(StateMachine stateMachine)
    {
        base.LogicUpdate(stateMachine);
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Exit(StateMachine stateMachine)
    {
        base.Exit(stateMachine);
    }

    public override void OnTriggerEnter(StateMachine stateMachine, Collider collider)
    {
        base.OnTriggerEnter(stateMachine, collider);
    }


}
