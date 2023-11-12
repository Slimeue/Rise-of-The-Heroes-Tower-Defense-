using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Charaters/Towers", menuName = "Character/Character Data")]
public class CharacterData : ScriptableObject
{

    [SerializeField] public Sprite charTypeArt;
    [SerializeField] public Sprite charArtWork;

    [SerializeField] public CharacterSkill skillData;

    [SerializeField] public LayerMask _layerMask;
    [SerializeField] public string _platformTag;

    [SerializeField] public GameObject previewChar;
    [SerializeField] public GameObject charObject;
    [SerializeField] public GameObject projectile;

    [Space(5)]
    [SerializeField] public string charName;

    [Space(5)]
    [SerializeField] public float dmgValue;

    [Space(5)]
    [SerializeField] public bool charType;

    [Space(5)]
    [Header("Level")]
    [SerializeField] public int charLevel;

    [Space(5)]
    [Header("Health")]
    [SerializeField] public float maxHp;

    [Space(5)]
    [SerializeField] public float mana;

    [Space(5)]
    [Header("Defense")]
    [SerializeField] public float baseArmor;

    [Space(5)]
    [Header("Cooldowns")]
    [SerializeField] public float towerCooldown; // Tower Cooldown after dying
    [SerializeField] public float skillCooldown;

    [Header("Cost")]
    [SerializeField] public int towerCost;

    [Space(5)]
    [Header("Locator")]
    [SerializeField] public float range;
    [SerializeField] public float projectileSpeed;
    [SerializeField] public float attackSpeed;

    [Space(5)]
    [SerializeField] public float rotationSpeed;

    [Space(5)]
    [Header("Lore")]
    [TextArea(5, 10)][SerializeField] public string charLore;





}
