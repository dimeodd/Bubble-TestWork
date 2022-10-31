using MyEcs;
using MyEcs.GoPool;
using EcsStructs;
using UnityEngine;

namespace EcsSystems
{
    public class BallSpawnerSystem : IUpd
    {
        Filter<BallSpawnData> spawnFilter = null;
        HexGridData _grid = null;
        SceneData _scene = null;
        EcsWorld _world = null;
        StaticData _stData = null;

        public void Upd()
        {
            foreach (var i in spawnFilter)
            {
                ref var spawnData = ref spawnFilter.Get1(i);
                var x = spawnData.x;
                var y = spawnData.y;

                var isOutOfWidth = x < 0 | x >= _grid.Width;
                var isOutOfHeight = y < 0 | y >= _grid.Height;
                if (isOutOfWidth | isOutOfHeight) continue;

                var isChet = y % 2 > 0;
                var isOutOfChetWidth = x >= _grid.Width - 1;
                if (isChet & isOutOfChetWidth) continue;

                var go = MonoBehaviour.Instantiate(_stData.balls[spawnData.BallID].Ball, _scene.BallsArea);
                go.transform.localPosition = BallHelper.IndexToPos(x, y);

                var ent = _world.NewEntity();
                var entID = go.AddComponent<EntityID>();
                entID.SetEntity(ent);
                ref var ballData = ref ent.Get<BallData>();
                ballData.x = x;
                ballData.y = y;
                ballData.BallID = spawnData.BallID;

                _grid.data[x, y] = ent;

                //TODO
                if (spawnData.isPlayerBall)
                {
                    var arr = BallHelper.GetClosestIndexesByIndex(x, y);
                    int sameCount = 0;

                    for (int k = 0; k < 2; k++)
                    {
                        utrirhtoniu
                    }
                    if (sameCount > 2)
                        ent.Get<BlopedBallTag>();
                }
            }
        }
        static float GetHexHeight(float h)
        {
            return h * StaticData.sin60;
        }
    }
}