using MyEcs;
using EcsStructs;
using UnityEngine;

namespace EcsSystems
{
    public class DestroySystem : IUpd
    {
        Filter<DestroyTag, PlayerBallData> playerBallFilter = null;
        Filter<DestroyTag, BallData, GoEntityProvider> ballFilter = null;
        Filter<InputData> inputFilter = null;
        EcsWorld _world = null;

        public void Upd()
        {
            //Удаление PlayerBall
            foreach (var i in playerBallFilter)
            {
                var ball = playerBallFilter.Get2(i);
                MonoBehaviour.Destroy(ball.go, 0.001f);

                var ent = playerBallFilter.GetEntity(i);
                ent.Destroy();
            }
            //Разблокировка управления
            if (playerBallFilter.Count > 0)
            {
                foreach (var j in inputFilter)
                {
                    var ent = inputFilter.GetEntity(j);
                    ent.Del<BlockInputTag>();

                    ent = _world.NewEntity();
                    ent.Get<NeedBallTag>();
                }
            }

            //TODO Удаление Ball
            foreach (var i in ballFilter)
            {
                var prov = ballFilter.Get3(i);
                MonoBehaviour.Destroy(prov.provider.gameObject, 0.001f);

                var ent = ballFilter.GetEntity(i);
                ent.Destroy();
            }

        }
    }
}