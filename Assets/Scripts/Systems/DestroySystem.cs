using MyEcs;
using EcsStructs;
using UnityEngine;

namespace EcsSystems
{
    public class DestroySystem : IUpd
    {
        Filter<DestroyTag, PlayerBallData> ballFilter = null;
        Filter<InputData> inputFilter = null;
        EcsWorld _world = null;

        public void Upd()
        {
            foreach (var i in ballFilter)
            {
                ref var ball = ref ballFilter.Get2(i);
                MonoBehaviour.Destroy(ball.go, 0.001f);
                var ent = ballFilter.GetEntity(i);
                ent.Destroy();
            }

            if (ballFilter.Count > 0)
            {
                foreach (var j in inputFilter)
                {
                    var ent = inputFilter.GetEntity(j);
                    ent.Del<BlockInputTag>();

                    ent = _world.NewEntity();
                    ent.Get<NeedBallTag>();
                }
            }

        }
    }
}