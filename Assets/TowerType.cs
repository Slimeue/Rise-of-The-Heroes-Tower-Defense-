using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerType : MonoBehaviour
{
    public CharacterData characterData;

    public LayerMask charType;

    private void Awake()
    {
        charType = characterData._layerMask;
    }
}
