using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
public class UI_Manager : MonoBehaviour
{
    public static UI_Manager Instance;

    [SerializeField]private TextMeshProUGUI totalScoreTMP;
    [SerializeField]private TextMeshProUGUI TopicTMP;
    [SerializeField]private TextMeshProUGUI scoreTextTMP;
    [SerializeField]private TextMeshProUGUI rerolsTextTMP;
    [SerializeField]private RectTransform EndScreen;
    [SerializeField]private TextMeshProUGUI currentLevelTMP;

    private bool IsEndScreenActive = false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void UpdateEndScreen(bool IsVictory, float score) 
    {
        
        totalScoreTMP.text = $"Total score:{score}";
        if (IsVictory) 
        {
            TopicTMP.text = "Victory";
        }
        else { TopicTMP.text = "Defeat"; }
    }
    public void RotateButton(RectTransform buttonRT)
    {
        if (buttonRT != null) { Debug.Log("кнопка найдена"); }
        buttonRT
            .DORotate(new Vector3(0, 0, -360), 1.5f, RotateMode.FastBeyond360)
            .SetEase(Ease.OutCubic);
    }

    public void UpdateScore(float score, float totalScoreToWin)
    {
        scoreTextTMP.text = $"{score}/{totalScoreToWin}";
    }

    public void UpdateRerols(int currentRerol, int MaxRerols) 
    {
        rerolsTextTMP.text = $"{currentRerol}/{MaxRerols}";
    }

    public void AnimateEndScreen() 
    {
        if (!IsEndScreenActive)
        {
            EndScreen.DOAnchorPos(new Vector2(0, 0), 1.5f).SetEase(Ease.OutCubic);
            IsEndScreenActive = true;
        }
        else 
        {
            EndScreen.DOAnchorPos(new Vector2(1080, 1920), 1.5f).SetEase(Ease.OutCubic);
            IsEndScreenActive = false;
        }
    }

    public void SwitchLevelText() 
    {
        currentLevelTMP.text = $"Level {GameManager.instance.levelSetting_SO.currentLevel}";
    }

}
