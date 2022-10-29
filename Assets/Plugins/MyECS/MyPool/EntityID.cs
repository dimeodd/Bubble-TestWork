using UnityEngine;

namespace MyEcs.GoPool
{
    [SelectionBase]
    public class EntityID : MonoBehaviour
    {
        Entity entity;

        public void SetEntity(Entity ent)
        {
            if (ent.IsDestroyed()) throw new System.Exception("Null Entity");

            entity = ent;
            ref var entProv = ref ent.Get<EcsStructs.GoEntityProvider>();
            entProv.provider = this;
        }

        public void SetEntity(Entity ent, MyPool pool)
        {
            if (ent.IsDestroyed()) throw new System.Exception("Null Entity");

            entity = ent;
            ref var entProv = ref ent.Get<EcsStructs.GoEntityProvider>();
            entProv.provider = this;
            entProv.pool = pool;
        }

        internal void Clean()
        {
            entity = Entity.NULL;
        }

        public Entity GetEntity()
        {
            if (entity.IsDestroyed()) throw new System.Exception("Null Entity");

            return entity;
        }

    }
}

namespace EcsStructs
{
    using MyEcs.GoPool;

    public struct GoEntityProvider
    {
        public EntityID provider;
        public MyPool pool;

        public void Recycle()
        {
            provider.Clean();
            provider.gameObject.SetActive(false);

            if (pool != null)
            {
                pool.Storage(provider.gameObject);
            }
            else
            {
                MonoBehaviour.Destroy(provider.gameObject, 0.01f);
            }
        }
    }
}