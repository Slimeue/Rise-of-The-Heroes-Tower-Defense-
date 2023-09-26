using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateMachine
{

    public BaseState currentState { get; private set; }

    public void ChangeState(BaseState newState)
    {
        currentState.Exit(this);
        currentState = newState;
        currentState.Enter(this);

    }

    public void Initialize(BaseState startingState)
    {
        currentState = startingState;
        if (currentState != null)
        {
            currentState.Enter(this);
        }
    }

    protected virtual BaseState GetInitialize()
    {
        return null;
    }




}
