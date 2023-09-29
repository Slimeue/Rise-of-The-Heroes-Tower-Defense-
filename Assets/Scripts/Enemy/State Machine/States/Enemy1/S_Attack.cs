using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Attack : BaseState
{
    EnemySM _enemySM;

    public S_Attack(EnemySM enemySMStateMachine, string animBoolName, StateMachine stateMachine) : base(animBoolName, stateMachine)
    {
        _enemySM = (EnemySM)enemySMStateMachine;
        this.animBoolName = animBoolName;
    }



    public override void Enter(StateMachine stateMachine)
    {
        base.Enter(stateMachine);
        Debug.Log("Hello From AttackState");
        // _enemySM.anim.SetBool(animBoolName, true);


    }

    public override void DoChecks()
    {
        base.DoChecks();
        _enemySM.isInFront = _enemySM.OnEnemyFrontCheck();

    }


    public override void Exit(StateMachine stateMachine)
    {
        base.Exit(stateMachine);
        _enemySM.anim.SetBool(animBoolName, false);
    }


    public override void LogicUpdate(StateMachine stateMachine)
    {
        base.LogicUpdate(stateMachine);
        _enemySM.anim.SetBool(animBoolName, true);
        if (!_enemySM.isInFront)
        {
            stateMachine.ChangeState(_enemySM.movingState);
        }
    }

    public override void OnTriggerEnter(StateMachine stateMachine, Collider collider)
    {
        base.OnTriggerEnter(stateMachine, collider);
        IDamageable tower = collider.GetComponent<IDamageable>();
        if (tower != null)
        {
            Debug.Log("Hit!!");
            tower.Damage(_enemySM.enemiesData.dmgValue);
        }
    }
}
