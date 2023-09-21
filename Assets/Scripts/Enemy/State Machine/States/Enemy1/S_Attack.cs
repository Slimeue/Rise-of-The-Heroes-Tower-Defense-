using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char1_S_Attack : BaseState
{
    EnemySM _enemySM;

    public Char1_S_Attack(EnemySM stateMachine, string animBoolName) : base(animBoolName, stateMachine)
    {
        _enemySM = (EnemySM)stateMachine;
        this.animBoolName = animBoolName;
    }



    public override void Enter(StateMachine stateMachine)
    {
        base.Enter(_enemySM);
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
        base.Exit(_enemySM);
        _enemySM.anim.SetBool(animBoolName, false);
    }


    public override void LogicUpdate(StateMachine stateMachine)
    {
        base.LogicUpdate(_enemySM);
        _enemySM.anim.SetBool(animBoolName, true);
        if (!_enemySM.isInFront)
        {
            stateMachine.ChangeState(_enemySM.movingState);
        }
    }

    public override void OnTriggerEnter(StateMachine stateMachine, Collider collider)
    {
        base.OnTriggerEnter(_enemySM, collider);
        IDamageable tower = collider.GetComponent<IDamageable>();
        if (tower != null)
        {
            Debug.Log("Hit!!");
            tower.Damage(_enemySM.enemiesData.dmgValue);
        }
    }
}
