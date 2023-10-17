using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumalindawSkill : MonoBehaviour
{
    float startCount;
    float maxCount = 3f;

    [SerializeField] CharacterData characterData;

    private void OnEnable()
    {
        startCount = 0f;
    }
    private void Update()
    {
        startCount += Time.deltaTime;
        if (startCount >= maxCount)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (other.gameObject.CompareTag("Enemy"))
        {
            float percentage = characterData.dmgValue * 0.5f;
            float skillDamage = characterData.dmgValue + percentage;
            damageable.Damage(skillDamage);
        }
    }
}
