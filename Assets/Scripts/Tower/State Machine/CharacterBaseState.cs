using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBaseState
{
    protected CharacterStateMachine characterStateMachine;
    protected string animBoolName;
    protected bool isAnimationFinished;

    public CharacterBaseState(string animBoolName, CharacterStateMachine characterStateMachine)
    {
        this.animBoolName = animBoolName;
        this.characterStateMachine = characterStateMachine;
    }

    public CharacterBaseState(CharacterStateMachine characterStateMachine)
    {
        this.characterStateMachine = characterStateMachine;
    }

    public CharacterBaseState() { }

    public virtual void Enter()
    {
        DoChecks();
        isAnimationFinished = false;
    }
    public virtual void LogicUpdate() { DoChecks(); }
    public virtual void Exit() { }
    public virtual void OnTriggerEnter(Collider collider) { }
    public virtual void DoChecks() { }

}
