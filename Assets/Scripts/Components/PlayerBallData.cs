using UnityEngine;
namespace EcsStructs
{
    public struct PlayerBallData
    {
        public int BallID;
        public GameObject go; //TODO remove
        public Rigidbody2D rigidbody;
    }
}