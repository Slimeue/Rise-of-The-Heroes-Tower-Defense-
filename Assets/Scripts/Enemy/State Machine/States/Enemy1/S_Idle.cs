using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Idle : BaseState
{
    public float idleTime = 1f;
    private EnemySM _enemySM;

    public S_Idle(EnemySM enemySMstateMachine, string animBoolName, StateMachine stateMachine) : base(animBoolName, stateMachine)
    {
        _enemySM = (EnemySM)enemySMstateMachine;
        this.animBoolName = animBoolName;
    }

    public override void Enter(StateMachine stateMachine)
    {
        base.Enter(stateMachine);
        Debug.Log(animBoolName);
    }

    public override void Exit(StateMachine stateMachine)
    {
        base.Exit(stateMachine);
    }

    public override void LogicUpdate(StateMachine stateMachine)
    {
        base.LogicUpdate(stateMachine);
        /*transition to movingState when it moves*/
        if (idleTime > 0)
        {
            idleTime -= Time.deltaTime;
        }
        else
        {
            Debug.Log("Transitioning to moveState");
            stateMachine.ChangeState(_enemySM.movingState);
        }
    }

    public override void OnTriggerEnter(StateMachine stateMachine, Collider collider)
    {
        base.OnTriggerEnter(stateMachine, collider);
    }
}
