using MyEcs;
using EcsStructs;
using UnityEngine;

namespace EcsSystems
{
    public class PlayerBallSpawner : IUpd
    {
        Filter<PlayerBallSpawnData> spawnFilter = null;
        StaticData _stData = null;
        SceneData _scene = null;
        EcsWorld _world = null;

        public void Upd()
        {
            foreach (var i in spawnFilter)
            {
                ref var spawnData = ref spawnFilter.Get1(i);

                var ent = _world.NewEntity();

                ref var ballData = ref ent.Get<PlayerBallData>();
                ballData.BallID = spawnData.BallID;
                ballData.go = MonoBehaviour.Instantiate(_stData.PlayerBall, _scene.FirePoint.position, Quaternion.identity);

                var go = ballData.go;
                var ballScript = go.GetComponent<PlayerBallScript>();
                ballScript.SetWorld(_world);
                ballScript.SetEntity(ent);
                var tf = go.transform;
                tf.position = _scene.FirePoint.position;

                var subGo = MonoBehaviour.Instantiate(_stData.balls[spawnData.BallID].Ball, tf);
                subGo.GetComponent<Collider2D>().enabled = false;
            }
        }
    }
}