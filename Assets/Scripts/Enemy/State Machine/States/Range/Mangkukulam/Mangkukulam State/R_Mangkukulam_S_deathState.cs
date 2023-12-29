using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_Mangkukulam_S_deathState : BaseState
{
    R_Mangkukulam r_Mangkukulam;

    public R_Mangkukulam_S_deathState(StateMachine stateMachine, string animBoolName, EnemyEntity enemyEntity, R_Mangkukulam r_Mangkukulam)
    : base(animBoolName, stateMachine)
    {
        this.animBoolName = animBoolName;
        this.r_Mangkukulam = r_Mangkukulam;
    }

    public override void Enter(StateMachine stateMachine)
    {
        base.Enter(stateMachine);
        r_Mangkukulam.PlayAnim(animBoolName);
        r_Mangkukulam.animationHandler.OnDeathFinish += r_Mangkukulam.DestroyGameObject;
    }

    public override void LogicUpdate(StateMachine stateMachine)
    {
        base.LogicUpdate(stateMachine);
    }

    public override void OnTriggerEnter(StateMachine stateMachine, Collider collider)
    {
        base.OnTriggerEnter(stateMachine, collider);
    }

    public override void OnTriggerExit(StateMachine stateMachine, Collider collider)
    {
        base.OnTriggerExit(stateMachine, collider);
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Exit(StateMachine stateMachine)
    {
        base.Exit(stateMachine);
    }
}
