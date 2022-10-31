using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyEcs;
using EcsStructs;

namespace EcsSystems
{
    public class CameraMoverSystem : IInit
    {
        StaticData _stData = null;
        SceneData _scene = null;

        float _targetWidth;

        public void Init()
        {
            _targetWidth = (float)_stData.GridWidth / 2;

            float h = Screen.height;
            float w = Screen.width;
            var z = h / w;
            var newSize = _targetWidth * z;
            _scene.MainCamera.orthographicSize = newSize;
        }
    }
}
