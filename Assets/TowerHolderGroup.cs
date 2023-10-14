using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHolderGroup : MonoBehaviour
{

    [SerializeField] private TowerHolder[] towerHolders;

    private void Awake()
    {
        towerHolders = FindObjectsOfType<TowerHolder>();
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (TowerHolder towerHolder in towerHolders)
        {
            foreach (CharacterData characterData in SquadManager.instance.selectedTowers)
            {
                towerHolder.characterData = characterData;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
