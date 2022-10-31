using MyEcs;
using EcsStructs;
using UnityEngine;

namespace EcsSystems
{
    public class ColorSelectorSystem_Chain : IInit, IUpd
    {
        Filter<NeedBallTag> needBallFilter = null;
        StaticData _stData = null;
        LevelData _level = null;
        EcsWorld _world = null;

        int index = 0;
        public void Init()
        {
            //Init Entity for ColorSelectorSystem
            var ent = _world.NewEntity();
            ent.Get<NeedBallTag>();
        }

        public void Upd()
        {
            foreach (var i in needBallFilter)
            {
                //Init Entity for BallSpawnerSystem
                var ent = _world.NewEntity();

                ref var spawnData = ref ent.Get<PlayerBallSpawnData>();
                spawnData.BallID = (int)_level.ballIDs[index];

                index++;
                if (index >= _level.ballIDs.Length)
                {
                    index = 0;
                }

                break;
            }
        }
    }
}