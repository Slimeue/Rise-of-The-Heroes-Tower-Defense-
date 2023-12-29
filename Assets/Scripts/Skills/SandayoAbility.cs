using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandayoAbility : MonoBehaviour
{
    public float speed = 10.0f; // Adjust this to set the speed of the movement.
    float startCount;
    float maxCount = 3f;

    public float damageValue;

    IDataService dataService = new JsonDataService();
    string saveDataPath = "/character-data"; //TODO static reference


    [SerializeField] CharacterData characterData;

    private void Awake()
    {
        LoadCharStats();
    }

    private void OnEnable()
    {
        startCount = 0f;
    }

    void Update()
    {
        // Move the object forward along its local Z-axis.
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (other.gameObject.CompareTag("Enemy"))
        {
            float percentage = damageValue * 0.5f;
            float skillDamage = damageValue + percentage;
            damageable.Damage(skillDamage);
            Debug.Log("Hit Sandayo Skill");
        }
    }

    public void LoadCharStats()
    {
        string newSaveDataPath = $"{saveDataPath}-{characterData.charName}.json";

        CharacterStats charStatsData = dataService.LoadData<CharacterStats>(newSaveDataPath, false);

        try
        {
            CharacterStats.charStats charData = charStatsData.stats[characterData.name];

            damageValue = charData.damage;

        }
        catch (Exception e)
        {
            Debug.Log(e.Message + " " + e.StackTrace);

        }


    }


}
