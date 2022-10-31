using UnityEngine;
using System;

namespace HexMap
{

    //HexMap
    // 0. [a1][a2][a3][a4]  (a1)(a2)(a3)(a4)
    // 1. [b1][b2][b3][b4]    (b1)(b2)(b3)
    // 2. [c1][c2][c3][c4]  (c1)(c2)(c3)(a4)
    public class HexVector
    {
        public int x;
        public int y;


        public HexVector(Vector2Int pos) : this(pos.x, pos.y) { }
        public HexVector(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public HexVector[] GetNearHex()
        {
            return new HexVector[]{
                MoveTo(1,HexType.LEFT_UP),
                MoveTo(1,HexType.LEFT),
                MoveTo(1,HexType.LEFT_DOWN),
                MoveTo(1,HexType.RIGHT_UP),
                MoveTo(1,HexType.RIGHT),
                MoveTo(1,HexType.RIGHT_DOWN)
            };
        }

        public static bool isNear(HexVector a, HexVector b)
        {
            //aChet
            //  bb
            // bab
            //  bb

            //!aChet
            // bb
            // bab
            // bb

            if (a.y == b.y)
                return Mathf.Abs(a.x - b.x) < 2;

            var aIsChet = BallHelper.IsChet(a.y);

            if (Mathf.Abs(a.y - b.y) == 1)
            {
                var offset = a.x - b.x;
                if (aIsChet)
                    return offset == 0 | offset == 1;
                else
                    return offset == 0 | offset == -1;
            }

            return false;
        }

        public static HexType getDirection(Vector2 from, Vector2 to)
        {
            var dir = to - from;
            var deg = Vector2.SignedAngle(Vector2.up, dir);
            if (deg < 0) deg += 360;

            if (deg < 60)
                return HexType.LEFT_UP;
            else if (deg < 120)
                return HexType.LEFT;
            else if (deg < 180)
                return HexType.LEFT_DOWN;
            else if (deg < 240)
                return HexType.RIGHT_DOWN;
            else if (deg < 300)
                return HexType.RIGHT;
            else // grad < 360
                return HexType.RIGHT_UP;
        }

        public static HexType getDirection(HexVector from, HexVector to)
        {
            if (!isNear(from, to) || from == to) throw new ArgumentOutOfRangeException();

            var aIsChet = BallHelper.IsChet(from.y);

            if (from.y == to.y)
            {
                if (from.x < to.x)
                    return HexType.RIGHT;
                else
                    return HexType.LEFT;
            }

            if (from.y < to.y) //b is Up
            {
                if (aIsChet)
                {
                    if (from.x == to.x)
                    {
                        return HexType.LEFT_UP;
                    }
                    else
                    {
                        return HexType.RIGHT_UP;
                    }
                }
                else
                {
                    if (from.x == to.x)
                    {
                        return HexType.RIGHT_UP;
                    }
                    else
                    {
                        return HexType.LEFT_UP;
                    }

                }
            }
            else //b is Down
            {
                if (aIsChet)
                {
                    if (from.x == to.x)
                    {
                        return HexType.LEFT_DOWN;
                    }
                    else
                    {
                        return HexType.RIGHT_DOWN;
                    }
                }
                else
                {
                    if (from.x == to.x)
                    {
                        return HexType.RIGHT_DOWN;
                    }
                    else
                    {
                        return HexType.LEFT_DOWN;
                    }
                }
            }
        }

        public HexVector MoveTo(int dist, HexType direction)
        {
            var isChet = IsChet();
            var x = this.x;
            var y = this.y;

            switch (direction)
            {
                case HexType.LEFT_UP:
                    x -= dist / 2; if (!isChet) x--;
                    y -= dist;
                    break;

                case HexType.LEFT:
                    x -= dist;
                    break;

                case HexType.LEFT_DOWN:
                    x -= dist / 2; if (!isChet) x--;
                    y += dist;
                    break;

                case HexType.RIGHT_DOWN:
                    x += dist / 2; if (isChet) x++;
                    y += dist;
                    break;

                case HexType.RIGHT:
                    x += dist;
                    break;

                case HexType.RIGHT_UP:
                    x += dist / 2; if (isChet) x++;
                    y -= dist;
                    break;
            }

            return new HexVector(x, y);
        }

        public Vector2 ToWorldPos()
        {
            var isChet = IsChet();
            Vector2 pos;

            if (isChet)
                pos = new Vector2(x + 0.5f, -y * StaticData.sin60);
            else
                pos = new Vector2(x, -y * StaticData.sin60);

            return pos;
        }


        public bool IsChet() => BallHelper.IsChet(y);

        public static implicit operator Vector2Int(HexVector hexVec)
        {
            return new Vector2Int(hexVec.x, hexVec.y);
        }
    }
}