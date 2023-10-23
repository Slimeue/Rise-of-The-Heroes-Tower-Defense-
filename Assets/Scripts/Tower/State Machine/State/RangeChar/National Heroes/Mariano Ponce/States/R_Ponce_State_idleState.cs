using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_Ponce_State_idleState : CharacterBaseState
{
    R_Ponce r_Ponce;
    float healInterval = 5f;
    float timeLastHeal = 0f;
    float healPercentageAmount = 5f;

    public R_Ponce_State_idleState(CharacterStateMachine characterStateMachine, string animBoolName, CharEntity charEntity, R_Ponce r_Ponce)
    : base(animBoolName, characterStateMachine)
    {
        this.animBoolName = animBoolName;
        this.r_Ponce = r_Ponce;
    }

    public override void Enter()
    {
        base.Enter();
        r_Ponce.PlayAnim(animBoolName);

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        ToDeathState();
        Debug.Log(animBoolName);
        PassiveHeal();
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
        InRangeCheck();
    }

    public override void Exit()
    {
        base.Exit();
    }

    #region  METHODS

    void ToDeathState()
    {
        if (r_Ponce.currentHealth <= 0)
        {
            r_Ponce.characterStateMachine.ChangeState(r_Ponce.deathState);
        }
    }

    void InRangeCheck()
    {
        if (r_Ponce._inRange)
        {
            r_Ponce.characterStateMachine.ChangeState(r_Ponce.attackState);
        }
    }


    void PassiveHeal()
    {
        timeLastHeal += Time.deltaTime;
        if (timeLastHeal >= healInterval)
        {
            float healValue = (healPercentageAmount * r_Ponce.maxHp) / 100f;

            r_Ponce.currentHealth = Mathf.Min(r_Ponce.maxHp, r_Ponce.currentHealth + healValue);
            timeLastHeal = 0f;
        }
    }

    #endregion
}
