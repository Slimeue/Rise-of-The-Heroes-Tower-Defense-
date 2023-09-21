using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour, IDamageable
{


    [SerializeField] CharacterData characterData;

    public float currentHp { get; set; }

    private void Awake()
    {
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
        }

    }



}
