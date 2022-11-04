using MyEcs;
using EcsStructs;

namespace EcsSystems
{
    public class WinSystem : IUpd
    {
        Filter<BlockInputTag> blockFilter = null;
        Filter<CheckWinTag> winFilter = null;
        HexGridData _grid;
        SceneData _scene;

        public void Upd()
        {
            blockFilter.GetEnumerator();
            if (blockFilter.Count > 0) return;

            foreach (var i in winFilter)
            {
                var h = _grid.Height;
                var w = _grid.Width;

                for (int x = 0; x < w; x++)
                {
                    for (int y = 0; y < h; y++)
                    {
                        ref var entFromGrid = ref _grid.data[x, y];
                        if (!entFromGrid.IsDestroyed())
                            return;
                    }
                }

                //Если добрались сюда, значит всё в гриде уничтожено

                _scene.WinWindow.SetActive(true);
                var ent = winFilter.GetEntity(i);
                ent.Get<BlockInputTag>();

                break;
            }
        }
    }
}