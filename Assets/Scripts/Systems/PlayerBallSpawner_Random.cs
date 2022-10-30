using MyEcs;
using EcsStructs;
using System;

namespace EcsSystems
{
    public class ColorSelectorSystem_Random : IInit, IUpd
    {
        Filter<NeedBallTag> filter = null;
        StaticData _stData = null;
        LevelData _level = null;
        EcsWorld _world = null;

        Random rnd;

        public void Init()
        {
            var ent = _world.NewEntity();
            ent.Get<NeedBallTag>();
            rnd = new Random(_level.BallsSeed);
        }

        public void Upd()
        {
            foreach (var i in filter)
            {
                var ent = _world.NewEntity();
                ref var spawnData = ref ent.Get<PlayerBallSpawnData>();
                spawnData.BallID = rnd.Next() % 7;
                break;
            }
        }
    }
}