using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_WhiteLady_S_movingState : BaseState
{
    M_WhiteLady m_WhiteLady;
    float movingTime;


    public M_WhiteLady_S_movingState(StateMachine stateMachine, string animBoolName, EnemyEntity enemyEntity, M_WhiteLady m_WhiteLady)
    : base(animBoolName, stateMachine)
    {
        this.animBoolName = animBoolName;
        this.m_WhiteLady = m_WhiteLady;
    }

    public override void Enter(StateMachine stateMachine)
    {
        base.Enter(stateMachine);
        movingTime = Random.Range(1f, 5f);
        m_WhiteLady.PlayAnim(animBoolName);
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
        m_WhiteLady.isInFront = m_WhiteLady.OnEnemyFrontCheck();
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
        if (m_WhiteLady.currentHealth <= 0f)
        {
            m_WhiteLady.stateMachine.ChangeState(m_WhiteLady.deathState);
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
            m_WhiteLady.stateMachine.ChangeState(m_WhiteLady.idleState);
        }
    }

    private void AttackTransition()
    {
        if (!m_WhiteLady.isInFront)
        {
            return;
        }
        m_WhiteLady.stateMachine.ChangeState(m_WhiteLady.attackState);
    }

    public void FollowPath()
    {
        m_WhiteLady.gameObject.transform.LookAt(m_WhiteLady.target);

        Vector3 dir = m_WhiteLady.target.position - m_WhiteLady.gameObject.transform.position;
        m_WhiteLady.gameObject.transform.Translate(dir.normalized * m_WhiteLady.speed * Time.deltaTime, Space.World);


        if (Vector3.Distance(m_WhiteLady.transform.position, m_WhiteLady.target.position) <= 0.2f)
        {
            GetNextWayPoint();
        }
    }

    void GetNextWayPoint()
    {
        if (m_WhiteLady.pointIndex >= Waypoints.points.Length - 1)
        {
            m_WhiteLady.gameObject.SetActive(false);
            m_WhiteLady.pointIndex = 0;
            m_WhiteLady.target = Waypoints.points[m_WhiteLady.pointIndex];
            return;
        }
        m_WhiteLady.pointIndex++;
        m_WhiteLady.target = Waypoints.points[m_WhiteLady.pointIndex];
    }

    #endregion

}
