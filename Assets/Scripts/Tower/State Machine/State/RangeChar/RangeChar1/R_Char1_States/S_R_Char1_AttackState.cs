using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_R_Char1_AttackState : CharacterBaseState
{

    R_Char1 characterSM;

    public S_R_Char1_AttackState(CharacterStateMachine characterStateMachine, string animBoolName, CharEntity entity, R_Char1 characterSM)
    : base(animBoolName, characterStateMachine)
    {
        this.characterSM = characterSM;
        this.animBoolName = animBoolName;
    }
}
