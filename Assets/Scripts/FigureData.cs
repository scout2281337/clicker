using UnityEngine;

[System.Serializable]
public class FigureData
{
    public string shape;             
    public Color borderColor;        
    public Sprite animalSprite;

    public override bool Equals(object obj)
    {
        if (obj is not FigureData other)
            return false;

        return shape == other.shape &&
               borderColor.Equals(other.borderColor) &&
               animalSprite == other.animalSprite;
    }

    public override int GetHashCode()
    {
        // Комбинируем хэши всех полей
        int hash = 17;
        hash = hash * 23 + (shape?.GetHashCode() ?? 0);
        hash = hash * 23 + borderColor.GetHashCode();
        hash = hash * 23 + (animalSprite?.GetHashCode() ?? 0);
        return hash;
    }
}
