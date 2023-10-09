using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Valenzuela_S_idleState : CharacterBaseState
{
    M_Valenzuela m_Valenzuela;

    public M_Valenzuela_S_idleState(CharacterStateMachine characterStateMachine, string animBoolName, CharEntity charEntity, M_Valenzuela m_Valenzuela)
    : base(animBoolName, characterStateMachine)
    {
        this.animBoolName = animBoolName;
        this.m_Valenzuela = m_Valenzuela;
    }

    public override void Enter()
    {
        base.Enter();
        m_Valenzuela.PlayAnim(animBoolName);
        Debug.Log("Hello From valenzuela idle state");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        ToDeathState();
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


    #region METHODS

    private void InRangeCheck()
    {
        if (m_Valenzuela._inRange)
        {
            characterStateMachine.ChangeState(m_Valenzuela.attackState);
        }
    }


    private void ToDeathState()
    {
        if (m_Valenzuela.currentHealth <= 0)
        {
            Debug.Log("Hello From to death state");
            characterStateMachine.ChangeState(m_Valenzuela.recoveryState);
        }
    }

    #endregion
}
