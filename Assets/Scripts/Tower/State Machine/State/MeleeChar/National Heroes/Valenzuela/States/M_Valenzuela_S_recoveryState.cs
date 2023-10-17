using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Valenzuela_S_recoveryState : CharacterBaseState
{
    M_Valenzuela m_Valenzuela;


    float recoveryDuration = 30f;
    float timeStarted;
    float elapsed = 0f;
    float recoveryRate = 1.0f; // percent   
    bool isRecovering = true;

    public M_Valenzuela_S_recoveryState(CharacterStateMachine characterStateMachine, string animBoolName, CharEntity charEntity, M_Valenzuela m_Valenzuela)
    : base(animBoolName, characterStateMachine)
    {
        this.animBoolName = animBoolName;
        this.m_Valenzuela = m_Valenzuela;
    }


    public override void Enter()
    {
        base.Enter();
        isRecovering = true;
        m_Valenzuela.PlayAnim(animBoolName);
        Debug.Log("Hello from Valenzuela RecoveryState");
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

            float amountToHeal = m_Valenzuela.characterData.maxHp / recoveryDuration;

            elapsed += Time.deltaTime;

            if (elapsed >= 1f)
            {
                m_Valenzuela.currentHealth += amountToHeal;
                elapsed = 0f;
            }

            if (timeStarted >= recoveryDuration)
            {
                isRecovering = false;
                m_Valenzuela.characterStateMachine.ChangeState(m_Valenzuela.idleState);
                timeStarted = 0f;
            }
        }
    }



}
