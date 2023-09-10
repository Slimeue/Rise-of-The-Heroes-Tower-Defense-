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
    #region MOVEMENT
    public float speed = 10f;
    public int pointIndex = 0;
    public float rotationSpeed = 10f;
    #endregion

    #region Transform

    public GameObject body;
    public Animator anim;

    #endregion
    float detectionRadius = 2f;

    public void Awake()
    {
        target = Waypoints.points[0];
        idleState = new S_Idle(this, "Idle");
        movingState = new S_Moving(this, "walk");

        anim = GetComponent<Animator>();

    }



    protected override BaseState GetInitialState()
    {
        return idleState;
    }
}
