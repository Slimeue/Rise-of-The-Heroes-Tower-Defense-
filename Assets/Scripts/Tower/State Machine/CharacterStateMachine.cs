using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateMachine
{
    public CharacterBaseState currentState { get; private set; }

    public void ChangeState(CharacterBaseState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();

    }

    public void Initialize(CharacterBaseState startingState)
    {
        currentState = startingState;
        if (currentState != null)
        {
            currentState.Enter();
        }
    }

    protected virtual CharacterBaseState GetInitialize()
    {
        return null;
    }

}
