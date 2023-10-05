using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class S_R_Char1_IdleState : CharacterBaseState
{

    R_Char1 characterSM;


    public S_R_Char1_IdleState(CharacterStateMachine characterStateMachine, string animBoolName, CharEntity entity, R_Char1 characterSM)
    : base(animBoolName, characterStateMachine)
    {
        this.characterSM = characterSM;
        this.animBoolName = animBoolName;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Hello From R_Char1 Idle State Character!!");

    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);
    }
    public override void DoChecks()
    {
        base.DoChecks();
        if (characterSM._inRange)
        {
            characterStateMachine.ChangeState(characterSM.attackState);
        }
    }


}
