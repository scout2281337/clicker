using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalManager : MonoBehaviour
{
    public static GlobalManager Instance;

    public LevelSetting_SO currentLevelDataSO;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    public void StartLevel(LevelSetting_SO yourCurrentLevel_SO) 
    {
        currentLevelDataSO = yourCurrentLevel_SO;
        SceneManager.LoadScene("MainScene");
    }

    public void ButtonBasicAnimation(RectTransform buttonRT)
    {
        buttonRT.DOScale(0.5f, 0.5f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            buttonRT.DOScale(1f, 0.1f).SetEase(Ease.OutBack);
        });
    }

    public IEnumerator StartLevelWAnims(RectTransform buttonRT) 
    {
        Sequence seq = DOTween.Sequence();
        seq.Join(buttonRT.DOScale(0.5f, 0.5f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            buttonRT.DOScale(1f, 0.1f).SetEase(Ease.OutBack);
        }));
        
        yield return seq.WaitForCompletion();

        
    }
}
