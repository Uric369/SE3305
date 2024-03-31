using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI组件")]
    public PlayerUI playerUI;
    public EnemyUI enemyUI;
    public TextMeshProUGUI crystalText;
    public TextMeshProUGUI scoreText;
    public float outerBlood;
    public float innerblood;

    [Header("事件监听")]
    public CharacterEventSO healthEvent;
    public ScoreManagerEventSO scoreManagerEvent;

    private void OnEnable()
    {
        healthEvent.OnEventRaised += OnHealthEvent;
        scoreManagerEvent.OnEventRaised += OnScoreManagerEvent;
    }

    private void OnDisable()
    {
        healthEvent.OnEventRaised -= OnHealthEvent;
    }

    private void OnHealthEvent(Character character)
    {
        outerBlood = character.currentHealth;
        if(character.gameObject.tag == "Player"){
            var percentage = character.currentHealth / character.maxHealth;
            playerUI.bloodUpdate(percentage);
        }
        if (character.gameObject.tag == "Enemy" && character.gameObject.name == "Dragon1")
        {
            var percentage = character.currentHealth / character.maxHealth;
            enemyUI.bloodUpdate(percentage, 0);
        }
        if (character.gameObject.tag == "Enemy" && character.gameObject.name == "Dragon2")
        {
            innerblood = character.currentHealth;
            var percentage = character.currentHealth / character.maxHealth;
            enemyUI.bloodUpdate(percentage, 1);
        }
    }

    private void OnScoreManagerEvent(ScoreManager scoreManager)
    {
        crystalText.text = scoreManager.crystalNum.ToString();
        scoreText.text = "Score  " + scoreManager.totalScore.ToString();
    }
}
