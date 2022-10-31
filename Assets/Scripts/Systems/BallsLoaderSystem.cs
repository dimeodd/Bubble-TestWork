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
            if (_level.IsRandomMap)
                GenerateLevel(_level.LevelSeed);
            else
                LoadLevel(_level.levelMap);
        }

        void GenerateLevel(int seed)
        {
            var w = 11;
            var h = 10;

            Random.InitState(seed);
            int colorsCount = Random.Range(7, 15);

            for (int i = 0; i < colorsCount; i++)
            {
                var pos = new Vector2Int(Random.Range(0, w), Random.Range(0, h));
                var size = new Vector2Int(Random.Range(2, 8), Random.Range(2, 5));
                var ballID = Random.Range(0, 7);

                for (int x = pos.x, xMax = pos.x + size.x; x < xMax; x++)
                {
                    for (int y = pos.y, yMax = pos.y + size.y; y < yMax; y++)
                    {
                        CreateBall(x, y, ballID);
                    }
                }
            }
        }


        void LoadLevel(Texture2D map)
        {
            var w = map.width;
            var h = map.height;

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

                    CreateBall(x, h - y - 1, ballID);
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

        void CreateBall(int x, int y, int ballID)
        {
            var ent = _world.NewEntity();

            ref var spawnData = ref ent.Get<BallSpawnData>();
            spawnData.hexPos = new HexVector(x, y);
            spawnData.BallID = ballID;
        }
    }
}