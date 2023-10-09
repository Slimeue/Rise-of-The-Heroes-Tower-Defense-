using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialHeroSpawn : MonoBehaviour
{
    public CharacterData characterData;

    private void Awake()
    {
        Instantiate(characterData.charObject, transform.position, Quaternion.identity);
    }

}
