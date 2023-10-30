using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Lumalindaw : MeleeCharacterEntity, IDamageable, IBuffable
{

    public GameObject abilityHolder;
    public float skillCd;
    public bool skillFinished;


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
        skillCd = characterData.skillCooldown;
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
        RefreshChar();
        HealthBarTracker();
        Debug.Log(characterStateMachine.currentState);
        //skillCd
        if (skillFinished)
        {
            //startCooldown
            skillCd -= Time.deltaTime;
            if (skillCd <= 0f)
            {
                skillFinished = false;
                skillCd = characterData.skillCooldown;
            }
        }
    }

    private void Start()
    {
        characterStateMachine.Initialize(idleState);
        isDead = false;
        ground = towerManager.canvasEnabler.gameObject;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void OnDisable()
    {
        animationHandler.OnDeathFinish -= DestroyGameObject;
        animationHandler.OnDeathFinish -= TowerHolderEnabler;
    }

    private void OnTriggerEnter(Collider other)
    {
        characterBaseState = characterStateMachine.currentState;
        characterBaseState.OnTriggerEnter(other);
    }


    private void OnTriggerExit(Collider other)
    {
        characterBaseState = characterStateMachine.currentState;
        characterBaseState.OnTriggerExit(other);
    }

    public override void HealthBarTracker()
    {
        base.HealthBarTracker();
    }



    #region Methods

    public void Damage(float damageAmount)
    {
        float totalDamage;

        totalDamage = damageAmount * (100 / (100 + baseArmor));
        currentHealth -= totalDamage;
    }

    public void DestroyGameObject()
    {
        CanvasEnabler canvasEnabler = ground.GetComponent<CanvasEnabler>();
        canvasEnabler.isPlaceable = true;
        Destroy(gameObject);
    }

    private void RefreshChar()
    {
        foreach (TowerHolder _towerTesting in towerTesting)
        {
            if (characterData == _towerTesting.characterData)
            {
                _towerTesting.isDead = false;
                _towerTesting._charCooldown = characterData.towerCooldown;
            }
        }
    }

    public void TowerHolderEnabler()
    {
        foreach (TowerHolder _towerTesting in towerTesting)
        {
            if (characterData == _towerTesting.characterData)
            {
                _towerTesting.gameObject.SetActive(true);
                _towerTesting.isDead = true;
                _towerTesting._placed = false;
            }
        }
    }

    public void Slowed(float slowAmount, float time)
    {
    }

    public void AttackSpeedBuff(float percentage, float duration)
    {
        float defaultAttackSpeed = anim.GetFloat(ATTACK_SPEED);

        float buffAttackSpeed = defaultAttackSpeed *= percentage;

        anim.SetFloat(ATTACK_SPEED, buffAttackSpeed);

        isBuffed = true;
        buffDuration = duration;
    }

    public void AttackBuff(float percentage, float duration)
    {
        damageValue *= percentage;

        isBuffed = true;
        buffDuration = duration;

    }

    #endregion


}
