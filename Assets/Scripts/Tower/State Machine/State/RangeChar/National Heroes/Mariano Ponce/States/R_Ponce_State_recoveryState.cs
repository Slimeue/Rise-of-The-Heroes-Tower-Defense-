using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class R_Ponce_State_recoveryState : CharacterBaseState
{
    R_Ponce r_Ponce;

    float recoveryDuration = 30f;
    float timeStarted;
    float elapsed = 0f;
    float recoveryRate = 1.0f; // percent   
    bool isRecovering;


    public R_Ponce_State_recoveryState(CharacterStateMachine characterStateMachine, string animBoolName, CharEntity charEntity, R_Ponce r_Ponce)
    : base(animBoolName, characterStateMachine)
    {
        this.animBoolName = animBoolName;
        this.r_Ponce = r_Ponce;
    }

    public override void Enter()
    {
        base.Enter();
        r_Ponce.PlayAnim(animBoolName);
        isRecovering = true;
        r_Ponce.animationHandler.OnDeathFinish -= r_Ponce.ToRecovery;

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
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
    }

    public override void Exit()
    {
        base.Exit();
    }

    void PassiveHeal()
    {
        if (isRecovering)
        {
            timeStarted += Time.deltaTime;

            float amountToHeal = r_Ponce.characterData.maxHp / recoveryDuration;

            elapsed += Time.deltaTime;

            if (elapsed >= 1f)
            {
                r_Ponce.currentHealth += amountToHeal;
                elapsed = 0f;
            }

            if (timeStarted >= recoveryDuration)
            {
                isRecovering = false;
                r_Ponce.characterStateMachine.ChangeState(r_Ponce.idleState);
                timeStarted = 0f;
            }
        }
    }

}
