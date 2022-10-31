using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BallHelper
{

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

    public static Vector2Int GetBallIndexByPositions(Vector2 myPos, Vector2 otherPos, int otherX, int otherY)
    {
        var isChet = otherY % 2 > 0;

        var dir = myPos - otherPos;
        var grad = Vector2.SignedAngle(Vector2.up, dir);
        if (grad < 0) grad += 360;


        Vector2Int pos;
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
}
