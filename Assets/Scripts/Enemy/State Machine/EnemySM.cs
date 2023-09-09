using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySM : StateMachine
{
    [HideInInspector]
    public S_Idle idleState;
    [HideInInspector]
    public S_Moving movingState;

    public Transform target;

    Vector3 point;
    public float speed = 10f;
    public int pointIndex = 0;
    public float rotationSpeed = 10f;

    float detectionRadius = 2f;

    public void Awake()
    {
        target = Waypoints.points[0];
        idleState = new S_Idle(this);
        movingState = new S_Moving(this);
    }



    protected override BaseState GetInitialState()
    {
        return idleState;
    }
}
