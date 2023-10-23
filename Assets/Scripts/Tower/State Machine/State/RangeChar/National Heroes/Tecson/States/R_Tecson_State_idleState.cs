using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_Tecson_State_idleState : CharacterBaseState
{
    R_Tecson r_Tecson;
    float healInterval = 5f;
    float timeLastHeal = 0f;
    float healPercentageAmount = 5f;

    public R_Tecson_State_idleState(CharacterStateMachine characterStateMachine, string animBoolName, CharEntity charEntity, R_Tecson r_Tecson)
    : base(animBoolName, characterStateMachine)
    {
        this.animBoolName = animBoolName;
        this.r_Tecson = r_Tecson;
    }

    public override void Enter()
    {
        base.Enter();
        r_Tecson.PlayAnim(animBoolName);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        ToDeathState();
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


    void ToDeathState()
    {
        if (r_Tecson.currentHealth <= 0)
        {
            r_Tecson.characterStateMachine.ChangeState(r_Tecson.deathState);
        }
    }

    void InRangeCheck()
    {
        if (r_Tecson._inRange)
        {
            r_Tecson.characterStateMachine.ChangeState(r_Tecson.attackState);
        }
    }


    void PassiveHeal()
    {
        timeLastHeal += Time.deltaTime;
        if (timeLastHeal >= healInterval)
        {
            float healValue = (healPercentageAmount * r_Tecson.maxHp) / 100f;

            r_Tecson.currentHealth = Mathf.Min(r_Tecson.maxHp, r_Tecson.currentHealth + healValue);
            timeLastHeal = 0f;
        }
    }

}
