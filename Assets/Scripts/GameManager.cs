using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float score;
    public float TotalScoreToWin;
    [SerializeField] private GameObject VictoryScreen;
    public void AddScore(float Amount) 
    {
        score += Amount;
    }

    private void Update()
    {
        if (score > TotalScoreToWin) 
        {
            VictoryScreen.SetActive(true);
        }
    }

    public void StartNewGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
