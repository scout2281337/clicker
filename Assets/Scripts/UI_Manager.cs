using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class UI_Manager : MonoBehaviour
{
    

    [SerializeField]private TextMeshProUGUI totalScoreTMP;
    [SerializeField]private TextMeshProUGUI TopicTMP;
    [SerializeField]private TextMeshProUGUI scoreTextTMP;

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
}
