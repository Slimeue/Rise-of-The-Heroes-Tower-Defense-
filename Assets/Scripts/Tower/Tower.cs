using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour, IDamageable
{

    [SerializeField] TowerTesting[] towerTesting;

    [SerializeField] CharacterData characterData;

    public float currentHp { get; set; }

    private void Awake()
    {
        towerTesting = FindObjectsOfType<TowerTesting>();
        currentHp = characterData.maxHp;
        Debug.Log(currentHp);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Damage(float damageAmount)
    {
        currentHp -= damageAmount;
        if (currentHp <= 0)
        {
            Debug.Log("Dead");
            Destroy(gameObject);
            TowerHolderEnabler();//TODO:: Enabled The towerholder holding this character again
        }
    }

    public void TowerHolderEnabler()
    {
        foreach (TowerTesting _towerTesting in towerTesting)
        {
            if (characterData == _towerTesting.characterData)
            {
                Debug.Log("TowerEnabling");
                _towerTesting.gameObject.SetActive(true);
                _towerTesting._placed = false;
            }
        }
    }



}
