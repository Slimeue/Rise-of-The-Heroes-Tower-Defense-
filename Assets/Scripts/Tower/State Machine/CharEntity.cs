using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

    //AnimParameters
    protected const string ATTACK_SPEED = "AttackSpeed";

    //Buffs
    [HideInInspector]
    public float buffDuration;
    [HideInInspector]
    public bool isBuffed;

    // Start is called before the first frame update
    public virtual void Awake()
    {
        LoadCharStats();
        currentMana = characterData.mana;
        _rotationSpeed = characterData.rotationSpeed;


        towerManager = FindObjectOfType<TowerManager>();
        characterStateMachine = new CharacterStateMachine();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        characterStateMachine.currentState.LogicUpdate();
        CheckIsBuffed();
    }

    public void LoadCharStats()
    {
        string newSaveDataPath = $"{saveDataPath}-{characterData.charName}.json";

        string path = Application.persistentDataPath + newSaveDataPath;


        if (!File.Exists(path))
        {
            if (!characterStats.stats.ContainsKey(characterData.charName))
            {
                CharacterStats.charStats characterStatsData = new CharacterStats.charStats
                {
                    charName = characterData.charName,
                    level = characterData.charLevel,
                    experienceToNextLevel = 50,
                    hp = characterData.maxHp,
                    damage = characterData.dmgValue,
                    armor = characterData.baseArmor
                };
                characterStats.stats.Clear();
                characterStats.stats.Add(characterData.charName, characterStatsData);
                dataService.SaveData(newSaveDataPath, characterStats, false);
            }
            else
            {
                return;
            }

        }



        CharacterStats charStatsData = dataService.LoadData<CharacterStats>(newSaveDataPath, false);

        try
        {
            CharacterStats.charStats charData = charStatsData.stats[characterData.charName];

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

    public void CheckIsBuffed()
    {
        if (isBuffed)
        {
            buffDuration -= Time.deltaTime;
            if (buffDuration <= 0f)
            {
                //setback to default speed
                anim.SetFloat(ATTACK_SPEED, 1f);
                isBuffed = false;
            }
        }
    }


}
