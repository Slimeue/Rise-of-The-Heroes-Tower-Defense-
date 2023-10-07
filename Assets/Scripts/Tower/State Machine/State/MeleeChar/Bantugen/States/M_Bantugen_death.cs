using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Bantugen_death : CharacterBaseState
{
    M_Bantugen m_Bantugen;

    public M_Bantugen_death(CharacterStateMachine characterStateMachine, string animBoolName, CharEntity charEntity, M_Bantugen m_Bantugen)
    : base(animBoolName, characterStateMachine)
    {
        this.animBoolName = animBoolName;
        this.m_Bantugen = m_Bantugen;
    }

    public override void Enter()
    {
        base.Enter();
        m_Bantugen.PlayAnim(animBoolName);
        m_Bantugen.animationHandler.OnDeathFinish += m_Bantugen.DestroyGameObject;
        m_Bantugen.animationHandler.OnDeathFinish += m_Bantugen.TowerHolderEnabler;
        m_Bantugen.isDead = true;
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
        m_Bantugen.animationHandler.OnDeathFinish -= m_Bantugen.TowerHolderEnabler;
    }
}
