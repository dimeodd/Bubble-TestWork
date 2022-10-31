using MyEcs;
using EcsStructs;

namespace EcsSystems
{
    public class BangBubbleSystem : IUpd
    {
        Filter<BlopedBallTag, BallData> filter = null;
        HexGridData _grid = null;

        public void Upd()
        {

        }
    }
}