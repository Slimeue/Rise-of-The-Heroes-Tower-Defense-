using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Charaters/Towers", menuName = "Character/Character Data")]
public class CharacterData : ScriptableObject
{

    [SerializeField] public LayerMask _layerMask;
    [SerializeField] public string _platformTag;

    [Space(5)]
    [SerializeField] public float dmgValue;

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

    [Header("Cost")]
    [SerializeField] public int towerCost;

    [Space(5)]
    [Header("Locator")]
    [SerializeField] public float range;






}
