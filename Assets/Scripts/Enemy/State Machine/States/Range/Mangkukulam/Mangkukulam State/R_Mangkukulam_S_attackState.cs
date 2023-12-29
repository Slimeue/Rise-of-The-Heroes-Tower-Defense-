using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class R_Mangkukulam_S_attackState : BaseState
{
    R_Mangkukulam r_Mangkukulam;

    public R_Mangkukulam_S_attackState(StateMachine stateMachine, string animBoolName, EnemyEntity enemyEntity, R_Mangkukulam r_Mangkukulam)
    : base(animBoolName, stateMachine)
    {
        this.animBoolName = animBoolName;
        this.r_Mangkukulam = r_Mangkukulam;
    }

    public override void Enter(StateMachine stateMachine)
    {
        base.Enter(stateMachine);
        r_Mangkukulam.PlayAnim(animBoolName);
        r_Mangkukulam.rangeCharAnimationHandler.OnRangeStartAttack += r_Mangkukulam.Fire;
    }

    public override void LogicUpdate(StateMachine stateMachine)
    {
        base.LogicUpdate(stateMachine);
        ToDeathState();
        FindTarget();
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
        r_Mangkukulam.rangeCharAnimationHandler.OnRangeStartAttack -= r_Mangkukulam.Fire;
    }

    void InRangeCheck()
    {
        if (!r_Mangkukulam._inRange)
        {
            r_Mangkukulam.stateMachine.ChangeState(r_Mangkukulam.movingState);
        }
    }

    void ToDeathState()
    {
        if (r_Mangkukulam.currentHealth <= 0f)
        {
            r_Mangkukulam.stateMachine.ChangeState(r_Mangkukulam.deathState);
        }
    }




    private void FindTarget()
    {
        if (r_Mangkukulam.enemyTarget != null)
        {
            Vector3 targetDir = r_Mangkukulam.enemyTarget.position - r_Mangkukulam.transform.position;
            targetDir.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(targetDir);
            r_Mangkukulam.transform.rotation = Quaternion.Slerp(r_Mangkukulam.transform.rotation, targetRotation, r_Mangkukulam.rotationSpeed * Time.deltaTime);
        }
    }

}
