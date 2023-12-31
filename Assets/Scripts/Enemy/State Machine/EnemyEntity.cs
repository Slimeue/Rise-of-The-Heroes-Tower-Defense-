using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyEntity : MonoBehaviour
{
    public StateMachine stateMachine;
    public EnemiesData enemiesData;
    public GameObject currentTarget;
    public Animator anim;
    public Slider healthBar;

    [HideInInspector]
    public SoundsPlayTrack soundsPlayTrack;

    //TargetWaypoint
    public Transform target;

    protected BaseState baseState;

    public AnimationHandler animationHandler;

    #region MOVEMENT
    public float speed;
    public int pointIndex = 0;
    #endregion

    [HideInInspector]
    public float radius;


    //debuff
    public bool isDamageTakenIncrease;
    public float damageMultiplier;
    public float debuffDuration;

    public float timeSlow;
    public bool slowed;

    public bool enemyAttackFinished;

    public float currentHealth;
    public float baseArmor { get; private set; }



    public virtual void Awake()
    {
        currentHealth = enemiesData.maxHp;
        baseArmor = enemiesData.baseArmor;
        speed = enemiesData.moveSpeed;
        stateMachine = new StateMachine();
        radius = enemiesData.attackRange;
        soundsPlayTrack = GetComponent<SoundsPlayTrack>();

    }
    private void Start()
    {
        target = Waypoints.points[0];
    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate(stateMachine);

        if (isDamageTakenIncrease)
        {
            debuffDuration -= Time.deltaTime;
            if (debuffDuration <= 0f)
            {
                isDamageTakenIncrease = false;
            }
        }

    }

    private void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate(stateMachine);
        Debug.Log("FixedUpdated");
    }

    public virtual void PlayAnim(string animBoolName)
    {
        anim.Play(animBoolName);
    }

    public virtual void HealthBarTracker()
    {
        float normalizedHealth = currentHealth / enemiesData.maxHp;
        healthBar.value = normalizedHealth;
    }




}
