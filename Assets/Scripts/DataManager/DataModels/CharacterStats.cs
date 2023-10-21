using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterStats
{

    public struct charStats
    {
        public string charName;
        public int level;
        public float hp;
        public float damage;
        public float armor;
    }


    public Dictionary<string, charStats> stats = new();


}
