using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalCameraScaler : MonoBehaviour
{
    public float TargetWidth = 5;
    Camera camera;

    void Start()
    {
        camera = GetComponent<Camera>();
    }

    void Update()
    {
        float h = Screen.height;
        float w = Screen.width;
        var z = h / w;
        var newSize = TargetWidth * z;
        camera.orthographicSize = newSize;
    }
}
