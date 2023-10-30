using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillHolder : MonoBehaviour, IPointerClickHandler
{

    public GameObject _selectedHero;

    SpecialHeroSpawn specialHeroSpawn;
    ISkillable skillable;

    public void OnPointerClick(PointerEventData eventData)
    {
        skillable.Skill();
    }

    // Start is called before the first frame update
    void Start()
    {
        specialHeroSpawn = FindObjectOfType<SpecialHeroSpawn>();
        _selectedHero = specialHeroSpawn._specialHero;

        skillable = _selectedHero.GetComponent<ISkillable>();
    }

}

