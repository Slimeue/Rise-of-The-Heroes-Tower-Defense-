using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharEntity : MonoBehaviour
{

    [SerializeField] public GameObject ground;
    [SerializeField] public TowerManager towerManager;
    public CharacterStateMachine characterStateMachine;
    public CharacterBaseState characterBaseState;
    public AnimationHandler animationHandler;
    public Slider healthBar;

    //Components
    [Header("Components")]
    public Animator anim;
    public CharacterData characterData;
    [Space(5)]
    //Stats
    [Header("Stats")]
    [Space(5)]
    public float maxHp;
    public float currentHealth;
    public float currentMana;
    public float baseArmor;
    public float damageValue;

    [HideInInspector]
    public float radius;

    [HideInInspector]
    public float _rotationSpeed;
    public bool isAttackFinished;

    IDataService dataService = new JsonDataService();
    string saveDataPath = "/character-data"; //TODO static reference
    private CharacterStats characterStats = new CharacterStats();



    // Start is called before the first frame update
    public virtual void Awake()
    {
        currentMana = characterData.mana;
        _rotationSpeed = characterData.rotationSpeed;
        LoadCharStats();

        towerManager = FindObjectOfType<TowerManager>();
        characterStateMachine = new CharacterStateMachine();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        characterStateMachine.currentState.LogicUpdate();
    }

    public void LoadCharStats()
    {
        string newSaveDataPath = $"{saveDataPath}-{characterData.charName}.json";

        CharacterStats charStatsData = dataService.LoadData<CharacterStats>(newSaveDataPath, false);

        try
        {
            CharacterStats.charStats charData = charStatsData.stats[characterData.name];

            maxHp = charData.hp;
            currentHealth = charData.hp;
            baseArmor = charData.armor;
            damageValue = charData.damage;

        }
        catch (Exception e)
        {
            Debug.Log(e.Message + " " + e.StackTrace);

        }


    }

    public virtual void PlayAnim(string animBoolName)
    {
        anim.Play(animBoolName);
    }

    public virtual void HealthBarTracker()
    {
        float normalizedHealth = currentHealth / maxHp;
        healthBar.value = normalizedHealth;
    }

    public void CheckData()
    {

    }

}
