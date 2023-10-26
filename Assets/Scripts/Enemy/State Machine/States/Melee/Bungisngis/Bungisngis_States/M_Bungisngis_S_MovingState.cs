using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Bungisngis_S_MovingState : BaseState
{
    M_Bungisngis m_Bungisngis;
    float movingTime;


    public M_Bungisngis_S_MovingState(StateMachine stateMachine, string animBoolName, EnemyEntity enemyEntity, M_Bungisngis m_Bungisngis)
    : base(animBoolName, stateMachine)
    {
        this.animBoolName = animBoolName;
        this.m_Bungisngis = m_Bungisngis;
    }

    public override void Enter(StateMachine stateMachine)
    {
        base.Enter(stateMachine);
        movingTime = Random.Range(1f, 5f);
        m_Bungisngis.PlayAnim(animBoolName);
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
        m_Bungisngis.isInFront = m_Bungisngis.OnEnemyFrontCheck();
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
        if (m_Bungisngis.currentHealth <= 0f)
        {
            m_Bungisngis.stateMachine.ChangeState(m_Bungisngis.deathState);
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
            m_Bungisngis.stateMachine.ChangeState(m_Bungisngis.idleState);
        }
    }

    private void AttackTransition()
    {
        if (!m_Bungisngis.isInFront)
        {
            return;
        }
        m_Bungisngis.stateMachine.ChangeState(m_Bungisngis.attackState);
    }

    public void FollowPath()
    {
        m_Bungisngis.gameObject.transform.LookAt(m_Bungisngis.target);

        Vector3 dir = m_Bungisngis.target.position - m_Bungisngis.gameObject.transform.position;
        m_Bungisngis.gameObject.transform.Translate(dir.normalized * m_Bungisngis.speed * Time.deltaTime, Space.World);


        if (Vector3.Distance(m_Bungisngis.transform.position, m_Bungisngis.target.position) <= 0.2f)
        {
            GetNextWayPoint();
        }
    }

    void GetNextWayPoint()
    {
        if (m_Bungisngis.pointIndex >= Waypoints.points.Length - 1)
        {
            m_Bungisngis.gameObject.SetActive(false);
            m_Bungisngis.pointIndex = 0;
            m_Bungisngis.target = Waypoints.points[m_Bungisngis.pointIndex];
            return;
        }
        m_Bungisngis.pointIndex++;
        m_Bungisngis.target = Waypoints.points[m_Bungisngis.pointIndex];
    }

    #endregion

}
