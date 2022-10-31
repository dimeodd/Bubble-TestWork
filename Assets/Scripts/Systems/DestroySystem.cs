using MyEcs;
using EcsStructs;
using UnityEngine;

namespace EcsSystems
{
    public class DestroySystem : IUpd
    {
        Filter<DestroyTag> destroyFilter = null;
        Filter<DestroyTag, PlayerBallData> playerBallFilter = null;
        Filter<BlockInputTag, InputData> inputFilter = null;
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
                    foreach (var j in inputFilter)
                    {
                        var inputEnt = inputFilter.GetEntity(j);
                        inputEnt.Del<BlockInputTag>();
                        inputEnt.Get<NeedBallTag>();
                        break;
                    }
                }

                ent.Destroy();
            }


        }
    }
}