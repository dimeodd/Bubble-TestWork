using MyEcs;
using EcsStructs;
using UnityEngine;

namespace EcsSystems
{
    public class ThrowBallSystem : IUpd
    {
        Filter<InputData, ButtonUpTag> inputFilter = null;
        Filter<PlayerBallData> playerBallFilter = null;
        Filter<BlockInputTag> blockFilter = null;

        StaticData _stData = null;
        SceneData _scene = null;
        PauseMenu _pauseMenu = null;
        EcsWorld _world = null;

        public void Upd()
        {
            blockFilter.GetEnumerator();
            if (blockFilter.Count > 0) return;

            //Предотвращает запуск шарика после закрытия меню паузы
            if (_pauseMenu.timeFromPause < 0.5f)
                return;

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
                var ent = _world.NewEntity();
                ent.Get<BlockInputTag>();
            }

        }
    }
}