using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]private ActionBar manager;
    void Update()
    {
        OnClick();    
    }

    private void OnClick() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            //Debug.Log(hit.collider.gameObject.name);
            if (hit.collider != null && hit.collider.tag == "Figure")
            {
                Debug.Log("Попали в объект: " + hit.collider.gameObject.transform.parent.gameObject);
                GameObject figure = hit.collider.gameObject;
                FigureData data = figure.GetComponentInParent<FigureBehavior>().figureData;
                manager.AddFigure(hit.collider.gameObject.transform.parent.gameObject, data);
            }
        }
    }
}
