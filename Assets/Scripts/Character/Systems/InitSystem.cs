using Components;
using Leopotam.EcsLite;

namespace Systems {
    sealed class InitSystem : IEcsRunSystem {
        public void Run(EcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            var filter = world.Filter<InitEvent>().Inc<RefEntity>().End();

            var refEntityPool = world.GetPool<RefEntity>();
            var initEventPool = world.GetPool<InitEvent>();
            foreach (int entity in filter)
            {
                ref RefEntity refEntity = ref refEntityPool.Get(entity);
                refEntity.Value.Entity = entity;
                refEntity.Value.World = world;
                initEventPool.Del(entity);
            }
        }
    }
}