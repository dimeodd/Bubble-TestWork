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

        public bool dirtyFlag = false;
        int UIlayer;
        public void Init()
        {
            UIlayer = _stData.UImask;

            var ent = _world.NewEntity();
            ent.Get<InputData>();
        }

        public void Upd()
        {

            var wPos = _scene.MainCamera.ScreenToWorldPoint(Input.mousePosition);

            foreach (var i in inputFilter)
            {
                //Init Entity
                var ent = inputFilter.GetEntity(i);

                ref var input = ref inputFilter.Get1(i);

                input.pos = wPos;

                var collider = Physics2D.OverlapPoint(wPos, UIlayer);
                input.IsInsideFireZone = collider != null;

                if (Input.GetMouseButtonDown(0))
                {
                    input.IsPressed = true;
                    ent.Get<ButtonDownTag>();
                }
                if (Input.GetMouseButtonUp(0))
                {
                    input.IsPressed = false;
                    ent.Get<ButtonUpTag>();
                }
            }
        }
    }
}