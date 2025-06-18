using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActionBar : MonoBehaviour
{
    public Transform[] barSlots;
    private List<FigureData> currentFigures = new List<FigureData>();
    
    public GameManager gameManager;

    [SerializeField]private GameObject DefeatScreen;
    public void AddFigure(GameObject figurePrefab, FigureData data)
    {
        int targetIndex = GetFirstFreeSlot();

        if (targetIndex == -1)
        {
            DefeatScreen.SetActive(true);
            Debug.Log("Бар полон — проигрыш");
            return;
        }

        GameObject clone = Instantiate(figurePrefab, barSlots[targetIndex].position, Quaternion.identity, barSlots[targetIndex]);
        clone.transform.localScale = Vector3.one * 1.25f; 

        var rb = clone.GetComponent<Rigidbody2D>();
        if (rb) Destroy(rb); 
        
        var collider = clone.GetComponentInChildren<Collider2D>();
        if (collider) Destroy(collider); //collider.enabled = false;

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

}
