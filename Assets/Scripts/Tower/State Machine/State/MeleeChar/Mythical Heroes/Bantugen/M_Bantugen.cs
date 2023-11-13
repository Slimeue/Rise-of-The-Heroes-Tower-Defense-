using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class M_Bantugen : MeleeCharacterEntity, IDamageable, IBuffable, IDeletable, IPointerClickHandler
{
    public GameObject ability;
    public float skillCd;
    public bool skillFinished;
    public bool skillIsActivated;
    float skillDuration = 5f;

    public M_Bantguen_idle idleState { get; private set; }
    public M_Bantugen_attack attackState { get; private set; }
    public M_Bantugen_death deathState { get; private set; }
    public M_Bantugen_skill skillState { get; private set; }

    const string TOWER_IDLE = "idle";
    const string TOWER_ATTACK = "attack";
    const string TOWER_DEATH = "death";
    const string TOWER_SKILL = "skill";


    public override void Awake()
    {

        base.Awake();
        skillCd = characterData.skillCooldown;
        attackState = new M_Bantugen_attack(characterStateMachine, TOWER_ATTACK, this, this);
        idleState = new M_Bantguen_idle(characterStateMachine, TOWER_IDLE, this, this);
        deathState = new M_Bantugen_death(characterStateMachine, TOWER_DEATH, this, this);
        skillState = new M_Bantugen_skill(characterStateMachine, TOWER_SKILL, this, this);
        anim = GetComponent<Animator>();
        animationHandler = GetComponent<AnimationHandler>();

    }

    // Start is called before the first frame update
    void Start()
    {
        characterStateMachine.Initialize(idleState);
        isDead = false;
        ground = towerManager.canvasEnabler.gameObject;
        soundsPlayTrack.Play("Voice1");


    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        radius = characterData.range;
        RefreshChar();
        HealthBarTracker();
        Debug.Log(characterStateMachine.currentState);
        if (skillFinished)
        {
            skillIsActivated = true;
            //startCooldown
            skillCd -= Time.deltaTime;
            if (skillCd <= 0f)
            {
                skillFinished = false;
                skillCd = characterData.skillCooldown;
            }
        }
        // GameObject rangeIndicator = Instantiate(TowerRangeIndicator, transform.position, Quaternion.identity);

        SkillCounter();
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
        Debug.Log(totalDamage);
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
                Debug.Log("TowerEnabling");
                _towerTesting.gameObject.SetActive(true);
                _towerTesting.isDead = true;
                _towerTesting._placed = false;
            }
        }

    }

    void SkillCounter()
    {
        if (skillIsActivated)
        {
            skillDuration -= Time.deltaTime;
            if (skillDuration <= 0f)
            {
                skillDuration = 5f;
                baseArmor = characterData.baseArmor;
                ability.SetActive(false);
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

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("CLicked");
        towerManager.DeleteState(gameObject, characterData, damageValue, baseArmor, currentHealth);
    }

    public void DeleteChar()
    {
        DestroyGameObject();
        TowerHolderEnabler();
    }



    #endregion



}
