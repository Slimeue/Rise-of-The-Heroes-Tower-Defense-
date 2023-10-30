using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Valenzuela_S_attackState : CharacterBaseState
{
    Valenzuela m_Valenzuela;

    public M_Valenzuela_S_attackState(CharacterStateMachine characterStateMachine, string animBoolName, CharEntity charEntity, Valenzuela m_Valenzuela)
    : base(animBoolName, characterStateMachine)
    {
        this.animBoolName = animBoolName;
        this.m_Valenzuela = m_Valenzuela;
    }


    public override void Enter()
    {
        base.Enter();
        m_Valenzuela.anim.speed = m_Valenzuela.characterData.attackSpeed;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        m_Valenzuela.PlayAnim(animBoolName);
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
        m_Valenzuela.isAttackFinished = true;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Exit()
    {
        base.Exit();
        m_Valenzuela.anim.speed = 1f;
    }


    #region  METHODS

    private void ToIdleState()
    {
        if (!m_Valenzuela._inRange)
        {
            characterStateMachine.ChangeState(m_Valenzuela.idleState);
        }
    }

    public void DamageListener(Collider collider)
    {
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if (!m_Valenzuela.isAttackFinished)
        {
            return;
        }

        if (collider.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit");
            damageable.Damage(m_Valenzuela.damageValue);
            m_Valenzuela.isAttackFinished = false;
        }
    }

    void FindTarget()
    {
        if (m_Valenzuela.target != null)
        {
            Vector3 targetDir = m_Valenzuela.target.position - m_Valenzuela.transform.position;
            targetDir.y = 0;
            Quaternion targtRotation = Quaternion.LookRotation(targetDir);
            m_Valenzuela.transform.rotation = Quaternion.Slerp(m_Valenzuela.transform.rotation, targtRotation, m_Valenzuela._rotationSpeed * Time.deltaTime);
        }
    }

    void ToDeathState()
    {
        if (m_Valenzuela.currentHealth <= 0)
        {
            characterStateMachine.ChangeState(m_Valenzuela.recoveryState);
        }
    }

    #endregion

}
