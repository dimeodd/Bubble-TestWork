using MyEcs;
using EcsStructs;

namespace EcsSystems
{
    public class GridCreatorSystem : IInit
    {
        HexGridData grid = null;
        StaticData _stData = null;

        public void Init()
        {
            var w = _stData.GridWidth;
            var h = _stData.GridHeight;

            grid.data = new Entity[w, h];
            grid.Width = w;
            grid.Height = h;
        }
    }
}