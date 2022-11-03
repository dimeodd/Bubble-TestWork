using MyEcs;
using EcsStructs;
using UnityEngine;

namespace EcsSystems
{
    public class DestroySystem : IUpd
    {
        Filter<DestroyTag> destroyFilter = null;
        Filter<BlockInputTag> blockFilter = null;
        EcsWorld _world = null;

        public void Upd()
        {
            foreach (var i in destroyFilter)
            {
                var ent = destroyFilter.GetEntity(i);

                if (ent.Contain<GoEntityProvider>())
                {
                    var provider = ent.Get<GoEntityProvider>();
                    provider.Recycle();
                }

                //Разблокировка управления
                if (ent.Contain<PlayerBallData>())
                {
                    foreach (var j in blockFilter)
                    {
                        var inputEnt = blockFilter.GetEntity(j);
                        inputEnt.Destroy();
                    }
                }

                ent.Destroy();
            }

            if (destroyFilter.Count > 0)
            {
                var ent = _world.NewEntity();
                ent.Get<CheckWinTag>();
            }

        }
    }
}