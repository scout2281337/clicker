using UnityEngine;

public class FigureBehavior : MonoBehaviour
{
    public SpriteRenderer shapeRenderer;
    //public SpriteRenderer borderRenderer;
    public SpriteRenderer animalRenderer;

    public FigureData figureData;

    public Sprite circleSprite;
    public Sprite squareSprite;
    public Sprite triangleSprite;

    public void Setup(FigureData data)
    {
        figureData = data;

        // ��������� �����
        switch (data.shape)
        {
            case "Circle":
                shapeRenderer.sprite = circleSprite;
                //shapeRenderer.gameObject.AddComponent<PolygonCollider2D>();
                break;
            case "Square":
                shapeRenderer.sprite = squareSprite;
                break;
            case "Triangle":
                shapeRenderer.sprite = triangleSprite;
                break;
        }
        shapeRenderer.gameObject.AddComponent<PolygonCollider2D>();
        // ���� �����
        shapeRenderer.color = data.borderColor;

        // ������ ���������
        animalRenderer.sprite = data.animalSprite;
    }
}
