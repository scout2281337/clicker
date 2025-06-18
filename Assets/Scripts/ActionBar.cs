using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ActionBar : MonoBehaviour
{
    public Transform[] barSlots;
    private List<FigureData> currentFigures = new List<FigureData>();

    public void AddFigure(GameObject figurePrefab, FigureData data)
    {
        int targetIndex = GetFirstFreeSlot();

        if (targetIndex == -1)
        {
            Debug.Log("Бар полон — проигрыш");
            return;
        }

        GameObject clone = Instantiate(figurePrefab, barSlots[targetIndex].position, Quaternion.identity, barSlots[targetIndex]);
        clone.transform.localScale = Vector3.one * 1.25f; 

        var rb = clone.GetComponent<Rigidbody2D>();
        if (rb) Destroy(rb); // или: rb.simulated = false;

        var collider = clone.GetComponentInChildren<Collider2D>();
        if (collider) collider.enabled = false;

        currentFigures.Add(data);

        // Optionally: CheckForTriples();
    }

    private int GetFirstFreeSlot()
    {
        for (int i = 0; i < barSlots.Length; i++)
        {
            if (barSlots[i].childCount == 0)
                return i;
        }
        return -1; 
    }


}
