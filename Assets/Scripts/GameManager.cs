using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public LevelSetting_SO levelSetting_SO;
    [HideInInspector] public float score;
    [HideInInspector] public float TotalScoreToWin;
    [HideInInspector] public int currentRerol;
    [HideInInspector] public int MaxRerols;


    [SerializeField]private GameObject EndScreen; 

    private FigureSpawner _figureSpawner;
    private UI_Manager _UI_Manager;

    public event Action OnDataRecieved;
    
    private void Awake()
    {
        if (instance == null) 
        {
            instance = this;
        }
        else { Destroy(gameObject); }


        _figureSpawner = GetComponent<FigureSpawner>();
        _UI_Manager = GetComponent<UI_Manager>();   
        
        _figureSpawner.OnSpawnFigures += AddReroll;
    }

    private void Start()
    {
        ApplyNewData();
    }
    public void AddScore(float Amount) 
    {
        score += Amount;
        _UI_Manager.UpdateScore(score, TotalScoreToWin);
        if (score >= TotalScoreToWin)
        {
            _UI_Manager.UpdateEndScreen(true, score);
            _UI_Manager.AnimateEndScreen();
            //EndScreen.SetActive(true);
        }
    }
    public void StartNewGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddReroll() 
    {
        currentRerol += 1;
        _UI_Manager.UpdateRerols(currentRerol, MaxRerols);
        if (currentRerol >= MaxRerols) 
        {
            _UI_Manager.UpdateEndScreen(false, score);
            _UI_Manager.AnimateEndScreen();
        }
    }

    private void ApplyNewData()
    {
        if (GlobalManager.Instance != null) 
        {
            levelSetting_SO = GlobalManager.Instance.currentLevelDataSO;
        }
        UpdateData();
        OnDataRecieved?.Invoke();
    }
    public void ApplyNewDataForButtons(LevelSetting_SO _levelSetting_SO) 
    {
        levelSetting_SO = _levelSetting_SO;
        UpdateData();
        OnDataRecieved?.Invoke();
    }

    private void UpdateData() 
    {
        TotalScoreToWin = levelSetting_SO.TotalScoreToWin;
        MaxRerols = levelSetting_SO.MaxRerols;
        _UI_Manager.UpdateScore(score, TotalScoreToWin);
        _UI_Manager.SwitchLevelText();
    } 

}
