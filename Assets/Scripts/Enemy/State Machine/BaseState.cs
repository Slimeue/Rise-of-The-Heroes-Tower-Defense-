using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseState
{
    protected StateMachine stateMachine;
    protected string animBoolName;

    public BaseState(string animBoolName, StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    protected BaseState(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public BaseState()
    {
    }

    public virtual void Enter(StateMachine stateMachine)
    {
        DoChecks();
    }
    public virtual void LogicUpdate(StateMachine stateMachine) { DoChecks(); }
    public virtual void Exit(StateMachine stateMachine) { }
    public virtual void OnTriggerEnter(StateMachine stateMachine, Collider collider) { }
    public virtual void DoChecks()
    {

    }

}
