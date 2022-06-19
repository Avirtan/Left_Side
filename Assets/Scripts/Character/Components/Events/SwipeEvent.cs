namespace Components
{
    public struct SwipeEvent
    {
        public TypeSwipe Type;
    }

    public enum TypeSwipe
    {
        UP,
        DOWN,
        RIGHT,
        LEFT
    }
}