using Components;
using Leopotam.EcsLite;

namespace Systems
{
    sealed class AnimationSystem : IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            var filter = world.Filter<PlayerTag>().Inc<Animator>().Inc<Components.Player>().Inc<Rigidbody>().End();

            var animatorPool = world.GetPool<Animator>();
            var playerPool = world.GetPool<Components.Player>();
            var rbPool = world.GetPool<Rigidbody>();
            foreach (int entity in filter)
            {
                ref Animator animator = ref animatorPool.Get(entity);
                ref Rigidbody rb = ref rbPool.Get(entity);
                ref Components.Player player = ref playerPool.Get(entity);
                animator.Value.SetBool("Jump", !player.IsOnGround);
                animator.Value.SetFloat("Speed", rb.Value.velocity.z);
            }
        }
    }
}