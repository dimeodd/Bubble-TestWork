using MyEcs;
using EcsStructs;
using HexMap;
using System;

namespace EcsSystems
{
    public class BlopedBubbleSystem : IUpd
    {
        Filter<BlopedBallTag, BallData> filter = null;
        HexGridData _grid = null;

        public void Upd()
        {
            foreach (var i in filter)
            {
                var ent = filter.GetEntity(i);
                MarkSameBalls(ent);
            }
        }

        //Схож с алгоритмом заливки пикселей, только вместо замены цвета, он будет добавлять тег
        // 0. Есть список исходных точек (SourcePointList)
        // 1. Брерут пиксель из SourcePointList
        // 2. Ищут самую левый пиксель в этом ряду. Идут пока не будет другой цвет.
        // 3. Идут от него вправо (и сразу заменяют цвет), проверяя верхний и нижний пиксель. Если цвет похож (У внерхнего/нижнего), то добавляют пиксель в SourcePointList
        // 4. Если есть ещё точки в SourcePointList, то GOTO 1
        void MarkSameBalls(Entity source)
        {
            var SourceList = new MyList<Entity>(4);
            SourceList.Add(source);

            while (SourceList.Count > 0)
            {

                #region  1

                var sourceBallEnt = SourceList._data[0];
                SourceList.DeleteReplaced(0);

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
                // Xm.   (X)(m)                    | () |
                // .0.  (.)(0)(.) - isNotChet Row  |()()|
                // Xm.   (X)(m)                    | () |
                if (!isChet & hexVec.x > 0)
                {
                    if (haveUpSpace)
                    {
                        var upEnt = _grid.data[hexVec.x - 1, hexVec.y + 1];
                        if (IsRightBall(upEnt, sourceBallID))
                        {
                            upIsSame = true;
                            SourceList.Add(upEnt);
                        }
                    }
                    if (haveDownSpace)
                    {
                        var downEnt = _grid.data[hexVec.x - 1, hexVec.y - 1];
                        if (IsRightBall(downEnt, sourceBallID))
                        {
                            downIsSame = true;
                            SourceList.Add(downEnt);
                        }
                    }
                }

                // .X.
                // .0.
                // .X.
                do
                {
                    try
                    {
                        var currEnt = _grid.data[hexVec.x, hexVec.y];
                        currEnt.Get<DestroyTag>();
                    }
                    catch (Exception ex)
                    {
                        UnityEngine.Debug.LogError("values " + hexVec.x + "_" + hexVec.y);
                        throw ex;
                    }

                    if (haveUpSpace)
                    {
                        var upEnt = _grid.data[hexVec.x, hexVec.y + 1];
                        if (IsRightBall(upEnt, sourceBallID) & !upIsSame)
                        {
                            SourceList.Add(upEnt);
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
                            SourceList.Add(downEnt);
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
                // .mX     (m)(X)                |()()|
                // .0.   (.)(0)(.) - isChet Row  | () |
                // .mX     (m)(X)                |()()|
                if (isChet & hexVec.x < _grid.Width - 1)
                {
                    if (haveUpSpace)
                    {
                        var upEnt = _grid.data[hexVec.x + 1, hexVec.y + 1];
                        if (IsRightBall(upEnt, sourceBallID))
                        {
                            if (!upIsSame)
                                SourceList.Add(upEnt);
                        }
                    }
                    if (haveDownSpace)
                    {
                        var downEnt = _grid.data[hexVec.x + 1, hexVec.y - 1];
                        if (IsRightBall(downEnt, sourceBallID))
                        {
                            if (downIsSame)
                                SourceList.Add(downEnt);
                        }
                    }
                }

                #endregion

            }

        }

        bool IsRightBall(Entity ballEnt, int BallID)
        {
            if (ballEnt.IsDestroyed() || ballEnt.Contain<DestroyTag>()) return false;

            var ball = ballEnt.Get<BallData>();

            return BallID == ball.BallID;
        }
    }
}