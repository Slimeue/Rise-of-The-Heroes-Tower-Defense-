using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Idle : BaseState
{
    public float idleTime = 1f;
    private EnemySM _enemySM;

    public S_Idle(EnemySM stateMachine) : base("Idle", stateMachine)
    {
        _enemySM = (EnemySM)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
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


}
