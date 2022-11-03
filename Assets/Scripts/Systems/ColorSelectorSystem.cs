using MyEcs;
using EcsStructs;
using System;

namespace EcsSystems
{
    public class ColorSelectorSystem : IInit, IUpd
    {
        Filter<PlayerBallData> playerBallFilter = null;
        Filter<BlockInputTag> blockFilter = null;
        LevelData _level = null;
        EcsWorld _world = null;

        Random _rnd;
        int _index = 0;

        public void Init()
        {
            _rnd = new Random(_level.ColorSeed);
        }

        public void Upd()
        {
            blockFilter.GetEnumerator();
            if (blockFilter.Count > 0) return;

            playerBallFilter.GetEnumerator();
            if (playerBallFilter.Count == 0)
            {
                //Init Entity for BallSpawnerSystem
                var ent = _world.NewEntity();

                ref var spawnData = ref ent.Get<PlayerBallSpawnData>();
                spawnData.BallID = _level.IsRandomColors ?
                        GetRandomColor() :
                        GetCustomColor();
            }
        }

        int GetRandomColor()
        {
            return _rnd.Next() % 7;
        }

        int GetCustomColor()
        {
            int ballID = (int)_level.ballIDs[_index];
            _index++;
            if (_index >= _level.ballIDs.Length)
            {
                _index = 0;
            }

            return ballID;
        }
    }
}