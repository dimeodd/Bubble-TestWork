using MyEcs;
using EcsStructs;
using System;

namespace EcsSystems
{
    public class ColorSelectorSystem : IInit, IUpd
    {
        Filter<InputData> inputFilter = null;
        Filter<NeedBallTag> needBallFilter = null;
        Filter<BlockInputTag> blockFilter = null;
        StaticData _stData = null;
        LevelData _level = null;
        EcsWorld _world = null;

        Random _rnd;
        int index = 0;

        public void Init()
        {
            _rnd = new Random(_level.BallsSeed);

            foreach (var i in inputFilter)
            {
                var ent = inputFilter.GetEntity(i);
                ent.Get<NeedBallTag>();
            }
        }

        public void Upd()
        {
            blockFilter.GetEnumerator();
            if (blockFilter.Count > 0) return;

            if (_level.IsRandomColors)
                CreateRandomColor();
            else
                CreateCustomColor();

            foreach (var i in needBallFilter)
            {
                var ent = needBallFilter.GetEntity(i);
                ent.Del<NeedBallTag>();
            }
        }

        void CreateRandomColor()
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

        void CreateCustomColor()
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