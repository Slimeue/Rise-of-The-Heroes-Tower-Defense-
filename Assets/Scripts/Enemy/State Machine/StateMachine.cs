using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    BaseState currentState;

    // Start is called before the first frame update
    void Start()
    {
        currentState = GetInitialState();
        if (currentState != null)
        {
            currentState.Enter();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
        {
            currentState.LogicUpdate();
            Debug.Log("CurrentState: " + currentState);
        }
        else
        {
            Debug.Log("No State: " + currentState);
        }
    }

    public void ChangeState(BaseState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    protected virtual BaseState GetInitialState()
    {
        return null;
    }
}
