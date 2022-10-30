using MyEcs;
using EcsStructs;
using UnityEngine;

namespace EcsSystems
{
    public class AimSystem : IUpd
    {
        Filter<InputData> inputFilter = null;
        SceneData _scene = null;
        EcsWorld _world = null;

        public void Upd()
        {
            var aimLine = _scene.AimLine;

            foreach (var i in inputFilter)
            {
                ref var inputData = ref inputFilter.Get1(i);
                ref var input = ref inputData.curr;
                ref var temp = ref inputData.temp;
                var firePointPos = _scene.FirePoint.position;


                //If Fire
                if (input.IsInsideFireZone &
                    !input.IsPressed & temp.IsPressed)
                {
                    var ent = _world.NewEntity();
                    ref var fireData = ref ent.Get<FireData>();
                }


                //AimLine
                if (input.IsInsideFireZone & input.IsPressed)
                {
                    if (!aimLine.enabled) aimLine.enabled = true;
                    var points = new Vector3[] { firePointPos, input.pos };

                    aimLine.SetPositions(points);
                }
                else
                {
                    if (aimLine.enabled) aimLine.enabled = false;
                }
            }
        }
    }
}