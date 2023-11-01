using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Berberoka_S_MovingState : BaseState
{
    M_Berberoka m_Berberoka;
    float movingTime;


    public M_Berberoka_S_MovingState(StateMachine stateMachine, string animBoolName, EnemyEntity enemyEntity, M_Berberoka m_Berberoka)
    : base(animBoolName, stateMachine)
    {
        this.animBoolName = animBoolName;
        this.m_Berberoka = m_Berberoka;
    }

    public override void Enter(StateMachine stateMachine)
    {
        base.Enter(stateMachine);
        movingTime = Random.Range(1f, 5f);
        m_Berberoka.PlayAnim(animBoolName);
    }

    public override void LogicUpdate(StateMachine stateMachine)
    {
        base.LogicUpdate(stateMachine);

        StartIdle();
        AttackTransition();
        ToDeathState();
    }

    public override void PhysicsUpdate(StateMachine stateMachine)
    {
        base.PhysicsUpdate(stateMachine);
        FollowPath();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        m_Berberoka.isInFront = m_Berberoka.OnEnemyFrontCheck();
    }

    public override void OnTriggerEnter(StateMachine stateMachine, Collider collider)
    {
        base.OnTriggerEnter(stateMachine, collider);
    }

    public override void OnTriggerExit(StateMachine stateMachine, Collider collider)
    {
        base.OnTriggerExit(stateMachine, collider);
    }

    public override void Exit(StateMachine stateMachine)
    {
        base.Exit(stateMachine);
    }


    #region METHODS

    void ToDeathState()
    {
        if (m_Berberoka.currentHealth <= 0f)
        {
            m_Berberoka.stateMachine.ChangeState(m_Berberoka.deathState);
        }
    }


    private void StartIdle()
    {
        if (movingTime > 0)
        {
            movingTime -= Time.deltaTime;
        }
        else
        {
            m_Berberoka.stateMachine.ChangeState(m_Berberoka.idleState);
        }
    }

    private void AttackTransition()
    {
        if (!m_Berberoka.isInFront)
        {
            return;
        }
        m_Berberoka.stateMachine.ChangeState(m_Berberoka.attackState);
    }

    public void FollowPath()
    {
        m_Berberoka.gameObject.transform.LookAt(m_Berberoka.target);

        Vector3 dir = m_Berberoka.target.position - m_Berberoka.gameObject.transform.position;
        m_Berberoka.gameObject.transform.Translate(dir.normalized * m_Berberoka.speed * Time.deltaTime, Space.World);


        if (Vector3.Distance(m_Berberoka.transform.position, m_Berberoka.target.position) <= 0.2f)
        {
            GetNextWayPoint();
        }
    }

    void GetNextWayPoint()
    {
        if (m_Berberoka.pointIndex >= Waypoints.points.Length - 1)
        {
            m_Berberoka.gameObject.SetActive(false);
            m_Berberoka.pointIndex = 0;
            m_Berberoka.target = Waypoints.points[m_Berberoka.pointIndex];
            return;
        }
        m_Berberoka.pointIndex++;
        m_Berberoka.target = Waypoints.points[m_Berberoka.pointIndex];
    }

    #endregion


}
