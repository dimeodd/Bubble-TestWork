using MyEcs;
using EcsStructs;
using UnityEngine;

namespace EcsSystems
{
    public class UserInputSystem : IInit, IUpd
    {
        Filter<InputData> inputFilter = null;
        StaticData _stData = null;
        SceneData _scene = null;
        EcsWorld _world = null;

        int UIlayer;
        public void Init()
        {
            UIlayer = _stData.UImask;

            var ent = _world.NewEntity();
            ent.Get<InputData>();
        }

        public void Upd()
        {
            var pos = Input.mousePosition;
            var wPos = _scene.MainCamera.ScreenToWorldPoint(pos);

            foreach (var i in inputFilter)
            {
                ref var input = ref inputFilter.Get1(i);
                var temp = input;

                input.pos = wPos;
                var collider = Physics2D.OverlapPoint(wPos, UIlayer);
                input.IsInsideFireZone = collider != null;

                if (Input.GetMouseButtonDown(0))
                {
                    input.IsPressed = true;

                }
                if (Input.GetMouseButtonUp(0))
                {
                    input.IsPressed = false;

                }
            }
        }
    }
}