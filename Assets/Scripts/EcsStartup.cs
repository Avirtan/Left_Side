using Leopotam.EcsLite;
using Leopotam.EcsLite.ExtendedSystems;
using UnityEngine;
using Voody.UniLeo.Lite;

namespace Client
{
    sealed class EcsStartup : MonoBehaviour
    {
        EcsSystems _systems;
        EcsSystems _fixedSystems;

        private void Start()
        {
            EcsWorld world = new EcsWorld();
            _systems = new EcsSystems(world);
            _fixedSystems = new EcsSystems(world);
            _systems
                .Add(new Systems.InitSystem())
                .Add(new Systems.InputSystem())
                .Add(new Systems.AnimationSystem())
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                .ConvertScene()
                .Init();
            _fixedSystems
                .Add(new Systems.RunSystem())
                .Add(new Systems.JumpSystem())
                .DelHere<Components.SwipeEvent>()
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                .Init();

        }

        private void Update()
        {
            _systems?.Run();
        }
        private void FixedUpdate()
        {
            _fixedSystems?.Run();
        }

        private void OnDestroy()
        {
            if (_systems != null)
            {
                _systems.Destroy();
                _fixedSystems.Destroy();
                // add here cleanup for custom worlds, for example:
                // _systems.GetWorld ("events").Destroy ();
                _systems.GetWorld().Destroy();
                _systems = null;
            }
        }
    }
}