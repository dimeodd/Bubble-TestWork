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
            UIlayer = _stData.UImask.value;

            var ent = _world.NewEntity();
            ent.Get<InputData>();
        }

        public void Upd()
        {
            var pos = Input.mousePosition;
            var wPos = _scene.MainCamera.ScreenToWorldPoint(pos);

            foreach (var i in inputFilter)
            {
                ref var inputData = ref inputFilter.Get1(i);
                inputData.temp = inputData.curr;

                ref var input = ref inputData.curr;

                inputData.curr.pos = wPos;
                inputData.curr.IsPressed = Input.GetMouseButton(0);
                var collider = Physics2D.OverlapPoint(wPos, UIlayer);
                input.IsInsideFireZone = collider != null;
            }
        }
    }
}