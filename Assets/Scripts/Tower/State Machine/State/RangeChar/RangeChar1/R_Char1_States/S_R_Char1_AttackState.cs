using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_R_Char1_AttackState : CharacterBaseState
{

    R_Char1 characterSM;


    Vector3 _targetDir;

    public S_R_Char1_AttackState(CharacterStateMachine characterStateMachine, string animBoolName, CharEntity entity, R_Char1 characterSM)
    : base(animBoolName, characterStateMachine)
    {
        this.characterSM = characterSM;
        this.animBoolName = animBoolName;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Hello From R_Char1 Attack State Character!!");

    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        FindTarget();

    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void OnTriggerEnter()
    {
        base.OnTriggerEnter();
    }
    public override void DoChecks()
    {
        base.DoChecks();
    }

    private void FindTarget()
    {
        if (characterSM.target != null)
        {
            Vector3 targetDir = characterSM.target.position - characterSM.transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDir);
            characterSM.transform.rotation = Quaternion.Slerp(characterSM.transform.rotation, targetRotation, characterSM._rotationSpeed * Time.deltaTime);
        }
    }
}
