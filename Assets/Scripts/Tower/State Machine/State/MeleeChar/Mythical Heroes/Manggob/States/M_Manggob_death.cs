using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Manggob_death : CharacterBaseState
{
    M_Manggob m_Manggob;

    public M_Manggob_death(CharacterStateMachine characterStateMachine, string animBoolName, CharEntity charEntity, M_Manggob m_Manggob)
    : base(animBoolName, characterStateMachine)
    {
        this.animBoolName = animBoolName;
        this.m_Manggob = m_Manggob;
    }

    public override void Enter()
    {

        base.Enter();
        m_Manggob.PlayAnim(animBoolName);
        m_Manggob.animationHandler.OnDeathFinish += m_Manggob.DestroyGameObject;
        m_Manggob.animationHandler.OnDeathFinish += m_Manggob.TowerHolderEnabler;
        m_Manggob.isDead = true;

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

    public override void OnTriggerExit(Collider collider)
    {
        base.OnTriggerExit(collider);
    }

    public override void Exit()
    {
        base.Exit();
        m_Manggob.animationHandler.OnDeathFinish -= m_Manggob.TowerHolderEnabler;
    }

}
