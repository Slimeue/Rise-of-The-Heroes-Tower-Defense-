using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_TikTik_S_idleState : BaseState
{
    R_TikTik r_TikTik;
    float idleTime;

    public R_TikTik_S_idleState(StateMachine stateMachine, string animBoolName, EnemyEntity enemyEntity, R_TikTik r_TikTik)
    : base(animBoolName, stateMachine)
    {
        this.animBoolName = animBoolName;
        this.r_TikTik = r_TikTik;
    }

    public override void Enter(StateMachine stateMachine)
    {
        base.Enter(stateMachine);
        Debug.Log("hello from Tiktik idlestate");
        r_TikTik.PlayAnim(animBoolName);
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

    #region METHODS

    private void InRangeCheck()
    {
        if (!r_TikTik._inRange)
        {
            r_TikTik.stateMachine.ChangeState(r_TikTik.movingState);
        }
        else
        {
            r_TikTik.stateMachine.ChangeState(r_TikTik.attackState);
        }
    }



    #endregion


}
