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
                var tf = go.transform;


                if (isChet)
                {
                    tf.localPosition = new Vector3(x + 0.5f, -GetHexHeight(y), 0);
                }
                else
                {
                    tf.localPosition = new Vector3(x, -GetHexHeight(y), 0);
                }

                var ent = _world.NewEntity();
                var entID = go.AddComponent<EntityID>();
                entID.SetEntity(ent);
                ref var ballData = ref ent.Get<BallData>();
                ballData.x = x;
                ballData.y = y;
                ballData.BallID = spawnData.BallID;

                _grid.data[x, y] = ent;
            }
        }

        const float sin60 = 0.86602540378443864676372317075294f;
        static float GetHexHeight(float h)
        {
            return h * StaticData.sin60;
        }
    }
}