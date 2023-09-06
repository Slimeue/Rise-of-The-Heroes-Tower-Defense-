using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Charaters/Towers", menuName = "Character/Character Data")]
public class CharacterData : ScriptableObject
{
    [Space(5)]
    [SerializeField] public GameObject enemyPrefab;

    [Space(5)]
    [SerializeField] public float dmgValue;

    [Space(5)]
    [Header("Health")]
    [SerializeField] public float hitPoints;
    [SerializeField] public float maxHp;

    [Space(5)]
    [SerializeField] public float mana;

    [Space(5)]
    [Header("Defense")]
    [SerializeField] public float baseArmor;


}
