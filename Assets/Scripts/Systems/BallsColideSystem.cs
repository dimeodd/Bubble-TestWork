using MyEcs;
using MyEcs.GoPool;
using EcsStructs;

namespace EcsSystems
{
    public class BallsColideSystem : IUpd
    {
        Filter<BallCollideData, PlayerBallData> filter = null;

        public void Upd()
        {
            foreach (var i in filter)
            {
                ref var collideData = ref filter.Get1(i);
                ref var ballData = ref filter.Get2(i);

                var ballPos = ballData.go.transform.position;
                var entityID = collideData.other.GetComponent<EntityID>();
                var otherEnt = entityID.GetEntity();

                var otherBallData = otherEnt.Get<BallData>();

                var isChet = otherBallData.y % 2 > 0;

                var otherPos = collideData.other.transform.position;


            }

        }
    }
}