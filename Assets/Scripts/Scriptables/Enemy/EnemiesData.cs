using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemies", menuName = "Enemy/Enemy Data")]
public class EnemiesData : ScriptableObject
{
    [Space(5)]
    [SerializeField] public GameObject enemyPrefab;
    [SerializeField] public GameObject projectile;
    [SerializeField] public Sprite enemyArtWork;

    [SerializeField] public string enemyName;

    [Space(5)]
    [SerializeField] public float dmgValue;

    [Space(5)]
    [Header("Health")]
    [SerializeField] public float hitPoints;
    [SerializeField] public float maxHp;

    [Space(5)]
    [Header("Defense")]
    [SerializeField] public float baseArmor;

    [SerializeField] public float moveSpeed;

    [SerializeField] public float attackRange;

    [SerializeField] public float rotationsSpeed;

    [Space(5)]
    [Header("Lore")]
    [TextArea(5, 10)][SerializeField] public string enemyLore;

    [SerializeField] public LayerMask whatIsTower;


}
