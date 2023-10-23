using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroductionTrigger : MonoBehaviour
{
    [SerializeField] EnemiesData enemiesData;
    [SerializeField] GameObject introductionPanel;
    [SerializeField] Image enemyArtWork;
    [SerializeField] TextMeshProUGUI enemyName;
    [SerializeField] TextMeshProUGUI enemyHp;
    [SerializeField] TextMeshProUGUI enemyDefense;
    [SerializeField] TextMeshProUGUI enemyAttack;
    [SerializeField] TextMeshProUGUI enemyLore;


    private void Awake()
    {
        introductionPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        IEnemyDataGetable enemyDataGetable = other.GetComponent<IEnemyDataGetable>();

        if (other.gameObject.CompareTag("Enemy"))
        {
            Time.timeScale = 0f;
            enemiesData = enemyDataGetable.GetEnemyData();

            enemyName.text = enemiesData.enemyName;
            enemyArtWork.sprite = enemiesData.enemyArtWork;
            enemyHp.text = enemiesData.maxHp.ToString();
            enemyDefense.text = enemiesData.baseArmor.ToString();
            enemyAttack.text = enemiesData.dmgValue.ToString();
            enemyLore.text = enemiesData.enemyLore;
            introductionPanel.SetActive(true);
            PauseMenu.isGamePause = true;
        }
    }
}
