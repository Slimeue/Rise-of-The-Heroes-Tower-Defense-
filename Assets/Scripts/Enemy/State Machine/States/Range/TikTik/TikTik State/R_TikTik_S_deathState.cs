using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_TikTik_S_deathState : BaseState
{
    R_TikTik r_TikTik;

    public R_TikTik_S_deathState(StateMachine stateMachine, string animBoolName, EnemyEntity enemyEntity, R_TikTik r_TikTik)
    : base(animBoolName, stateMachine)
    {
        this.animBoolName = animBoolName;
        this.r_TikTik = r_TikTik;
    }

    public override void Enter(StateMachine stateMachine)
    {
        base.Enter(stateMachine);
        r_TikTik.PlayAnim(animBoolName);
        r_TikTik.animationHandler.OnDeathFinish += r_TikTik.DestroyGameObject;
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
