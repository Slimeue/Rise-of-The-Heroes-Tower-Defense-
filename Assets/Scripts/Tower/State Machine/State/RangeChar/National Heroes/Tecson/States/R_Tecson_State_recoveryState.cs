using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_Tecson_State_recoveryState : CharacterBaseState
{
    R_Tecson r_Tecson;

    float recoveryDuration = 30f;
    float timeStarted;
    float elapsed = 0f;
    float recoveryRate = 1.0f; // percent   
    bool isRecovering = true;


    public R_Tecson_State_recoveryState(CharacterStateMachine characterStateMachine, string animBoolName, CharEntity charEntity, R_Tecson r_Tecson)
    : base(animBoolName, characterStateMachine)
    {
        this.animBoolName = animBoolName;
        this.r_Tecson = r_Tecson;
    }

    public override void Enter()
    {
        base.Enter();
        isRecovering = true;
        r_Tecson.PlayAnim(animBoolName);

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

            float amountToHeal = r_Tecson.characterData.maxHp / recoveryDuration;

            elapsed += Time.deltaTime;

            if (elapsed >= 1f)
            {
                r_Tecson.currentHealth += amountToHeal;
                elapsed = 0f;
            }

            if (timeStarted >= recoveryDuration)
            {
                isRecovering = false;
                r_Tecson.characterStateMachine.ChangeState(r_Tecson.idleState);
                timeStarted = 0f;
            }
        }
    }

}
