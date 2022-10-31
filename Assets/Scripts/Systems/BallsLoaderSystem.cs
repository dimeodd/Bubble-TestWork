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

            //HexMap
            // 0. [a1][a2][a3][a4]  (a1)(a2)(a3)(a4)
            // 1. [b1][b2][b3][b4]    (b1)(b2)(b3)
            // 2. [c1][c2][c3][c4]  (c1)(c2)(c3)(a4)

            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    Color32 ballColor = map.GetPixel(x, y);
                    var ballID = GetBallIDByColor(ballColor);
                    if (ballID == -1) continue;

                    var ent = _world.NewEntity();

                    ref var spawnData = ref ent.Get<BallSpawnData>();
                    spawnData.hexPos = new HexVector(x, h - y - 1);
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