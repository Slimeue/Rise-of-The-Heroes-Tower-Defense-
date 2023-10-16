using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_Tecson_State_attackState : CharacterBaseState
{
    R_Tecson r_Tecson;

    public R_Tecson_State_attackState(CharacterStateMachine characterStateMachine, string animBoolName, CharEntity charEntity, R_Tecson r_Tecson)
    : base(animBoolName, characterStateMachine)
    {
        this.animBoolName = animBoolName;
        this.r_Tecson = r_Tecson;
    }

    public override void Enter()
    {
        base.Enter();
        r_Tecson.rangeCharAnimationHandler.OnRangeStartAttack += r_Tecson.Fire;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        FindTarget();
        ToDeathState();
        r_Tecson.PlayAnim(animBoolName);

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
    }

    #region METHODS

    void ToIdleState()
    {
        if (!r_Tecson._inRange)
        {
            characterStateMachine.ChangeState(r_Tecson.idleState);
        }
    }

    void FindTarget()
    {
        if (r_Tecson.target != null)
        {
            Vector3 targetDir = r_Tecson.target.position - r_Tecson.transform.position;
            targetDir.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(targetDir);
            r_Tecson.transform.rotation = Quaternion.Slerp(r_Tecson.transform.rotation, targetRotation, r_Tecson._rotationSpeed * Time.deltaTime);

        }
    }

    private void ToDeathState()
    {
        if (r_Tecson.currentHealth <= 0)
        {
            characterStateMachine.ChangeState(r_Tecson.deathState);
        }
    }

    #endregion
}
