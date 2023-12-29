using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_Mangkukulam_S_idleState : BaseState
{
    R_Mangkukulam r_Mangkukulam;

    public R_Mangkukulam_S_idleState(StateMachine stateMachine, string animBoolName, EnemyEntity enemyEntity, R_Mangkukulam r_Mangkukulam)
    : base(animBoolName, stateMachine)
    {
        this.animBoolName = animBoolName;
        this.r_Mangkukulam = r_Mangkukulam;
    }

    public override void Enter(StateMachine stateMachine)
    {
        base.Enter(stateMachine);
        r_Mangkukulam.PlayAnim(animBoolName);
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
        InRangeCheck();
    }

    public override void Exit(StateMachine stateMachine)
    {
        base.Exit(stateMachine);
    }

    private void InRangeCheck()
    {
        if (!r_Mangkukulam._inRange)
        {
            r_Mangkukulam.stateMachine.ChangeState(r_Mangkukulam.movingState);
        }
        else
        {
            r_Mangkukulam.stateMachine.ChangeState(r_Mangkukulam.attackState);
        }
    }

}
