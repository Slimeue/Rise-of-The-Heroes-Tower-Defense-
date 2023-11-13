using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QualityManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown qualityDropdown;

    private void Awake()
    {
        qualityDropdown.value = QualitySettings.GetQualityLevel();
    }

    public void SetQualityLevelDropdown(int index)
    {
        QualitySettings.SetQualityLevel(index, false);
    }
}
