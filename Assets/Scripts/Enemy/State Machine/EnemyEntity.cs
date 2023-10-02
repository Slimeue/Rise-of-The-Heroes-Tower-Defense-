using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : MonoBehaviour
{
    public StateMachine stateMachine;
    public EnemiesData enemiesData;

    public Animator anim;

    //TargetWaypoint
    public Transform target;

    protected BaseState baseState;

    #region MOVEMENT
    public float speed;
    public int pointIndex = 0;
    #endregion

    #region Transform



    #endregion



    [SerializeField] BoxCollider attackCollider;


    float detectionRadius = 2f;

    public float currentHealth { get; private set; }
    public float baseArmor { get; private set; }

    public virtual void Awake()
    {
        currentHealth = enemiesData.maxHp;
        baseArmor = enemiesData.baseArmor;
        speed = enemiesData.moveSpeed;
        stateMachine = new StateMachine();

    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate(stateMachine);
    }

}
