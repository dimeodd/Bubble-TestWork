using MyEcs;
using EcsStructs;
using UnityEngine;

namespace EcsSystems
{
    public class BallsAreaMoverSystem : IUpd
    {
        SceneData _scene = null;

        public void Upd()
        {
            //TODO сделать прокрутку как в играх из ТЗ
            Vector3 leftScreenCornerPos = _scene.MainCamera.ScreenToWorldPoint(new Vector2(0, Screen.height));
            leftScreenCornerPos.z = 1;
            _scene.BallsArea.transform.position = leftScreenCornerPos + new Vector3(0.5f, -0.5f);
        }
    }
}