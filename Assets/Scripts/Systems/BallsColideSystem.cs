using MyEcs;
using MyEcs.GoPool;
using EcsStructs;
using HexMap;
using UnityEngine;

namespace EcsSystems
{
    public class BallsColideSystem : IUpd
    {
        Filter<BallCollideData, PlayerBallData, GoEntityProvider> filter = null;
        EcsWorld _world = null;
        HexGridData _grid = null;

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

                //Фикс ошибок физического движка от юнити. Очень редко, но бывают
                if (spawnPos.x < 0)
                {
                    spawnPos.x = 0;
                }
                if (spawnPos.x >= _grid.Width ||
                spawnPos.IsChet() & spawnPos.x >= _grid.Width - 1)
                {
                    spawnPos.x--;
                }

                var ent = _world.NewEntity();
                ref var spawnData = ref ent.Get<BallSpawnData>();
                spawnData.BallID = ballData.BallID;
                spawnData.hexPos = spawnPos;
                spawnData.isPlayerBall = true;
            }

        }
    }
}