using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Moving : BaseState
{
    private EnemySM _enemySM;





    public S_Moving(EnemySM stateMachine, string animBoolName) : base(animBoolName, stateMachine)
    {
        _enemySM = (EnemySM)stateMachine;
        this.animBoolName = animBoolName;


    }

    public override void Enter(StateMachine stateMachine)
    {
        base.Enter(_enemySM);
        // _enemySM.anim.SetBool(animBoolName, true);

    }

    public override void LogicUpdate(StateMachine stateMachine)
    {
        base.LogicUpdate(_enemySM);
        _enemySM.anim.SetBool(animBoolName, true);
        /*if enemy goes still transition to idleState*/
        FollowPath();
        if (_enemySM.isInFront)
        {
            stateMachine.ChangeState(_enemySM.attackState);
        }


    }

    public override void Exit(StateMachine stateMachine)
    {
        base.Exit(_enemySM);
        _enemySM.anim.SetBool(animBoolName, false);
    }

    public override void DoChecks()
    {
        base.DoChecks();
        _enemySM.isInFront = _enemySM.OnEnemyFrontCheck();
    }




    public override void OnTriggerEnter(StateMachine stateMachine, Collider collider)
    {
        base.OnTriggerEnter(_enemySM, collider);

    }



    public void FollowPath()
    {

        _enemySM.gameObject.transform.LookAt(_enemySM.target);
        Vector3 dir = _enemySM.target.position - _enemySM.gameObject.transform.position;
        _enemySM.gameObject.transform.Translate(dir.normalized * _enemySM.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(_enemySM.transform.position, _enemySM.target.position) <= 0.2f)
        {
            GetNextWayPoint();
        }
    }

    void GetNextWayPoint()
    {
        if (_enemySM.pointIndex >= Waypoints.points.Length - 1)
        {
            _enemySM.gameObject.SetActive(false);
            _enemySM.pointIndex = 0;
            _enemySM.target = Waypoints.points[_enemySM.pointIndex];
            return;
        }
        _enemySM.pointIndex++;
        _enemySM.target = Waypoints.points[_enemySM.pointIndex];
    }

}
