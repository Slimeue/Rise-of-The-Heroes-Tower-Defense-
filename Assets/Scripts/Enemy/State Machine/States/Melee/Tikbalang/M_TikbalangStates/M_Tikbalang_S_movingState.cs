using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Tikbalang_S_movingState : BaseState
{
    M_Tikbalang m_Tikbalang;
    float movingTime;


    public M_Tikbalang_S_movingState(StateMachine stateMachine, string animBoolName, EnemyEntity enemyEntity, M_Tikbalang m_Tikbalang)
    : base(animBoolName, stateMachine)
    {
        this.animBoolName = animBoolName;
        this.m_Tikbalang = m_Tikbalang;
    }

    public override void Enter(StateMachine stateMachine)
    {
        base.Enter(stateMachine);
        movingTime = Random.Range(1f, 5f);
        m_Tikbalang.PlayAnim(animBoolName);
    }

    public override void LogicUpdate(StateMachine stateMachine)
    {
        base.LogicUpdate(stateMachine);
        FollowPath();
        StartIdle();
        AttackTransition();
        ToDeathState();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        m_Tikbalang.isInFront = m_Tikbalang.OnEnemyFrontCheck();
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
        if (m_Tikbalang.currentHealth <= 0f)
        {
            m_Tikbalang.stateMachine.ChangeState(m_Tikbalang.deathState);
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
            m_Tikbalang.stateMachine.ChangeState(m_Tikbalang.idleState);
        }
    }

    private void AttackTransition()
    {
        if (!m_Tikbalang.isInFront)
        {
            return;
        }
        m_Tikbalang.stateMachine.ChangeState(m_Tikbalang.attackState);
    }

    public void FollowPath()
    {
        m_Tikbalang.gameObject.transform.LookAt(m_Tikbalang.target);

        Vector3 dir = m_Tikbalang.target.position - m_Tikbalang.gameObject.transform.position;
        m_Tikbalang.gameObject.transform.Translate(dir.normalized * m_Tikbalang.speed * Time.deltaTime, Space.World);


        if (Vector3.Distance(m_Tikbalang.transform.position, m_Tikbalang.target.position) <= 0.2f)
        {
            GetNextWayPoint();
        }
    }

    void GetNextWayPoint()
    {
        if (m_Tikbalang.pointIndex >= Waypoints.points.Length - 1)
        {
            m_Tikbalang.gameObject.SetActive(false);
            m_Tikbalang.pointIndex = 0;
            m_Tikbalang.target = Waypoints.points[m_Tikbalang.pointIndex];
            return;
        }
        m_Tikbalang.pointIndex++;
        m_Tikbalang.target = Waypoints.points[m_Tikbalang.pointIndex];
    }

    #endregion

}
