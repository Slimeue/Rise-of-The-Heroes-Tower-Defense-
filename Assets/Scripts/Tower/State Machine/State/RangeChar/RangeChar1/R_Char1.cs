using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_Char1 : RangeCharacterEntity
{
    public S_R_Char1_IdleState idleState { get; private set; }
    public S_R_Char1_SkillState skillState { get; private set; }
    public S_R_Char1_AttackState attackState { get; private set; }
    public override void Awake()
    {
        base.Awake();

        idleState = new S_R_Char1_IdleState(characterStateMachine, "idle", this, this);
        skillState = new S_R_Char1_SkillState(characterStateMachine, "skill", this, this);
        attackState = new S_R_Char1_AttackState(characterStateMachine, "attack", this, this);
    }

    private void Start()
    {
        characterStateMachine.Initialize(idleState);
    }
}
