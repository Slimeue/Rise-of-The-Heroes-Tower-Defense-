using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_DelPIlar_S_recoveryState : CharacterBaseState
{
    M_DelPilar m_DelPilar;


    float recoveryDuration = 30f;
    float timeStarted;
    float elapsed = 0f;
    float recoveryRate = 1.0f; // percent   
    bool isRecovering = true;


    public M_DelPIlar_S_recoveryState(CharacterStateMachine characterStateMachine, string animBoolName, CharEntity charEntity, M_DelPilar m_DelPilar)
    : base(animBoolName, characterStateMachine)
    {
        this.animBoolName = animBoolName;
        this.m_DelPilar = m_DelPilar;
    }

    public override void Enter()
    {
        base.Enter();
        isRecovering = true;
        m_DelPilar.PlayAnim(animBoolName);
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

            float amountToHeal = m_DelPilar.characterData.maxHp / recoveryDuration;

            elapsed += Time.deltaTime;

            if (elapsed >= 1f)
            {
                m_DelPilar.currentHealth += amountToHeal;
                elapsed = 0f;
            }

            if (timeStarted >= recoveryDuration)
            {
                isRecovering = false;
                m_DelPilar.characterStateMachine.ChangeState(m_DelPilar.idleState);
                timeStarted = 0f;
            }
        }
    }

}
