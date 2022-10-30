using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyEcs;
using EcsStructs;

public class PlayerBallScript : MonoBehaviour
{
    Entity _myEnt;
    EcsWorld _world;

    public void SetWorld(EcsWorld world) => _world = world;
    public void SetEntity(Entity ent) => _myEnt = ent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Void"))
            _myEnt.Get<DestroyTag>();

        if (other.CompareTag("Ball"))
        {
            ref var collideData = ref _myEnt.Get<BallCollideData>();
            collideData.other = other.gameObject;
            //FIXME система по обработке BallCollideData
        }
    }
}
