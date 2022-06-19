using Components;
using Leopotam.EcsLite;

namespace Systems {
    sealed class JumpSystem : IEcsRunSystem {
        public void Run(EcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            var filter = world.Filter<SwipeEvent>().End();
            var filterPlayer = world.Filter<PlayerTag>().Inc<Rigidbody>().Inc<Components.Player>().End();

            var swipeEventPool = world.GetPool<SwipeEvent>();
            var rigibodyPool = world.GetPool<Rigidbody>();
            var playerPool = world.GetPool<Components.Player>();
            foreach (int entity in filter)
            {
                ref SwipeEvent swipeEvent = ref swipeEventPool.Get(entity);
                if (swipeEvent.Type != TypeSwipe.UP) continue;
                foreach(int playerEntity in filterPlayer)
                {
                    ref Components.Player player = ref playerPool.Get(playerEntity);
                    ref Rigidbody rb = ref rigibodyPool.Get(playerEntity);
                    rb.Value.AddForce(UnityEngine.Vector3.up * player.JumpForce, UnityEngine.ForceMode.Acceleration);
                }
            }
        }
    }
}