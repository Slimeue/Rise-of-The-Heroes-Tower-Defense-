using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Enemy1_S_MovingState : BaseState
{

    M_Enemy1 m_Enemy1SM;

    public M_Enemy1_S_MovingState(StateMachine stateMachine, string animBoolName, EnemyEntity enemyEntity, M_Enemy1 m_Enemy1SM)
       : base(animBoolName, stateMachine)
    {
        this.m_Enemy1SM = m_Enemy1SM;
        this.animBoolName = animBoolName;
    }


    public override void Enter(StateMachine stateMachine)
    {
        base.Enter(stateMachine);
        Debug.Log("Hello From Manananggal Moving state");
        m_Enemy1SM.PlayAnim(animBoolName);

    }

    public override void LogicUpdate(StateMachine stateMachine)
    {
        base.LogicUpdate(stateMachine);
        /*if enemy goes still transition to idleState*/
        FollowPath();
        CheckFront(stateMachine);
    }



    public override void Exit(StateMachine stateMachine)
    {
        base.Exit(stateMachine);
    }

    public override void DoChecks()
    {
        base.DoChecks();
        m_Enemy1SM.isInFront = m_Enemy1SM.OnEnemyFrontCheck();
    }

    public override void OnTriggerEnter(StateMachine stateMachine, Collider collider)
    {
        base.OnTriggerEnter(stateMachine, collider);
    }


    #region METHODS

    #region Checks

    private void CheckFront(StateMachine stateMachine)
    {
        if (m_Enemy1SM.isInFront)
        {
            stateMachine.ChangeState(m_Enemy1SM.attackState);
        }
    }
    #endregion

    public void FollowPath()
    {
        m_Enemy1SM.gameObject.transform.LookAt(m_Enemy1SM.target);

        Vector3 dir = m_Enemy1SM.target.position - m_Enemy1SM.gameObject.transform.position;
        m_Enemy1SM.gameObject.transform.Translate(dir.normalized * m_Enemy1SM.speed * Time.deltaTime, Space.World);


        if (Vector3.Distance(m_Enemy1SM.transform.position, m_Enemy1SM.target.position) <= 0.2f)
        {
            GetNextWayPoint();
        }
    }

    void GetNextWayPoint()
    {
        if (m_Enemy1SM.pointIndex >= Waypoints.points.Length - 1)
        {
            m_Enemy1SM.gameObject.SetActive(false);
            m_Enemy1SM.pointIndex = 0;
            m_Enemy1SM.target = Waypoints.points[m_Enemy1SM.pointIndex];
            return;
        }
        m_Enemy1SM.pointIndex++;
        m_Enemy1SM.target = Waypoints.points[m_Enemy1SM.pointIndex];
    }

    #endregion




}
