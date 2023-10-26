using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Bantugen_attack : CharacterBaseState
{
    M_Bantugen m_Bantugen;

    public M_Bantugen_attack(CharacterStateMachine characterStateMachine, string animBoolName, CharEntity charEntity, M_Bantugen m_Bantugen)
    : base(animBoolName, characterStateMachine)
    {
        this.animBoolName = animBoolName;
        this.m_Bantugen = m_Bantugen;
    }

    public override void Enter()
    {
        base.Enter();
        m_Bantugen.PlayAnim(animBoolName);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        FindTarget();
        ToDeathState();
        ToSkillState();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        ToIdleState();
    }

    public override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);
        DamageListener(collider);
    }

    public override void OnTriggerExit(Collider collider)
    {
        base.OnTriggerExit(collider);
        m_Bantugen.isAttackFinished = true;
    }

    public override void Exit()
    {
        base.Exit();
    }

    #region  METHODS

    private void ToIdleState()
    {
        if (!m_Bantugen._inRange)
        {
            characterStateMachine.ChangeState(m_Bantugen.idleState);
        }
    }

    public void DamageListener(Collider collider)
    {
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if (!m_Bantugen.isAttackFinished)
        {
            return;
        }

        if (collider.gameObject.CompareTag("Enemy"))
        {
            damageable.Damage(m_Bantugen.damageValue);
            m_Bantugen.isAttackFinished = false;
        }
    }

    void FindTarget()
    {
        if (m_Bantugen.target != null)
        {
            Vector3 targetDir = m_Bantugen.target.position - m_Bantugen.transform.position;
            targetDir.y = 0;
            Quaternion targtRotation = Quaternion.LookRotation(targetDir);
            m_Bantugen.transform.rotation = Quaternion.Slerp(m_Bantugen.transform.rotation, targtRotation, m_Bantugen._rotationSpeed * Time.deltaTime);
        }
    }

    void ToDeathState()
    {
        if (m_Bantugen.currentHealth <= 0)
        {
            characterStateMachine.ChangeState(m_Bantugen.deathState);
        }
    }

    void ToSkillState()
    {
        float healthPercentage = m_Bantugen.maxHp * 0.5f;
        if (m_Bantugen.currentHealth < healthPercentage && !m_Bantugen.skillFinished)
        {
            m_Bantugen.characterStateMachine.ChangeState(m_Bantugen.skillState);
        }
    }

    #endregion

}
