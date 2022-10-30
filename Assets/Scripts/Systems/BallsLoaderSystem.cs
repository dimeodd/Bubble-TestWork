using MyEcs;
using EcsStructs;

namespace EcsSystems
{
    public class BallsLoaderSystem : IInit
    {
        EcsWorld _world = null;
        StaticData _stData = null;

        public void Init()
        {
            var h = 5;
            var w = 11;

            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    var ent = _world.NewEntity();

                    ref var spawnData = ref ent.Get<BallSpawnData>();
                    spawnData.x = x;
                    spawnData.y = y;
                    spawnData.ball = _stData.TestBall;

                }
            }
        }
    }
}