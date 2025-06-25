using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float score;
    public float TotalScoreToWin;
    [SerializeField] private GameObject VictoryScreen;

    [SerializeField]private List<TextMeshProUGUI> inGameScoreTexts;

    public void AddScore(float Amount) 
    {
        score += Amount;
        UpdateScore();
        if (score > TotalScoreToWin)
        {
            VictoryScreen.SetActive(true);
        }
    }
    public void StartNewGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void UpdateScore() 
    {
        foreach (var txt in inGameScoreTexts) 
        {
            txt.text = $"Score: {score}";
        }
    }
}
