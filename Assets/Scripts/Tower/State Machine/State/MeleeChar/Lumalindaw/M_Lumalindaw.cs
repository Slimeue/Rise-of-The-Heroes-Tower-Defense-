using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Lumalindaw : MeleeCharacterEntity
{
    public AnimationHandler animationHandler;

    public M_Lumalindaw_attack attackState { get; private set; }
    public M_Lumalindaw_death deathState { get; private set; }
    public M_Lumalindaw_Idle idleState { get; private set; }
    public M_Lumalindaw_skill skillState { get; private set; }

    const string TOWER_IDLE = "idle";
    const string TOWER_ATTACK = "attack";
    const string TOWER_DEATH = "death";
    const string TOWER_SKILL = "skill";

    public override void Awake()
    {

        base.Awake();
        attackState = new M_Lumalindaw_attack(characterStateMachine, TOWER_ATTACK, this, this);
        idleState = new M_Lumalindaw_Idle(characterStateMachine, TOWER_IDLE, this, this);
        deathState = new M_Lumalindaw_death(characterStateMachine, TOWER_DEATH, this, this);
        skillState = new M_Lumalindaw_skill(characterStateMachine, TOWER_SKILL, this, this);
        anim = GetComponent<Animator>();
        animationHandler = GetComponent<AnimationHandler>();

    }

    public override void Update()
    {
        base.Update();
        radius = characterData.range;
    }

    private void Start()
    {
        characterStateMachine.Initialize(idleState);
        isDead = false;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }


}
