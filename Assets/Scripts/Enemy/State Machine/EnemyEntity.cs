using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : MonoBehaviour
{
    public StateMachine stateMachine;
    public EnemiesData enemiesData;

    public Animator anim;

    public float currentHealth { get; private set; }
    public float baseArmor { get; private set; }

    public virtual void Awake()
    {
        currentHealth = enemiesData.maxHp;
        baseArmor = enemiesData.baseArmor;

        stateMachine = new StateMachine();

    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate(stateMachine);
    }

}
