using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;
using System.Collections;
public class ActionBar : MonoBehaviour
{
    public Transform[] barSlots;
    private List<FigureData> currentFigures = new List<FigureData>();
    
    public GameManager gameManager;
    public float LocalScale;

    [SerializeField]private GameObject DefeatScreen;
    [SerializeField]private float animDuration;
    public IEnumerator AddFigure(GameObject figurePrefab, FigureData data, Transform originalTranform)
    {
        int targetIndex = GetFirstFreeSlot();

        if (targetIndex == -1)
        {
            DefeatScreen.SetActive(true);
            Debug.Log("Бар полон — проигрыш");

            yield break;
        }

        GameObject clone = Instantiate(figurePrefab, originalTranform.position, originalTranform.rotation, barSlots[targetIndex]); // barSlots[targetIndex].position
        clone.transform.localScale = originalTranform.localScale * LocalScale;


        Sequence seq = DOTween.Sequence();
        seq.Join(clone.transform.DOMove(barSlots[targetIndex].position, animDuration).SetEase(Ease.InOutExpo));
        seq.Join(clone.transform.DOScale(1f, animDuration));
        seq.Join(clone.transform.DORotate(Vector3.zero, animDuration));


        var rb = clone.GetComponent<Rigidbody2D>();
        if (rb) Destroy(rb); 
        
        var collider = clone.GetComponentInChildren<Collider2D>();
        if (collider) Destroy(collider); //collider.enabled = false;


        yield return seq.WaitForCompletion();

        currentFigures.Add(data);
        CheckForTriples();
    }

    private int GetFirstFreeSlot()
    {
        for (int i = 0; i < barSlots.Length -1; i++) // -1 можно убрать
        {
            if (barSlots[i].childCount == 0)
                return i;
        }
        return -1; 
    }

    private void CheckForTriples()
    {
        Dictionary<FigureData, List<int>> groups = new Dictionary<FigureData, List<int>>();

        for (int i = 0; i < currentFigures.Count; i++)
        {
            FigureData data = currentFigures[i];

            if (!groups.ContainsKey(data))
                groups[data] = new List<int>();

            groups[data].Add(i);
        }

        foreach (var group in groups)
        {
            if (group.Value.Count >= 3)
            {
                Debug.Log("Нашли тройку! Удаляем...");
                gameManager.AddScore(100);
                for (int i = 0; i < 3; i++)
                {
                    int index = group.Value[i];

                    if (barSlots[index].childCount > 0)
                        Destroy(barSlots[index].GetChild(0).gameObject);

                    currentFigures[index] = null;
                }

                break; // удаляем только одну тройку за раз
            }
        }

        currentFigures.RemoveAll(item => item == null);
    }

    void AnimatedMovingToActionBar(GameObject go, Transform origianlTransform, Transform barSlot) 
    {
        go.transform.localScale = origianlTransform.localScale * LocalScale;
        go.transform.DOMove(barSlot.position, animDuration).SetEase(Ease.InOutExpo);
        go.transform.DOScale(1f, animDuration);

    }

}
