using MyEcs;
using EcsStructs;

namespace EcsSystems
{
    public class WinSystem : IUpd
    {
        Filter<CheckWinTag> winFilter = null;
        Filter<InputData> inputFilter = null;
        HexGridData _grid;
        SceneData _scene;

        public void Upd()
        {
            foreach (var i in winFilter)
            {
                var h = _grid.Height;
                var w = _grid.Width;

                for (int x = 0; x < w; x++)
                {
                    for (int y = 0; y < h; y++)
                    {
                        ref var ent = ref _grid.data[x, y];
                        if (!ent.IsDestroyed())
                            return;
                    }
                }

                //Если добрались сюда, значит всё в гриде уничтожено

                _scene.WinWindow.SetActive(true);
                foreach (var j in inputFilter)
                {
                    var ent = inputFilter.GetEntity(j);
                    ent.Get<BlockInputTag>();
                }
                break;
            }


        }
    }
}