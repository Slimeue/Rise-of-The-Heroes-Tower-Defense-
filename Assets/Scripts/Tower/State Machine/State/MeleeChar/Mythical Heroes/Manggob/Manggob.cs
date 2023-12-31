using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Manggob : MeleeCharacterEntity, IDamageable, IBuffable, IDeletable, IPointerClickHandler, IRangeSoundable
{

    public GameObject skillHolder;
    public float skillCd;
    public float skillDuration = 5f;
    public bool skillFinished;

    public bool skillDur;




    public M_Manggob_attack attackState { get; private set; }
    public M_Manggob_death deathState { get; private set; }
    public M_Manggob_idle idleState { get; private set; }
    public M_Manggob_skill skillState { get; private set; }

    const string TOWER_IDLE = "idle";
    const string TOWER_ATTACK = "attack";
    const string TOWER_DEATH = "death";
    const string TOWER_SKILL = "skill";

    public override void Awake()
    {

        base.Awake();
        skillHolder.SetActive(false);
        skillCd = characterData.skillCooldown;
        attackState = new M_Manggob_attack(characterStateMachine, TOWER_ATTACK, this, this);
        idleState = new M_Manggob_idle(characterStateMachine, TOWER_IDLE, this, this);
        deathState = new M_Manggob_death(characterStateMachine, TOWER_DEATH, this, this);
        skillState = new M_Manggob_skill(characterStateMachine, TOWER_SKILL, this, this);
        anim = GetComponent<Animator>();
        animationHandler = GetComponent<AnimationHandler>();

    }

    public override void Update()
    {
        base.Update();
        radius = characterData.range;
        RefreshChar();
        HealthBarTracker();

        if (skillFinished)
        {

            SkillDurationFinished();
            if (skillDur)
            {
                skillCd -= Time.deltaTime;
                if (skillCd <= 0f)
                {
                    skillFinished = false;
                    skillCd = characterData.skillCooldown;
                    skillDur = false;
                }
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

    void SkillDurationFinished()
    {

        if (skillDuration <= 0f)
        {
            skillHolder.SetActive(false);
            skillDuration = 5f;
            skillDur = true;
        }
        else
        {
            skillDuration -= Time.deltaTime;
        }
    }



    public void Slowed(float slowAmount, float time) { }

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
        Debug.Log("Clicked");
        towerManager.DeleteState(gameObject, characterData, damageValue, baseArmor, currentHealth);
    }

    public void DeleteChar()
    {
        DestroyGameObject();
        TowerHolderEnabler();
    }

    public void PlayRangeHitSFX(string sfx)
    {
        soundsPlayTrack.Play(sfx);
    }
    #endregion

}
