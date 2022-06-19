using System;

namespace Components {
    [Serializable]
    public struct Player
    {
        public bool IsOnGround;
        public float JumpForce;
        public UnityEngine.GameObject Target;
    }
}