using MyEcs;
using EcsStructs;

namespace EcsSystems
{
    public class BlopedBubbleSystem : IUpd
    {
        Filter<BlopedBallTag, BallData> filter = null;
        HexGridData _grid = null;

        public void Upd()
        {
            //TODO

            // var arr = hexPos.GetNearHex();

            // foreach (var nearHexVec in arr)
            // {
            //     isOutOfWidth = nearHexVec.x < 0 | nearHexVec.x >= _grid.Width;
            //     isOutOfHeight = nearHexVec.y < 0 | nearHexVec.y >= _grid.Height;
            //     if (isOutOfWidth | isOutOfHeight) continue;

            //     isChet = nearHexVec.IsChet();
            //     isOutOfChetWidth = nearHexVec.x >= _grid.Width - 1;
            //     if (isChet & isOutOfChetWidth) continue;

            //     var nearEnt = _grid.data[nearHexVec.x, nearHexVec.y];
            //     if (nearEnt.IsDestroyed() || !nearEnt.Contain<BallData>()) continue;

            //     var nearBallData = nearEnt.Get<BallData>();
            //     if (nearBallData.BallID == ballData.BallID) sameCount++;
            // }

            // if (sameCount > 2)
        }
    }
}