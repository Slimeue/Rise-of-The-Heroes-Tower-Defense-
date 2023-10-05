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

    #region Override Methods

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Hello from Sandayo Attack state");
        r_Sandayo.PlayAnim(animBoolName);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        FindTarget();
        ToDeathState();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);
    }

    public override void DoChecks()
    {
        base.DoChecks();
        if (!r_Sandayo._inRange)
        {
            characterStateMachine.ChangeState(r_Sandayo.idleState);
        }
    }

    #endregion



    #region METHODS 

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

    private void ToDeathState()
    {
        if (r_Sandayo.currentHealth <= 0)
        {
            characterStateMachine.ChangeState(r_Sandayo.deathState);
        }
    }
    #endregion

}
