using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillHolder : MonoBehaviour, IPointerClickHandler
{

    public GameObject _selectedHero;
    public CharacterData characterData;

    SpecialHeroSpawn specialHeroSpawn;
    ISkillable skillable;

    //UI
    [SerializeField] Image skillArtWork;
    [SerializeField] TextMeshProUGUI skillName;

    public Slider skilSlider;



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
        characterData = skillable.SkillData();

        CharacterSkill characterSkill = characterData.skillData;

        skillArtWork.sprite = characterSkill.skillArtWork;
        skillName.text = characterData.charName;
    }





}

