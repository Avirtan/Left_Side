using Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Systems
{
    sealed class InputSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private int _entity;

        public void Init(EcsSystems systems)
        {
            _world = systems.GetWorld();
            _entity = _world.NewEntity();
            var inputs = _world.GetPool<PlayerInput>();
            inputs.Add(_entity);
            ref PlayerInput inputAction = ref inputs.Get(_entity);
            inputAction.Value = new PlayerInputAction();
            inputAction.EndPosition = Vector2.zero;
            inputAction.StartPosition = Vector2.zero;
            inputAction.DirectionThreshold = 0.7f;
            inputAction.Value.Player.Enable();
            inputAction.Value.Player.PrimaryContact.started += PrimaryContact_started;
            inputAction.Value.Player.PrimaryContact.canceled += PrimaryContact_end;
        }

        public void Run(EcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            var filter = world.Filter<PlayerInput>().End();
            var inputs = _world.GetPool<PlayerInput>();
            var swipeEvents = _world.GetPool<SwipeEvent>();
            foreach (int entity in filter)
            {
                ref PlayerInput inputAction = ref inputs.Get(entity);
                if (!inputAction.IsTouch && inputAction.EndPosition == Vector2.zero) continue;
                if (!inputAction.IsTouch)
                {
                    var directionSwipe = (inputAction.EndPosition - inputAction.StartPosition).normalized;
                    var swipeEventEntity = _world.NewEntity();
                    swipeEvents.Add(swipeEventEntity);
                    ref SwipeEvent swipeEvent = ref swipeEvents.Get(swipeEventEntity);
                    if (Vector3.Dot(directionSwipe, Vector3.up) > inputAction.DirectionThreshold)
                    {
                        swipeEvent.Type = TypeSwipe.UP;
                    }
                    else if (Vector3.Dot(directionSwipe, Vector3.down) > inputAction.DirectionThreshold)
                    {
                        swipeEvent.Type = TypeSwipe.DOWN;
                    }
                    else if (Vector3.Dot(directionSwipe, Vector3.left) > inputAction.DirectionThreshold)
                    {
                        swipeEvent.Type = TypeSwipe.LEFT;
                    }
                    else if (Vector3.Dot(directionSwipe, Vector3.right) > inputAction.DirectionThreshold)
                    {
                        swipeEvent.Type = TypeSwipe.RIGHT;
                    }
                    inputAction.EndPosition = Vector2.zero;
                }
            }
        }

        private void PrimaryContact_started(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            var inputs = _world.GetPool<PlayerInput>();
            ref PlayerInput inputActions = ref inputs.Get(_entity);
            inputActions.IsTouch = true;
            inputActions.StartPosition = inputActions.Value.Player.TouchPosition.ReadValue<Vector2>();
        }

        private void PrimaryContact_end(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            var inputs = _world.GetPool<PlayerInput>();
            ref PlayerInput inputActions = ref inputs.Get(_entity);
            inputActions.IsTouch = false;
            inputActions.EndPosition = inputActions.Value.Player.TouchPosition.ReadValue<Vector2>();
        }
    }
}