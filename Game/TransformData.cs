namespace Game
{
    public class TransformData
    {
        private Vector2 position;
        private float scale;
        private float rotation;

        public Vector2 Position { get => position; set => position = value; }
        public float Scale { get => scale; set => scale = value; }
        public float Rotation { get => rotation; set => rotation = value; }
    }
}
