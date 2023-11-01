using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Manggob_attack : CharacterBaseState
{
    Manggob m_Manggob;

    public M_Manggob_attack(CharacterStateMachine characterStateMachine, string animBoolName, CharEntity charEntity, Manggob m_Manggob)
    : base(animBoolName, characterStateMachine)
    {
        this.animBoolName = animBoolName;
        this.m_Manggob = m_Manggob;
    }

    public override void Enter()
    {
        base.Enter();
        m_Manggob.PlayAnim(animBoolName);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        FindTarget();
        ToDeathState();
        SkillActivate();
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
        m_Manggob.isAttackFinished = true;
    }

    public override void Exit()
    {
        base.Exit();
    }

    #region 

    private void ToIdleState()
    {
        if (!m_Manggob._inRange)
        {
            characterStateMachine.ChangeState(m_Manggob.idleState);
        }
    }

    void DamageListener(Collider collider)
    {
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if (m_Manggob.isAttackFinished)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                damageable.Damage(m_Manggob.damageValue);
                Debug.Log(collider.gameObject.name);
                m_Manggob.isAttackFinished = false;
            }
        }

    }

    private void FindTarget()
    {
        if (m_Manggob.target != null)
        {
            Debug.Log("Targeting");
            Vector3 targetDir = m_Manggob.target.position - m_Manggob.transform.position;
            targetDir.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(targetDir);
            m_Manggob.transform.rotation = Quaternion.Slerp(m_Manggob.transform.rotation, targetRotation, m_Manggob._rotationSpeed * Time.deltaTime);
        }
    }

    private void ToDeathState()
    {
        if (m_Manggob.currentHealth <= 0)
        {
            characterStateMachine.ChangeState(m_Manggob.deathState);
        }
    }

    void SkillActivate()
    {
        if (!m_Manggob.skillFinished)
        {
            m_Manggob.characterStateMachine.ChangeState(m_Manggob.skillState);
        }
    }

    #endregion

}
