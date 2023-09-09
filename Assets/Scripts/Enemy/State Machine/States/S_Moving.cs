using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Moving : BaseState
{
    private EnemySM _enemySM;
    public S_Moving(EnemySM stateMachine) : base("Moving", stateMachine)
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
        /*if enemy goes still transition to idleState*/
        FollowPath();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(_enemySM.idleState);
        }



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