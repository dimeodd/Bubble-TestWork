using MyEcs;
using EcsStructs;
using UnityEngine;

namespace EcsSystems
{
    public class FirePointMoverSystem : IInit
    {
        StaticData _stData = null;
        SceneData _scene = null;

        public void Init()
        {
            var halfHeight = _scene.MainCamera.orthographicSize;
            _scene.FirePoint.position = new Vector3(0, _stData.FirePointOffset - halfHeight, 0);
        }
    }
}