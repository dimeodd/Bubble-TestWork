using MyEcs;
using EcsStructs;
using UnityEngine;

namespace EcsSystems
{
    public class ColorSelectorSystem_Chain : IInit, IUpd
    {
        Filter<InputData> inputFilter = null;
        Filter<NeedBallTag> needBallFilter = null;
        StaticData _stData = null;
        LevelData _level = null;
        EcsWorld _world = null;

        int index = 0;
        public void Init()
        {
            foreach (var i in inputFilter)
            {
                var ent = inputFilter.GetEntity(i);
                ent.Get<NeedBallTag>();
            }
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

            foreach (var i in needBallFilter)
            {
                var ent = needBallFilter.GetEntity(i);
                ent.Del<NeedBallTag>();
            }
        }
    }
}