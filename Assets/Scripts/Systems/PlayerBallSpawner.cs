using MyEcs;
using MyEcs.GoPool;
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

            //HexMap
            // 0. [a1][a2][a3][a4]  (a1)(a2)(a3)(a4)
            // 1. [b1][b2][b3][b4]    (b1)(b2)(b3)
            // 2. [c1][c2][c3][c4]  (c1)(c2)(c3)(a4)

            foreach (var i in spawnFilter)
            {
                var BallID = spawnFilter.Get1(i).BallID;

                //Init Entity
                var ent = _world.NewEntity();
                var go = MonoBehaviour.Instantiate(_stData.PlayerBall, _scene.FirePoint.position, Quaternion.identity);

                ref var ballData = ref ent.Get<PlayerBallData>();
                ballData.BallID = BallID;
                ballData.rigidbody = go.GetComponent<Rigidbody2D>();

                //Init GameObject
                var entID = go.AddComponent<EntityID>();
                entID.SetEntity(ent);

                var ballScript = go.GetComponent<PlayerBallScript>();
                ballScript.SetWorld(_world);
                ballScript.SetEntity(ent);

                var tf = go.transform;
                tf.position = _scene.FirePoint.position;

                //Раскрашивание шарика
                var subGo = MonoBehaviour.Instantiate(_stData.balls[BallID].Ball, tf);
                subGo.GetComponent<Collider2D>().enabled = false;
            }

            foreach (var i in spawnFilter)
            {
                var ent = spawnFilter.GetEntity(i);
                ent.Destroy();
            }
        }
    }
}