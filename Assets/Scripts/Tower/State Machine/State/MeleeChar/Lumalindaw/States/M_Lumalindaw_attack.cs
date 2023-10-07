using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Lumalindaw_attack : CharacterBaseState
{
    M_Lumalindaw m_Lumalindaw;


    public M_Lumalindaw_attack(CharacterStateMachine characterStateMachine, string animBoolName, CharEntity charEntity, M_Lumalindaw m_Lumalindaw)
    : base(animBoolName, characterStateMachine)
    {
        this.animBoolName = animBoolName;
        this.m_Lumalindaw = m_Lumalindaw;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Hello From Lumalindaw attack state");
        m_Lumalindaw.PlayAnim(animBoolName);

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        FindTarget();
        ToDeathState();
    }

    public override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);
        DamageListener(collider);
    }

    public override void OnTriggerExit(Collider collider)
    {
        base.OnTriggerExit(collider);
        m_Lumalindaw.isAttackFinished = true;
    }



    public override void DoChecks()
    {
        base.DoChecks();
        ToIdleState();
    }



    public override void Exit()
    {
        base.Exit();
    }


    #region METHODS 

    private void ToIdleState()
    {
        if (!m_Lumalindaw._inRange)
        {
            characterStateMachine.ChangeState(m_Lumalindaw.idleState);
        }
    }

    void DamageListener(Collider collider)
    {
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if (m_Lumalindaw.isAttackFinished)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                damageable.Damage(m_Lumalindaw.characterData.dmgValue);
                Debug.Log(collider.gameObject.name);
                m_Lumalindaw.isAttackFinished = false;
            }
        }

    }

    private void FindTarget()
    {
        if (m_Lumalindaw.target != null)
        {
            Debug.Log("Targeting");
            Vector3 targetDir = m_Lumalindaw.target.position - m_Lumalindaw.transform.position;
            targetDir.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(targetDir);
            m_Lumalindaw.transform.rotation = Quaternion.Slerp(m_Lumalindaw.transform.rotation, targetRotation, m_Lumalindaw._rotationSpeed * Time.deltaTime);
        }
    }

    private void ToDeathState()
    {
        if (m_Lumalindaw.currentHealth <= 0)
        {
            characterStateMachine.ChangeState(m_Lumalindaw.deathState);
        }
    }
    #endregion

}
