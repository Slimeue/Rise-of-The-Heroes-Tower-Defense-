using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Kapre_S_MovingState : BaseState
{
    M_Kapre m_Kapre;
    float movingTime;

    public M_Kapre_S_MovingState(StateMachine stateMachine, string animBoolName, EnemyEntity enemyEntity, M_Kapre m_Kapre)
    : base(animBoolName, stateMachine)
    {
        this.animBoolName = animBoolName;
        this.m_Kapre = m_Kapre;
    }

    #region override Methods
    public override void Enter(StateMachine stateMachine)
    {
        base.Enter(stateMachine);
        movingTime = 5f;
        Debug.Log("Hello From Kapre moving state");
        m_Kapre.PlayAnim(animBoolName);
    }

    public override void LogicUpdate(StateMachine stateMachine)
    {
        base.LogicUpdate(stateMachine);
        // m_Kapre.anim.Play(animBoolName);
        FollowPath();
        StartIdle();
        AttackTransition();
        ToDeathState();
    }


    public override void DoChecks()
    {
        base.DoChecks();
        m_Kapre.isInFront = m_Kapre.OnEnemyFrontCheck();
    }

    public override void OnTriggerEnter(StateMachine stateMachine, Collider collider)
    {
        base.OnTriggerEnter(stateMachine, collider);
    }

    public override void Exit(StateMachine stateMachine)
    {
        base.Exit(stateMachine);
    }
    #endregion







    #region METHODS

    void ToDeathState()
    {
        if (m_Kapre.currentHealth <= 0f)
        {
            m_Kapre.stateMachine.ChangeState(m_Kapre.deathState);
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
            m_Kapre.stateMachine.ChangeState(m_Kapre.idleState);
        }
    }

    private void AttackTransition()
    {
        if (!m_Kapre.isInFront)
        {
            return;
        }
        m_Kapre.stateMachine.ChangeState(m_Kapre.attackState);
    }

    public void FollowPath()
    {
        m_Kapre.gameObject.transform.LookAt(m_Kapre.target);

        Vector3 dir = m_Kapre.target.position - m_Kapre.gameObject.transform.position;
        m_Kapre.gameObject.transform.Translate(dir.normalized * m_Kapre.speed * Time.deltaTime, Space.World);


        if (Vector3.Distance(m_Kapre.transform.position, m_Kapre.target.position) <= 0.2f)
        {
            GetNextWayPoint();
        }
    }

    void GetNextWayPoint()
    {
        if (m_Kapre.pointIndex >= Waypoints.points.Length - 1)
        {
            m_Kapre.gameObject.SetActive(false);
            m_Kapre.pointIndex = 0;
            m_Kapre.target = Waypoints.points[m_Kapre.pointIndex];
            return;
        }
        m_Kapre.pointIndex++;
        m_Kapre.target = Waypoints.points[m_Kapre.pointIndex];
    }

    #endregion

}
