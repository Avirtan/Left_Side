using System;

namespace Components {
    [Serializable]
    public struct PlayerInput
    {
        public PlayerInputAction Value;
        public bool IsTouch;
        public UnityEngine.Vector2 StartPosition;
        public UnityEngine.Vector2 EndPosition;
        public float DirectionThreshold;
    }
}