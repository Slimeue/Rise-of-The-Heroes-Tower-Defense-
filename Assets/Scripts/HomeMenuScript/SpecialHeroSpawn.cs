using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialHeroSpawn : MonoBehaviour
{
    public CharacterData characterData;

    public GameObject _specialHero;

    private void Awake()
    {
        characterData = SquadManager.instance.selectedSpecialHero;
        _specialHero = Instantiate(characterData.charObject, transform.position, Quaternion.identity);
    }

}
