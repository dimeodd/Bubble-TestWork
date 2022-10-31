using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BallHelper
{

    public static Vector2 IndexToPos(int x, int y)
    {
        var isChet = IsChet(y);
        Vector2 pos;

        if (isChet)
            pos = new Vector2(x + 0.5f, -y * StaticData.sin60);
        else
            pos = new Vector2(x, -y * StaticData.sin60);

        return pos;
    }


    public static Vector2 GetBallPositionByHit(RaycastHit2D hit)
    {
        var otherPos = (Vector2)hit.transform.position;
        return GetBallPositionByPositions(hit.point, otherPos);
    }

    public static Vector2 GetBallPositionByPositions(Vector2 myPos, Vector2 otherPos)
    {
        var dir = myPos - otherPos;
        var grad = Vector2.SignedAngle(Vector2.up, dir);
        if (grad < 0) grad += 360;

        Vector2 aimBallPos;
        if (grad < 60)
            aimBallPos = otherPos + new Vector2(-StaticData.cos60, StaticData.sin60);
        else if (grad < 120)
            aimBallPos = otherPos + Vector2.left;
        else if (grad < 180)
            aimBallPos = otherPos + new Vector2(-StaticData.cos60, -StaticData.sin60);
        else if (grad < 240)
            aimBallPos = otherPos + new Vector2(StaticData.cos60, -StaticData.sin60);
        else if (grad < 300)
            aimBallPos = otherPos + Vector2.right;
        else // grad < 360
            aimBallPos = otherPos + new Vector2(StaticData.cos60, StaticData.sin60);

        return aimBallPos;
    }

    public static Vector2Int[] GetClosestIndexesByIndex(int otherX, int otherY)
    {
        Vector2Int[] poses = new Vector2Int[6];
        var isChet = IsChet(otherY);

        poses[0] = new Vector2Int(otherX - 1, otherY);
        poses[1] = new Vector2Int(otherX + 1, otherY);
        if (isChet) otherX++;
        poses[2] = new Vector2Int(otherX - 1, otherY - 1);
        poses[3] = new Vector2Int(otherX, otherY - 1);
        poses[4] = new Vector2Int(otherX - 1, otherY + 1);
        poses[5] = new Vector2Int(otherX, otherY + 1);

        return poses;
    }
    public static Vector2Int GetBallIndexByIndex(int otherX, int otherY, Vector2Int dir)
    {
        var isChet = IsChet(otherY);

        Vector2Int pos = new Vector2Int();
        if (dir.x < 0)
        {
            if (dir.y > 0)
                pos = new Vector2Int(otherX - 1, otherY - 1);
            else if (dir.y == 0)
                pos = new Vector2Int(otherX - 1, otherY);
            else if (dir.y < 0)
                pos = new Vector2Int(otherX - 1, otherY + 1);
        }
        else
        {
            if (dir.y > 0)
                pos = new Vector2Int(otherX, otherY + 1);
            else if (dir.y == 0)
                pos = new Vector2Int(otherX + 1, otherY);
            else if (dir.y < 0)
                pos = new Vector2Int(otherX, otherY - 1);
        }
        if (isChet & pos.y != 0) pos.x++;

        return pos;
    }
    public static Vector2Int GetBallIndexByPositions(Vector2 myPos, Vector2 otherPos, int otherX, int otherY)
    {
        var isChet = IsChet(otherY);

        var dir = myPos - otherPos;
        var grad = Vector2.SignedAngle(Vector2.up, dir);
        if (grad < 0) grad += 360;


        Vector2Int pos = new Vector2Int();
        if (grad < 60)
            pos = new Vector2Int(otherX - 1, otherY - 1);
        else if (grad < 120)
            pos = new Vector2Int(otherX - 1, otherY);
        else if (grad < 180)
            pos = new Vector2Int(otherX - 1, otherY + 1);
        else if (grad < 240)
            pos = new Vector2Int(otherX, otherY + 1);
        else if (grad < 300)
            pos = new Vector2Int(otherX + 1, otherY);
        else // grad < 360
            pos = new Vector2Int(otherX, otherY - 1);

        if (isChet & pos.y != otherY) pos.x++;

        return pos;
    }

    public static bool IsChet(int value)
    {
        return value % 2 > 0;
    }
}

public enum HexType
{
    LEFT_UP,
    LEFT,
    LEFT_DOWN,
    RIGHT_DOWN,
    RIGHT,
    RIGHT_UP
}
