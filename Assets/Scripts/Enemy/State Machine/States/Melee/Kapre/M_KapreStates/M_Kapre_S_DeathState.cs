using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Kapre_S_DeathState : BaseState
{

    M_Kapre m_Kapre;

    public M_Kapre_S_DeathState(StateMachine stateMachine, string animBoolName, EnemyEntity enemyEntity, M_Kapre m_Kapre)
    : base(animBoolName, stateMachine)
    {
        this.animBoolName = animBoolName;
        this.m_Kapre = m_Kapre;
    }

    public override void Enter(StateMachine stateMachine)
    {
        base.Enter(stateMachine);
        Debug.Log("Hello From Kapre Death state");
        m_Kapre.PlayAnim(animBoolName);
        m_Kapre.animationHandler.OnDeathFinish += m_Kapre.DestroyGameObject;
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
