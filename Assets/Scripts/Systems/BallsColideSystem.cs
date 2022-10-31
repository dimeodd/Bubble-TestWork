using MyEcs;
using MyEcs.GoPool;
using EcsStructs;
using HexMap;

namespace EcsSystems
{
    public class BallsColideSystem : IUpd
    {
        Filter<BallCollideData, PlayerBallData, GoEntityProvider> filter = null;
        SceneData _scene = null;
        EcsWorld _world = null;

        public void Upd()
        {
            foreach (var i in filter)
            {
                ref var collideData = ref filter.Get1(i);
                ref var ballData = ref filter.Get2(i);
                ref var provider = ref filter.Get3(i);

                var myPos = provider.provider.transform.position;
                var otherPos = collideData.other.transform.position;

                var otherEnt = collideData.other.GetComponent<EntityID>().GetEntity();
                var otherBallData = otherEnt.Get<BallData>();

                var dir = HexVector.getDirection(otherPos, myPos);
                var spawnPos = otherBallData.hexPos.MoveTo(1, dir);

                // UnityEngine.Vector2 wPos = (UnityEngine.Vector3)spawnPos.ToWorldPos() + _scene.BallsArea.position;
                // UnityEngine.Debug.DrawLine(otherPos, myPos, UnityEngine.Color.white, 2f);
                // UnityEngine.Debug.DrawLine(wPos, wPos + UnityEngine.Vector2.right, UnityEngine.Color.yellow, 2f);

                var ent = _world.NewEntity();
                ref var spawnData = ref ent.Get<BallSpawnData>();
                spawnData.BallID = ballData.BallID;
                spawnData.hexPos = spawnPos;
                spawnData.isPlayerBall = true;
            }

        }
    }
}