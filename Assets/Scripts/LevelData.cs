using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Bubble-TestWork/LevelData", order = 0)]
public class LevelData : ScriptableObject
{

    [Header("Map")]
    public bool IsRandomMap;
    public Texture2D levelMap;
    public int LevelSeed;

    [Header("Colors")]
    public bool IsRandomColors;
    public int ColorSeed;

    public BallType[] ballIDs;
}

public enum BallType
{
    RED,
    ORANGE,
    YELLOW,
    GREEN,
    CYAN,
    BLUE,
    PURPLE
}
