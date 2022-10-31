using MyEcs;
using EcsStructs;
using UnityEngine;
using HexMap;

namespace EcsSystems
{
    public class BallsLoaderSystem : IInit
    {
        StaticData _stData = null;
        LevelData _level = null;
        EcsWorld _world = null;

        public void Init()
        {
            var map = _level.levelMap;
            var h = map.height;
            var w = map.width;

            for (int x = 0; x < w; x++)
            {
                for (int y = 1; y < h; y++) //FIXME костыль фиксящий дубликат верхнего ряда
                {
                    Color32 ballColor = map.GetPixel(x, h - y);
                    var ballID = GetBallIDByColor(ballColor);
                    if (ballID == -1) continue;

                    var ent = _world.NewEntity();

                    ref var spawnData = ref ent.Get<BallSpawnData>();
                    spawnData.hexPos = new HexVector(x, y - 1); //FIXME костыль фиксящий дубликат верхнего ряда
                    spawnData.BallID = ballID;
                }
            }
        }

        int GetBallIDByColor(Color32 color)
        {
            for (int i = 0, iMax = _stData.balls.Length; i < iMax; i++)
            {
                var ballColor = _stData.balls[i].SpawnColor;
                if (ballColor.CustomEquel(color))
                    return i;
            }

            return -1;
        }
    }
}