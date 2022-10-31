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
                var BallID = spawnFilter.Get1(i).BallID;

                //Init Entity
                var ent = _world.NewEntity();

                ref var ballData = ref ent.Get<PlayerBallData>();
                ballData.BallID = BallID;
                ballData.go = MonoBehaviour.Instantiate(_stData.PlayerBall, _scene.FirePoint.position, Quaternion.identity);
                ballData.rigidbody = ballData.go.GetComponent<Rigidbody2D>();

                //Init GameObject
                var go = ballData.go;

                var ballScript = go.GetComponent<PlayerBallScript>();
                ballScript.SetWorld(_world);
                ballScript.SetEntity(ent);

                var tf = go.transform;
                tf.position = _scene.FirePoint.position;

                //Раскрашивание шарика
                var subGo = MonoBehaviour.Instantiate(_stData.balls[BallID].Ball, tf);
                subGo.GetComponent<Collider2D>().enabled = false;
            }
        }
    }
}