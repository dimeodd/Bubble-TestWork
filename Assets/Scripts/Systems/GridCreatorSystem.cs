using MyEcs;
using EcsStructs;

namespace EcsSystems
{
    public class GridCreatorSystem : IInit
    {
        StaticData _stData = null;
        HexGridData _grid = null;

        public void Init()
        {
            var w = _stData.GridWidth;
            var h = _stData.GridHeight;

            _grid.data = new Entity[w, h];
            _grid.Width = w;
            _grid.Height = h;
        }
    }
}