using Components;
using Leopotam.EcsLite;

namespace Systems
{
    sealed class RunSystem : IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            var filter = world.Filter<PlayerTag>().Inc<Rigidbody>().Inc<Components.Player>().Inc<Transform>().Inc<Speed>().End();
            var filterInput = world.Filter<PlayerInput>().End();
            var rigidbodys = world.GetPool<Rigidbody>();
            var transforms = world.GetPool<Transform>();
            var speeds = world.GetPool<Speed>();
            var inputPool = world.GetPool<PlayerInput>();
            var playerPool = world.GetPool<Components.Player>();
            foreach (int _entite in filterInput)
            {
                ref PlayerInput input = ref inputPool.Get(_entite);
                foreach (int entity in filter)
                {
                    ref Rigidbody rb = ref rigidbodys.Get(entity);
                    ref Transform transform = ref transforms.Get(entity);
                    ref Speed speed = ref speeds.Get(entity);
                    ref Components.Player player = ref playerPool.Get(entity);
                    var moveVector = input.Value.Player.Move.ReadValue<UnityEngine.Vector2>();
                    var velocity = new UnityEngine.Vector3(moveVector.x * speed.Value, rb.Value.velocity.y, moveVector.y * speed.Value);
                    rb.Value.velocity = velocity;
                    var rotateVector = input.Value.Player.Rotate.ReadValue<UnityEngine.Vector2>();
                    transform.Value.Rotate(0, rotateVector.x * 2, 0.0f);
                    var ClampY = UnityEngine.Mathf.Clamp(player.Target.transform.localPosition.y + rotateVector.y, 0, 5);
                    player.Target.transform.localPosition = new UnityEngine.Vector3(0, ClampY, 7);
                }
            }
        }
    }
}