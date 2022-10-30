using MyEcs;
using EcsStructs;

namespace EcsSystems
{
    public class GunSystem : IInit, IUpd
    {
        Filter<InputData> inputFilter = null;
        EcsWorld _world = null;

        public void Init()
        {

        }

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
            }
        }
    }
}