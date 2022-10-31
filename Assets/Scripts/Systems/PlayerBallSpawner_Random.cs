using MyEcs;
using EcsStructs;
using System;

namespace EcsSystems
{
    public class ColorSelectorSystem_Random : IInit, IUpd
    {
        Filter<NeedBallTag> needBallFilter = null;
        StaticData _stData = null;
        LevelData _level = null;
        EcsWorld _world = null;

        Random _rnd;

        public void Init()
        {
            _rnd = new Random(_level.BallsSeed);

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
                spawnData.BallID = _rnd.Next() % 7;
                break;
            }
        }
    }
}