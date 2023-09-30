using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharEntity : MonoBehaviour
{

    public CharacterStateMachine characterStateMachine;

    //Components
    public Animator anim;
    public CharacterData characterData;

    //Stats
    public float currentHealth;
    public float currentMana;
    public float baseArmor;




    // Start is called before the first frame update
    public virtual void Awake()
    {
        currentHealth = characterData.maxHp;
        currentMana = characterData.mana;
        baseArmor = characterData.baseArmor;


        characterStateMachine = new CharacterStateMachine();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        characterStateMachine.currentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {

    }


}
