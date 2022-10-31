using MyEcs;
using MyEcs.GoPool;
using EcsStructs;
using HexMap;

namespace EcsSystems
{
    public class BallsColideSystem : IUpd
    {
        Filter<BallCollideData, PlayerBallData> filter = null;
        EcsWorld _world = null;

        public void Upd()
        {
            foreach (var i in filter)
            {
                ref var collideData = ref filter.Get1(i);
                ref var ballData = ref filter.Get2(i);

                var myPos = ballData.go.transform.position;
                var otherPos = collideData.other.transform.position;

                var otherEnt = collideData.other.GetComponent<EntityID>().GetEntity();
                var otherBallData = otherEnt.Get<BallData>();

                var dir = HexVector.getDirection(otherPos, myPos);
                var spawnPos = otherBallData.hexPos.MoveTo(1, dir);

                var ent = _world.NewEntity();
                ref var spawnData = ref ent.Get<BallSpawnData>();
                spawnData.BallID = ballData.BallID;
                spawnData.hexPos = spawnPos;
                spawnData.isPlayerBall = true;
            }

        }
    }
}