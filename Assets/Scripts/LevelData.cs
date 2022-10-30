using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Bubble-TestWork/LevelData", order = 0)]
public class LevelData : ScriptableObject
{

    [Header("Map")]
    public Texture2D levelMap;

    [Header("Colors")]
    public bool IsRandomColors;
    public int BallsSeed;

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
