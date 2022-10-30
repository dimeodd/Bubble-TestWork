using UnityEngine;

[CreateAssetMenu(fileName = "StaticData", menuName = "Bubble-TestWork/StaticData", order = 0)]
public class StaticData : ScriptableObject
{
    public int GridWidth;
    public int GridHeight;

    public float GridOffset = 6.5f;
    public float FirePointOffset = 2;

    public GameObject TestBall;
    public BallProperty[] balls;

    public LayerMask UImask;
    public LayerMask WallMask;
    public LayerMask BallMask;
}

[System.Serializable]
public class BallProperty
{
    public Color32 SpawnColor;
    public GameObject Ball;
}