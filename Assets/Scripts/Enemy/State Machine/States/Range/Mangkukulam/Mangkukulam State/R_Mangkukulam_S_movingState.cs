using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_Mangkukulam_S_movingState : BaseState
{
    R_Mangkukulam r_Mangkukulam;

    public R_Mangkukulam_S_movingState(StateMachine stateMachine, string animBoolName, EnemyEntity enemyEntity, R_Mangkukulam r_Mangkukulam)
    : base(animBoolName, stateMachine)
    {
        this.animBoolName = animBoolName;
        this.r_Mangkukulam = r_Mangkukulam;
    }

    public override void Enter(StateMachine stateMachine)
    {
        base.Enter(stateMachine);
        r_Mangkukulam.PlayAnim(animBoolName);
    }

    public override void LogicUpdate(StateMachine stateMachine)
    {
        base.LogicUpdate(stateMachine);
        ToDeathState();
        ToAttackState();

    }

    public override void PhysicsUpdate(StateMachine stateMachine)
    {
        base.PhysicsUpdate(stateMachine);
        FollowPath();
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
        if (r_Mangkukulam.currentHealth <= 0f)
        {
            r_Mangkukulam.stateMachine.ChangeState(r_Mangkukulam.deathState);
        }
    }

    private void ToAttackState()
    {
        if (r_Mangkukulam._inRange)
        {
            r_Mangkukulam.stateMachine.ChangeState(r_Mangkukulam.attackState);
        }
    }

    public void FollowPath()
    {
        r_Mangkukulam.gameObject.transform.LookAt(r_Mangkukulam.target);

        Vector3 dir = r_Mangkukulam.target.position - r_Mangkukulam.gameObject.transform.position;
        r_Mangkukulam.gameObject.transform.Translate(dir.normalized * r_Mangkukulam.speed * Time.deltaTime, Space.World);


        if (Vector3.Distance(r_Mangkukulam.transform.position, r_Mangkukulam.target.position) <= 0.2f)
        {
            GetNextWayPoint();
        }
    }

    private void GetNextWayPoint()
    {
        if (r_Mangkukulam.pointIndex >= Waypoints.points.Length - 1)
        {
            r_Mangkukulam.gameObject.SetActive(false);
            r_Mangkukulam.pointIndex = 0;
            r_Mangkukulam.target = Waypoints.points[r_Mangkukulam.pointIndex];
            return;
        }
        r_Mangkukulam.pointIndex++;
        r_Mangkukulam.target = Waypoints.points[r_Mangkukulam.pointIndex];
    }



}
