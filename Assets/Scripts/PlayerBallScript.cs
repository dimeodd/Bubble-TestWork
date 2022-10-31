using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyEcs;
using EcsStructs;

public class PlayerBallScript : MonoBehaviour
{
    Entity _myEnt;
    EcsWorld _world;
    bool isCollided;

    public void SetWorld(EcsWorld world) => _world = world;
    public void SetEntity(Entity ent) => _myEnt = ent;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (_myEnt.IsDestroyed()) return;

        if (other.gameObject.CompareTag("Void"))
        {
            _myEnt.Get<DestroyTag>();
        }

        if (other.gameObject.CompareTag("Ball"))
        {
            GetComponent<Rigidbody2D>().simulated = false;

            ref var collideData = ref _myEnt.Get<BallCollideData>();
            collideData.other = other.gameObject;

            _myEnt.Get<DestroyTag>();
        }
    }
}
