using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Valenzuela : MeleeCharacterEntity, IDamageable
{

    BaseManager baseManager;

    public M_Valenzuela_S_attackState attackState { get; private set; }
    public M_Valenzuela_S_idleState idleState { get; private set; }
    public M_Valenzuela_S_skillState skillState { get; private set; }
    public M_Valenzuela_S_recoveryState recoveryState { get; private set; }
    public M_Valenzuela_S_deathState deathState { get; private set; }


    const string HERO_IDLE = "idle";
    const string HERO_ATTACK = "attack";
    const string HERO_RECOVERY = "recovery";
    const string HERO_SKILL = "skill";
    const string HERO_DEATH = "death";


    public override void Awake()
    {
        base.Awake();
        baseManager = FindObjectOfType<BaseManager>();
        idleState = new M_Valenzuela_S_idleState(characterStateMachine, HERO_IDLE, this, this);
        attackState = new M_Valenzuela_S_attackState(characterStateMachine, HERO_ATTACK, this, this);
        skillState = new M_Valenzuela_S_skillState(characterStateMachine, HERO_SKILL, this, this);
        recoveryState = new M_Valenzuela_S_recoveryState(characterStateMachine, HERO_RECOVERY, this, this);
        deathState = new M_Valenzuela_S_deathState(characterStateMachine, HERO_DEATH, this, this);
        anim = GetComponent<Animator>();
        animationHandler = GetComponent<AnimationHandler>();

    }

    private void Start()
    {
        characterStateMachine.Initialize(idleState);
    }

    public override void Update()
    {
        base.Update();
        radius = characterData.range;
        Debug.Log(characterStateMachine.currentState);
        HealthBarTracker();
    }

    public override void HealthBarTracker()
    {
        base.HealthBarTracker();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void OnTriggerEnter(Collider collider)
    {
        characterBaseState = characterStateMachine.currentState;
        characterBaseState.OnTriggerEnter(collider);
    }

    private void OnTriggerExit(Collider other)
    {
        characterBaseState = characterStateMachine.currentState;
        characterBaseState.OnTriggerExit(other);
    }

    #region METHODS

    public void Damage(float damageAmount)
    {
        float totalDamage;
        totalDamage = damageAmount * (100 / (100 + baseArmor));
        Debug.Log("Hit " + totalDamage);

        if (characterStateMachine.currentState == recoveryState)
        {
            baseManager.currentBaseHp -= damageAmount;
            return;
        }

        currentHealth -= totalDamage;
    }

    #endregion

}
