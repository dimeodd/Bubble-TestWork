using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyEcs;
using EcsStructs;

namespace EcsSystems
{
    public class CameraMoverSystem : IInit, IUpd
    {
        SceneData _scene = null;
        StaticData _stData = null;

        float _targetWidth;

        public void Init()
        {
            _targetWidth = (float)_stData.GridWidth / 2;
        }

        public void Upd()
        {
            float h = Screen.height;
            float w = Screen.width;
            var z = h / w;
            var newSize = _targetWidth * z;
            _scene.MainCamera.orthographicSize = newSize;
        }
    }
}
