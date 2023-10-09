using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Lumalindaw_death : CharacterBaseState
{
    M_Lumalindaw m_Lumalindaw;

    public M_Lumalindaw_death(CharacterStateMachine characterStateMachine, string animBoolName, CharEntity charEntity, M_Lumalindaw m_Lumalindaw)
    : base(animBoolName, characterStateMachine)
    {
        this.animBoolName = animBoolName;
        this.m_Lumalindaw = m_Lumalindaw;
    }

    public override void Enter()
    {
        base.Enter();
        m_Lumalindaw.PlayAnim(animBoolName);
        m_Lumalindaw.animationHandler.OnDeathFinish += m_Lumalindaw.DestroyGameObject;
        m_Lumalindaw.animationHandler.OnDeathFinish += m_Lumalindaw.TowerHolderEnabler;
        m_Lumalindaw.isDead = true;

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);

    }

    public override void Exit()
    {
        base.Exit();
        m_Lumalindaw.animationHandler.OnDeathFinish -= m_Lumalindaw.TowerHolderEnabler;
    }
}
