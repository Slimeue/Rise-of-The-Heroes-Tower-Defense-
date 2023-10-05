using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharEntity : MonoBehaviour
{

    public CharacterStateMachine characterStateMachine;
    public CharacterBaseState characterBaseState;
    public AnimationHandler animationHandler;
    //Components
    [Header("Components")]
    public Animator anim;
    public CharacterData characterData;
    [Space(5)]
    //Stats
    [Header("Stats")]
    [Space(5)]
    public float currentHealth;
    public float currentMana;
    public float baseArmor;

    [HideInInspector]
    public float radius;

    [HideInInspector]
    public float _rotationSpeed;



    // Start is called before the first frame update
    public virtual void Awake()
    {
        currentHealth = characterData.maxHp;
        currentMana = characterData.mana;
        baseArmor = characterData.baseArmor;
        _rotationSpeed = characterData.rotationSpeed;

        characterStateMachine = new CharacterStateMachine();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        characterStateMachine.currentState.LogicUpdate();
    }

    public virtual void PlayAnim(string animBoolName)
    {
        anim.Play(animBoolName);
    }


}
