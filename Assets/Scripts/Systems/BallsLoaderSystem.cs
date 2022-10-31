using MyEcs;
using EcsStructs;
using UnityEngine;

namespace EcsSystems
{
    public class BallsLoaderSystem : IInit
    {
        EcsWorld _world = null;
        StaticData _stData = null;
        LevelData _level = null;

        public void Init()
        {
            var map = _level.levelMap;
            var h = map.height;
            var w = map.width;

            for (int x = 0; x < w; x++)
            {
                for (int y = 1; y < h; y++)
                {
                    Color32 ballColor = map.GetPixel(x, h - y);
                    var ballID = GetBallIDByColor(ballColor);
                    if (ballID == -1) continue;

                    var ent = _world.NewEntity();
                    ref var spawnData = ref ent.Get<BallSpawnData>();
                    spawnData.x = x;
                    spawnData.y = y - 1; //костыль фиксящий дубликат верхнего ряда
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