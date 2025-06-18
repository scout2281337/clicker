using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private ActionBar manager;

    void Update()
    {
        OnClick();
    }

    private void OnClick()
    {
        if (IsInputDown())
        {
            Vector2 worldPoint;

#if UNITY_EDITOR || UNITY_STANDALONE
            worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
#elif UNITY_IOS || UNITY_ANDROID
            worldPoint = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
#else
            worldPoint = Vector2.zero;
#endif

            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit.collider != null && hit.collider.CompareTag("Figure"))
            {
                Debug.Log("Попали в объект: " + hit.collider.gameObject.transform.parent.gameObject.name);
                GameObject figure = hit.collider.gameObject;
                FigureData data = figure.GetComponentInParent<FigureBehavior>().figureData;
                manager.AddFigure(hit.collider.gameObject.transform.parent.gameObject, data);
                Destroy(hit.collider.gameObject.transform.parent.gameObject);
            }
        }
    }

    private bool IsInputDown()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        return Input.GetMouseButtonDown(0);
#elif UNITY_IOS || UNITY_ANDROID
        return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
#else
        return false;
#endif
    }
}
