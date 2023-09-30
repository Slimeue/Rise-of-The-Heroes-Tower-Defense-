using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_Sandayo_attack : CharacterBaseState
{
    R_Sandayo r_Sandayo;

    public R_Sandayo_attack(CharacterStateMachine characterStateMachine, string animBoolName, CharEntity charEntity, R_Sandayo r_Sandayo)
    : base(animBoolName, characterStateMachine)
    {
        this.r_Sandayo = r_Sandayo;
        this.animBoolName = animBoolName;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Hello from Sandayo Attack state");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        FindTarget();
        r_Sandayo.anim.SetTrigger(animBoolName);
        if (r_Sandayo.currentHealth <= 0)
        {
            characterStateMachine.ChangeState(r_Sandayo.deathState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void OnTriggerEnter()
    {
        base.OnTriggerEnter();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        if (!r_Sandayo._inRange)
        {
            characterStateMachine.ChangeState(r_Sandayo.idleState);
        }
    }
    private void FindTarget()
    {
        if (r_Sandayo.target != null)
        {
            Debug.Log("Targeting");
            Vector3 targetDir = r_Sandayo.target.position - r_Sandayo.transform.position;
            targetDir.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(targetDir);
            r_Sandayo.transform.rotation = Quaternion.Slerp(r_Sandayo.transform.rotation, targetRotation, r_Sandayo._rotationSpeed * Time.deltaTime);
        }
    }

}
