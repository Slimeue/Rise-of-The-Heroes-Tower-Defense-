using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateMachine : MonoBehaviour
{

    protected BaseState currentState;

    // Start is called before the first frame update
    void Start()
    {
        currentState = GetInitialState();
        if (currentState != null)
        {
            currentState.Enter(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
        {
            currentState.LogicUpdate(this);
        }
        else
        {
            Debug.Log("No State");
        }
    }

    public void ChangeState(BaseState newState)
    {
        currentState.Exit(this);
        currentState = newState;
        currentState.Enter(this);
    }

    protected virtual BaseState GetInitialState()
    {
        return null;
    }




}
