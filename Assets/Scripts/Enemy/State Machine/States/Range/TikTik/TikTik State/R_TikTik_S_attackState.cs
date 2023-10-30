using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_TikTik_S_attackState : BaseState
{
    R_TikTik r_TikTik;

    public R_TikTik_S_attackState(StateMachine stateMachine, string animBoolName, EnemyEntity enemyEntity, R_TikTik r_TikTik)
    : base(animBoolName, stateMachine)
    {
        this.animBoolName = animBoolName;
        this.r_TikTik = r_TikTik;
    }

    public override void Enter(StateMachine stateMachine)
    {
        base.Enter(stateMachine);
        Debug.Log("Hello From tiktik attackstate");
        r_TikTik.PlayAnim(animBoolName);
    }

    public override void LogicUpdate(StateMachine stateMachine)
    {
        base.LogicUpdate(stateMachine);
        FindTarget();
        ToDeathState();
        InRangeCheck();
    }

    public override void OnTriggerEnter(StateMachine stateMachine, Collider collider)
    {
        base.OnTriggerEnter(stateMachine, collider);
        DamageListener(collider);

    }

    public override void OnTriggerExit(StateMachine stateMachine, Collider collider)
    {
        base.OnTriggerExit(stateMachine, collider);
        r_TikTik.enemyAttackFinished = true;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Exit(StateMachine stateMachine)
    {
        base.Exit(stateMachine);
    }

    void InRangeCheck()
    {
        if (!r_TikTik._inRange)
        {
            r_TikTik.stateMachine.ChangeState(r_TikTik.movingState);
        }
    }

    void ToDeathState()
    {
        if (r_TikTik.currentHealth <= 0f)
        {
            r_TikTik.stateMachine.ChangeState(r_TikTik.deathState);
        }
    }

    private void DamageListener(Collider collider)
    {
        IDamageable tower = collider.GetComponentInParent<IDamageable>();

        if (!r_TikTik.enemyAttackFinished)
        {
            return;
        }

        if (collider.gameObject.CompareTag("Body"))
        {
            tower.Damage(r_TikTik.enemiesData.dmgValue);
        }
    }

    private void FindTarget()
    {
        if (r_TikTik.enemyTarget != null)
        {
            Vector3 targetDir = r_TikTik.enemyTarget.position - r_TikTik.transform.position;
            targetDir.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(targetDir);
            r_TikTik.transform.rotation = Quaternion.Slerp(r_TikTik.transform.rotation, targetRotation, r_TikTik.rotationSpeed * Time.deltaTime);
        }
    }


}
