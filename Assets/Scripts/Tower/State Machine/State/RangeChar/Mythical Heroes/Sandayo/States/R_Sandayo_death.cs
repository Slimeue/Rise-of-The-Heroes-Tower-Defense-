using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_Sandayo_death : CharacterBaseState
{

    Sandayo r_Sandayo;


    public R_Sandayo_death(CharacterStateMachine characterStateMachine, string animBoolName, CharEntity charEntity, Sandayo r_Sandayo)
    : base(animBoolName, characterStateMachine)
    {
        this.r_Sandayo = r_Sandayo;
        this.animBoolName = animBoolName;
    }

    #region Override Methods
    public override void Enter()
    {
        base.Enter();
        r_Sandayo.animationHandler.OnDeathFinish += r_Sandayo.DestroyGameObject;
        r_Sandayo.animationHandler.OnDeathFinish += r_Sandayo.TowerHolderEnabler;
        r_Sandayo.isDead = true;
        r_Sandayo.PlayAnim(animBoolName);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();


    }

    public override void Exit()
    {
        base.Exit();
        r_Sandayo.animationHandler.OnDeathFinish -= r_Sandayo.DestroyGameObject;
    }

    public override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }
    #endregion





}
