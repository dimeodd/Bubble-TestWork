using MyEcs;
using EcsStructs;
using HexMap;
using System;

namespace EcsSystems
{
    public class BlopedBubbleSystem : IUpd
    {
        Filter<MarkedBallTag> markedBallFilter = null;

        Filter<BlopBallCheckData> filter = null;
        HexGridData _grid = null;
        StaticData _stData = null;
        SceneData _scene = null;

        public void Upd()
        {
            foreach (var j in filter)
            {
                var checkData = filter.Get1(j);

                var blopCount = 0;

                MarkSameBalls(checkData.ballEnt, ref blopCount);

                if (blopCount >= _stData.CountRequredForBlop)
                {
                    _scene.BloopSound.Play();
                    foreach (var i in markedBallFilter)
                    {
                        var destrEnt = markedBallFilter.GetEntity(i);
                        destrEnt.Get<DestroyTag>();
                    }
                }
            }

            foreach (var i in filter)
            {
                var ent = filter.GetEntity(i);
                ent.Del<BlopBallCheckData>();
            }
        }

        //Схож с алгоритмом заливки пикселей, только вместо замены цвета, он будет добавлять тег
        // 0. Есть список исходных точек (SourcePointList)
        // 1. Брерут пиксель из SourcePointList
        // 2. Ищут самую левый пиксель в этом ряду. Идут пока не будет другой цвет.
        // 3. Идут от него вправо (и сразу заменяют цвет), проверяя верхний и нижний пиксель. Если цвет похож (У внерхнего/нижнего), то добавляют пиксель в SourcePointList
        // 4. Если есть ещё точки в SourcePointList, то GOTO 1
        void MarkSameBalls(Entity source, ref int blopCount)
        {
            var sourceList = new MyList<Entity>(4);
            sourceList.Add(source);

            while (sourceList.Count > 0)
            {

                #region  1

                var sourceBallEnt = sourceList._data[0];
                sourceList.DeleteReplaced(0);

                var sourceBall = sourceBallEnt.Get<BallData>();
                var sourceBallID = sourceBall.BallID;
                var hexVec = sourceBall.hexPos;
                var isChet = !hexVec.IsChet();

                #endregion

                // 2
                while (hexVec.x > 0 && IsRightBall(_grid.data[hexVec.x - 1, hexVec.y], sourceBallID))
                    hexVec.x--;

                #region  3

                var haveUpSpace = hexVec.y < _grid.Height - 1;
                var haveDownSpace = hexVec.y > 0;
                var upIsSame = false;
                var downIsSame = false;

                //HexStuff
                // | () |   X..   (X)(.)
                // |()()|   .0.  (.)(0)(.)
                // | () |   X..   (X)(.)
                if (isChet & hexVec.x > 0)
                {
                    if (haveUpSpace)
                    {
                        var a = hexVec.MoveTo(1, HexType.LEFT_DOWN);
                        var upEnt = _grid.data[a.x, a.y];
                        if (IsRightBall(upEnt, sourceBallID))
                        {
                            upIsSame = true;
                            sourceList.Add(upEnt);
                        }
                    }
                    if (haveDownSpace)
                    {
                        var a = hexVec.MoveTo(1, HexType.LEFT_UP);
                        var downEnt = _grid.data[a.x, a.y];
                        if (IsRightBall(downEnt, sourceBallID))
                        {
                            downIsSame = true;
                            sourceList.Add(downEnt);
                        }
                    }
                }


                // .X.
                // .0.
                // .X.
                do
                {
                    var currEnt = _grid.data[hexVec.x, hexVec.y];
                    currEnt.Get<MarkedBallTag>();
                    blopCount++;

                    if (haveUpSpace)
                    {
                        var upEnt = _grid.data[hexVec.x, hexVec.y + 1];
                        if (IsRightBall(upEnt, sourceBallID) & !upIsSame)
                        {
                            sourceList.Add(upEnt);
                            upIsSame = true;
                        }
                        else
                        {
                            upIsSame = false;
                        }
                    }
                    if (haveDownSpace)
                    {
                        var downEnt = _grid.data[hexVec.x, hexVec.y - 1];
                        if (IsRightBall(downEnt, sourceBallID) & !downIsSame)
                        {
                            sourceList.Add(downEnt);
                            downIsSame = true;
                        }
                        else
                        {
                            downIsSame = false;
                        }
                    }

                    hexVec.x++;
                }
                while (hexVec.x < _grid.Width && IsRightBall(_grid.data[hexVec.x, hexVec.y], sourceBallID));

                //HexStuff
                // |()()|   ..X   (.)(X)
                // | () |   .0.  (.)(0)(.)
                // |()()|   ..X   (.)(X)
                hexVec.x--;
                if (!isChet & hexVec.x < _grid.Width - 1)
                {
                    if (haveUpSpace)
                    {
                        var a = hexVec.MoveTo(1, HexType.RIGHT_DOWN);
                        var upEnt = _grid.data[a.x, a.y];
                        if (IsRightBall(upEnt, sourceBallID) & !upIsSame)
                        {
                            upIsSame = true;
                            sourceList.Add(upEnt);
                        }
                    }
                    if (haveDownSpace)
                    {
                        var a = hexVec.MoveTo(1, HexType.RIGHT_UP);
                        var downEnt = _grid.data[a.x, a.y];
                        if (IsRightBall(downEnt, sourceBallID) & !downIsSame)
                        {
                            sourceList.Add(downEnt);
                        }
                    }
                }




                #endregion

            }

        }

        bool IsRightBall(Entity ballEnt, int BallID)
        {
            if (ballEnt.IsDestroyed() || ballEnt.Contain<MarkedBallTag>()) return false;

            var ball = ballEnt.Get<BallData>();

            return BallID == ball.BallID;
        }
    }
}