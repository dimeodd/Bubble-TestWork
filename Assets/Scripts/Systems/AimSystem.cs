using MyEcs;
using EcsStructs;
using UnityEngine;

namespace EcsSystems
{
    public class AimSystem :  IUpd
    {
        Filter<InputData> inputFilter = null;
        SceneData _scene = null;
        EcsWorld _world = null;

        public void Upd()
        {
            foreach (var i in inputFilter)
            {
                ref var inputData = ref inputFilter.Get1(i);
                ref var input = ref inputData.curr;
                ref var temp = ref inputData.temp;

                //If Fire
                if (input.IsInsideFireZone &
                    !input.IsPressed & temp.IsPressed)
                {
                    var ent = _world.NewEntity();
                    ref var fireData = ref ent.Get<FireData>();
                }

                var firePointPos = _scene.FirePoint.position;

                if (input.IsInsideFireZone & input.IsPressed)
                {
                    Debug.Log("aimmed2");
                    Debug.DrawLine(firePointPos, input.pos);
                }
            }
        }
    }
}