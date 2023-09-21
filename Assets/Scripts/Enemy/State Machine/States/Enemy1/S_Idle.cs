using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Idle : BaseState
{
    public float idleTime = 1f;
    private EnemySM _enemySM;

    public S_Idle(EnemySM stateMachine, string animBoolName) : base(animBoolName, stateMachine)
    {
        _enemySM = (EnemySM)stateMachine;
        this.animBoolName = animBoolName;
    }

    public override void Enter(StateMachine stateMachine)
    {
        base.Enter(_enemySM);
        Debug.Log(animBoolName);
    }

    public override void Exit(StateMachine stateMachine)
    {
        base.Exit(_enemySM);
    }

    public override void LogicUpdate(StateMachine stateMachine)
    {
        base.LogicUpdate(_enemySM);
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
        base.OnTriggerEnter(_enemySM, collider);
    }
}
