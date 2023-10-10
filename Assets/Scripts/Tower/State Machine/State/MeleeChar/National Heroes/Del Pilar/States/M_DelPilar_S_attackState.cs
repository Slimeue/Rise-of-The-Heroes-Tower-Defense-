using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_DelPilar_S_attackState : CharacterBaseState
{
    M_DelPilar m_DelPilar;

    public M_DelPilar_S_attackState(CharacterStateMachine characterStateMachine, string animBoolName, CharEntity charEntity, M_DelPilar m_DelPilar)
    : base(animBoolName, characterStateMachine)
    {
        this.animBoolName = animBoolName;
        this.m_DelPilar = m_DelPilar;
    }


    public override void Enter()
    {
        base.Enter();
        m_DelPilar.anim.speed = m_DelPilar.characterData.attackSpeed;
        m_DelPilar.PlayAnim(animBoolName);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        ToIdleState();
        ToDeathState();
        FindTarget();
    }

    public override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);
        DamageListener(collider);
    }

    public override void OnTriggerExit(Collider collider)
    {
        base.OnTriggerExit(collider);
        m_DelPilar.isAttackFinished = true;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Exit()
    {
        base.Exit();
        m_DelPilar.anim.speed = 1f;
    }


    #region  METHODS

    private void ToIdleState()
    {
        if (!m_DelPilar._inRange)
        {
            characterStateMachine.ChangeState(m_DelPilar.idleState);
        }
    }

    public void DamageListener(Collider collider)
    {
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if (!m_DelPilar.isAttackFinished)
        {
            return;
        }

        if (collider.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit");
            damageable.Damage(m_DelPilar.characterData.dmgValue);
            m_DelPilar.isAttackFinished = false;
        }
    }

    void FindTarget()
    {
        if (m_DelPilar.target != null)
        {
            Vector3 targetDir = m_DelPilar.target.position - m_DelPilar.transform.position;
            targetDir.y = 0;
            Quaternion targtRotation = Quaternion.LookRotation(targetDir);
            m_DelPilar.transform.rotation = Quaternion.Slerp(m_DelPilar.transform.rotation, targtRotation, m_DelPilar._rotationSpeed * Time.deltaTime);
        }
    }

    void ToDeathState()
    {
        if (m_DelPilar.currentHealth <= 0)
        {
            characterStateMachine.ChangeState(m_DelPilar.recoveryState);
        }
    }

    #endregion
}
