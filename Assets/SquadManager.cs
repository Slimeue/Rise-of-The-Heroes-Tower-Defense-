using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadManager : MonoBehaviour
{
    public static SquadManager instance;

    [SerializeField] public CharacterData selectedSpecialHero { get; private set; }
    [SerializeField] public CharacterData[] selectedTowers { get; private set; }



    private void Awake()
    {
        CreateSquadManager();

    }

    private void CreateSquadManager()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (selectedSpecialHero != null)
        {
            Debug.Log(selectedSpecialHero);
        }
        else return;

    }

    public CharacterData GetSpecialHeroCharacterData(CharacterData characterData)
    {
        selectedSpecialHero = characterData;
        return characterData;
    }



}
