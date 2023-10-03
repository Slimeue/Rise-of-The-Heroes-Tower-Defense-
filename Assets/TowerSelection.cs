using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerSelection : MonoBehaviour
{
    public CharacterData characterData;
    public TextMeshProUGUI towerCharName;
    public Image towerCharSprite;

    private bool isSelected;

    // Start is called before the first frame update
    private void Awake()
    {
        towerCharName.text = characterData.name;
        towerCharSprite.sprite = characterData.charArtWork;
        towerCharSprite.gameObject.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {

    }



}
