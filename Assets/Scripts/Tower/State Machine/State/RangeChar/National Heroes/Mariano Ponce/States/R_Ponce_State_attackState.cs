using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_Ponce_State_attackState : CharacterBaseState
{
    R_Ponce r_Ponce;

    public R_Ponce_State_attackState(CharacterStateMachine characterStateMachine, string animBoolName, CharEntity charEntity, R_Ponce r_Ponce)
    : base(animBoolName, characterStateMachine)
    {
        this.animBoolName = animBoolName;
        this.r_Ponce = r_Ponce;
    }

    public override void Enter()
    {
        base.Enter();
        //r_Ponce.PlayAnim(animBoolName);
        r_Ponce.rangeCharAnimationHandler.OnRangeStartAttack += r_Ponce.Fire;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        FindTarget();
        ToDeathState();
        r_Ponce.PlayAnim(animBoolName);
    }

    public override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);
    }

    public override void OnTriggerExit(Collider collider)
    {
        base.OnTriggerExit(collider);
    }

    public override void DoChecks()
    {
        base.DoChecks();
        ToIdleState();
    }

    public override void Exit()
    {
        base.Exit();
        r_Ponce.rangeCharAnimationHandler.OnRangeStartAttack -= r_Ponce.Fire;

    }


    #region METHODS

    void ToIdleState()
    {
        if (!r_Ponce._inRange)
        {
            characterStateMachine.ChangeState(r_Ponce.idleState);
        }
    }

    void FindTarget()
    {
        if (r_Ponce.target != null)
        {
            Vector3 targetDir = r_Ponce.target.position - r_Ponce.transform.position;
            targetDir.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(targetDir);
            r_Ponce.transform.rotation = Quaternion.Slerp(r_Ponce.transform.rotation, targetRotation, r_Ponce._rotationSpeed * Time.deltaTime);

        }
    }

    private void ToDeathState()
    {
        if (r_Ponce.currentHealth <= 0)
        {
            characterStateMachine.ChangeState(r_Ponce.deathState);
        }
    }

    #endregion

}
