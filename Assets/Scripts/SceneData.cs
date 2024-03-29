using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneData : MonoBehaviour
{
    public Camera MainCamera;
    public GameObject PauseMenu;
    public PauseMenu PauseMenuScript;
    public GameObject WinWindow;

    public Transform BallsArea;
    public Transform FirePoint;
    public LineRenderer AimLine;
    public AudioSource BloopSound;
}
