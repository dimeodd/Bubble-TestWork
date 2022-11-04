using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "StaticData", menuName = "Bubble-TestWork/StaticData", order = 0)]
public class StaticData : ScriptableObject
{
    [Header("Aim")]
    public int AimCastCount = 3;
    public float LastAimRange = 3.5f;


    [Header("Grid")]
    public int GridWidth;
    public int GridHeight;

    public float GridOffset = 6.5f;
    public float FirePointOffset = 2;


    [Header("Balls")]
    
    public int CountRequredForBlop = 3;
    public float BallSpeed = 1f;
    public GameObject PlayerBall;
    public GameObject AimBall;
    public BallProperty[] balls;


    [Header("Layers")]
    public LayerMask UImask;
    public LayerMask ReflectMask;

    [Header("Other")]
    public Vector2 LimboPos = new Vector2(-500, -500);
    public Sprite SoundOnSprite;
    public Sprite SoundOffSprite;
    public AudioMixer AudioMixer;

    public const float sin60 = 0.86602540378443864676372317075294f;
    public const float cos60 = 0.5f;
}

[System.Serializable]
public class BallProperty
{
    public Color32 SpawnColor;
    public GameObject Ball;
}