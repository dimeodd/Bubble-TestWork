using MyEcs;
using EcsStructs;
using UnityEngine;

namespace EcsSystems
{
    public class ThrowBallSystem : IUpd
    {
        Filter<InputData, ButtonUpTag>.Exclude<BlockInputTag> inputFilter = null;
        Filter<PlayerBallData> playerBallFilter = null;
        StaticData _stData = null;
        SceneData _scene = null;

        public void Upd()
        {
            foreach (var j in inputFilter)
            {
                ref var input = ref inputFilter.Get1(j);
                if (!input.IsInsideFireZone) continue;

                foreach (var i in playerBallFilter)
                {
                    var rb = playerBallFilter.Get1(i).rigidbody;
                    rb.velocity = (input.pos - (Vector2)_scene.FirePoint.position).normalized * _stData.BallSpeed;
                }

                //Блокировка управления
                var ent = inputFilter.GetEntity(j);
                ent.Get<BlockInputTag>();
            }

        }
    }
}