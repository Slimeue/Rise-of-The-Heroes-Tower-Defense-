using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemies", menuName = "Enemy/Enemy Data")]
public class EnemiesData : ScriptableObject
{

    [SerializeField] public GameObject enemyPrefab;
    [SerializeField] public float hitPoints;
    [SerializeField] public float maxHp;

}
