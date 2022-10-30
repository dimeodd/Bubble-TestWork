using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BallHelper
{
    public static Vector2 GetBallPositionByHit(RaycastHit2D hit)
    {
        var otherPos = (Vector2)hit.transform.position;
        var dir = hit.point - otherPos;
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
}
