using UnityEngine;

[CreateAssetMenu(fileName = "LevelSetting_SO", menuName = "Scriptable Objects/LevelSetting_SO")]
public class LevelSetting_SO : ScriptableObject
{
    public float TotalScoreToWin;
    public int MaxRerols;

    public float Delay;
    public int AmountToSpawn;

    public int currentLevel;
}
