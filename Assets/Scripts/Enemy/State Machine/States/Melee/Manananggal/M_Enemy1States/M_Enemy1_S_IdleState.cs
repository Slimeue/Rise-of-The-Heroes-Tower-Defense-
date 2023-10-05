using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Enemy1_S_IdleState : BaseState
{
    public float idleTime;

    M_Enemy1 m_Enemy1SM;

    public M_Enemy1_S_IdleState(StateMachine stateMachine, string animBoolName, EnemyEntity enemyEntity, M_Enemy1 m_Enemy1SM)
     : base(animBoolName, stateMachine)
    {
        this.m_Enemy1SM = m_Enemy1SM;
        this.animBoolName = animBoolName;
    }

    #region override Methods
    public override void Enter(StateMachine stateMachine)
    {
        base.Enter(stateMachine);
        Debug.Log("HelloFrom Manananggal idle state");
        idleTime = 1f;
        m_Enemy1SM.PlayAnim(animBoolName);
    }

    public override void Exit(StateMachine stateMachine)
    {
        base.Exit(stateMachine);
    }

    public override void LogicUpdate(StateMachine stateMachine)
    {
        base.LogicUpdate(stateMachine);
        /*transition to movingState when it moves*/
        StopIdle(stateMachine);
    }

    public override void OnTriggerEnter(StateMachine stateMachine, Collider collider)
    {
        base.OnTriggerEnter(stateMachine, collider);
    }
    #endregion



    #region METOHDS
    private void StopIdle(StateMachine stateMachine)
    {
        if (idleTime > 0)
        {
            idleTime -= Time.deltaTime;
        }
        else
        {
            Debug.Log("Transitioning to moveState");
            stateMachine.ChangeState(m_Enemy1SM.movingState);
        }
    }
    #endregion
}
