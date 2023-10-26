using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_TikTik_S_moveState : BaseState
{
    R_TikTik r_TikTik;

    public R_TikTik_S_moveState(StateMachine stateMachine, string animBoolName, EnemyEntity enemyEntity, R_TikTik r_TikTik)
    : base(animBoolName, stateMachine)
    {
        this.animBoolName = animBoolName;
        this.r_TikTik = r_TikTik;
    }

    public override void Enter(StateMachine stateMachine)
    {
        base.Enter(stateMachine);
        Debug.Log("hello from Tiktik movestate");
        r_TikTik.PlayAnim(animBoolName);
    }

    public override void LogicUpdate(StateMachine stateMachine)
    {
        base.LogicUpdate(stateMachine);
        FollowPath();
        ToDeathState();
        ToAttackState();

    }

    public override void OnTriggerEnter(StateMachine stateMachine, Collider collider)
    {
        base.OnTriggerEnter(stateMachine, collider);
    }

    public override void OnTriggerExit(StateMachine stateMachine, Collider collider)
    {
        base.OnTriggerExit(stateMachine, collider);
    }

    public override void DoChecks()
    {
        base.DoChecks();

    }

    public override void Exit(StateMachine stateMachine)
    {
        base.Exit(stateMachine);
    }

    //to attack

    void ToDeathState()
    {
        if (r_TikTik.currentHealth <= 0f)
        {
            r_TikTik.stateMachine.ChangeState(r_TikTik.deathState);
        }
    }

    private void ToAttackState()
    {
        if (r_TikTik._inRange)
        {
            r_TikTik.stateMachine.ChangeState(r_TikTik.attackState);
        }
    }

    public void FollowPath()
    {
        r_TikTik.gameObject.transform.LookAt(r_TikTik.target);

        Vector3 dir = r_TikTik.target.position - r_TikTik.gameObject.transform.position;
        r_TikTik.gameObject.transform.Translate(dir.normalized * r_TikTik.speed * Time.deltaTime, Space.World);


        if (Vector3.Distance(r_TikTik.transform.position, r_TikTik.target.position) <= 0.2f)
        {
            GetNextWayPoint();
        }
    }

    private void GetNextWayPoint()
    {
        if (r_TikTik.pointIndex >= Waypoints.points.Length - 1)
        {
            r_TikTik.gameObject.SetActive(false);
            r_TikTik.pointIndex = 0;
            r_TikTik.target = Waypoints.points[r_TikTik.pointIndex];
            return;
        }
        r_TikTik.pointIndex++;
        r_TikTik.target = Waypoints.points[r_TikTik.pointIndex];
    }


}

