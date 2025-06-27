using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public LevelSetting_SO levelSetting_SO;    
    
    public float score;
    public float TotalScoreToWin;

    public int currentRerol;
    public int MaxRerols;


    [SerializeField]private GameObject EndScreen; 

    private FigureSpawner _figureSpawner;
    private UI_Manager _UI_Manager;
    
    private void Awake()
    {
        if (instance == null) 
        {
            instance = this;
        }
        else { Destroy(gameObject); }
        _figureSpawner = GetComponent<FigureSpawner>();
        _UI_Manager = GetComponent<UI_Manager>();   
        
        
        TotalScoreToWin = levelSetting_SO.TotalScoreToWin;
        MaxRerols = levelSetting_SO.MaxRerols;

        _UI_Manager.UpdateScore(score, TotalScoreToWin);
        _figureSpawner.OnSpawnFigures += AddReroll;
    }
    public void AddScore(float Amount) 
    {
        score += Amount;
        _UI_Manager.UpdateScore(score, TotalScoreToWin);
        if (score >= TotalScoreToWin)
        {
            _UI_Manager.UpdateEndScreen(true, score);
            EndScreen.SetActive(true);
        }
    }
    public void StartNewGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddReroll() 
    {
        currentRerol += 1;
        if (currentRerol >= MaxRerols) 
        {
            _UI_Manager.UpdateEndScreen(false, score);
            EndScreen.SetActive(true);
        }
    }
}
