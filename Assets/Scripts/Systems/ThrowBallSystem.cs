using MyEcs;
using EcsStructs;
using UnityEngine;

namespace EcsSystems
{
    public class ThrowBallSystem : IUpd
    {
        Filter<InputData, ButtonUpTag>.Exclude<BlockInputTag> inputFilter = null;
        Filter<PlayerBallData> ballFilter = null;
        SceneData _scene = null;
        StaticData _stData = null;

        public void Upd()
        {
            foreach (var j in inputFilter)
            {
                ref var input = ref inputFilter.Get1(j);
                if (!input.IsInsideFireZone) continue;

                foreach (var i in ballFilter)
                {
                    ref var ball = ref ballFilter.Get1(i);
                    ball.rigidbody.velocity = (input.pos - (Vector2)_scene.FirePoint.position).normalized * _stData.BallSpeed;
                }
                var ent = inputFilter.GetEntity(j);
                ent.Get<BlockInputTag>();
            }

        }
    }
}