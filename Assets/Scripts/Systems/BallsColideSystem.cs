using MyEcs;
using MyEcs.GoPool;
using EcsStructs;

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
                var entityID = collideData.other.GetComponent<EntityID>();
                var otherEnt = entityID.GetEntity();

                var otherBallData = otherEnt.Get<BallData>();
                var otherPos = collideData.other.transform.position;

                var spawnPos = BallHelper.GetBallIndexByPositions(myPos, otherPos, otherBallData.x, otherBallData.y);

                var ent = _world.NewEntity();
                ref var spawnData = ref ent.Get<BallSpawnData>();
                spawnData.BallID = ballData.BallID;
                spawnData.x = spawnPos.x;
                spawnData.y = spawnPos.y;
            }

        }
    }
}