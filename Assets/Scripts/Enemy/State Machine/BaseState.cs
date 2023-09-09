using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
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

    public virtual void Enter() { }
    public virtual void LogicUpdate() { }
    public virtual void Exit() { }
}
