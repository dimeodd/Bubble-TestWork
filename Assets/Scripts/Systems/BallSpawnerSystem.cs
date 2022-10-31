using MyEcs;
using MyEcs.GoPool;
using EcsStructs;
using UnityEngine;
using HexMap;

namespace EcsSystems
{
    public class BallSpawnerSystem : IUpd
    {
        Filter<BallSpawnData> spawnFilter = null;
        StaticData _stData = null;
        SceneData _scene = null;
        EcsWorld _world = null;
        HexGridData _grid = null;

        public void Upd()
        {
            foreach (var i in spawnFilter)
            {
                ref var spawnData = ref spawnFilter.Get1(i);
                var hexPos = spawnData.hexPos;

                var isOutOfWidth = hexPos.x < 0 | hexPos.x >= _grid.Width;
                var isOutOfHeight = hexPos.y < 0 | hexPos.y >= _grid.Height;
                if (isOutOfWidth | isOutOfHeight) continue;

                var isChet = hexPos.IsChet();
                var isOutOfChetWidth = hexPos.x >= _grid.Width - 1;
                if (isChet & isOutOfChetWidth) continue;

                //Init Entity
                var ent = _world.NewEntity();

                ref var ballData = ref ent.Get<BallData>();
                ballData.hexPos = hexPos;
                ballData.BallID = spawnData.BallID;

                if (spawnData.isPlayerBall)
                {
                    ent.Get<BlopedBallTag>();
                }

                _grid.data[hexPos.x, hexPos.y] = ent;

                //Init GameObject
                var go = MonoBehaviour.Instantiate(_stData.balls[spawnData.BallID].Ball, _scene.BallsArea);
                var tf = go.transform;
                tf.localPosition = hexPos.ToWorldPos();
                var entID = go.AddComponent<EntityID>();
                entID.SetEntity(ent);

                Debug.DrawLine(tf.position, tf.position + Vector3.one, Color.red, 1f);
            }

            foreach (var i in spawnFilter)
            {
                var ent = spawnFilter.GetEntity(i);
                ent.Destroy();
            }
        }
    }
}