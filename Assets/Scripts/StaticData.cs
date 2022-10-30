using UnityEngine;

[CreateAssetMenu(fileName = "StaticData", menuName = "Bubble-TestWork/StaticData", order = 0)]
public class StaticData : ScriptableObject
{
    public int GridWidth;
    public int GridHeight;

    public GameObject TestBall;
    public BallProperty[] balls;

}

[System.Serializable]
public class BallProperty
{
    public Color32 SpawnColor;
    public GameObject Ball;
}