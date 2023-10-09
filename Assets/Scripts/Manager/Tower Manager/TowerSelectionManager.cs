using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelectionManager : MonoBehaviour
{
    public static TowerSelectionManager Instance;

    public int maxTowersToSelect = 6;

    private List<CharacterData> selectedTowers = new List<CharacterData>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool CanSelectTower()
    {
        return selectedTowers.Count < maxTowersToSelect;
    }

    public void AddSelectedTower(CharacterData characterData)
    {
        if (CanSelectTower() && !selectedTowers.Contains(characterData))
        {
            selectedTowers.Add(characterData);
        }
    }

    public void RemoveSelectedTower(CharacterData characterData)
    {
        if (selectedTowers.Contains(characterData))
        {
            selectedTowers.Remove(characterData);
        }
    }

    public List<CharacterData> GetSelectedTowers()
    {
        return selectedTowers;
    }

    public void ClearSelectedTowers()
    {
        selectedTowers.Clear();

    }

}
