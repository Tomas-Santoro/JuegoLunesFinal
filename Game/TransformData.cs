namespace Game
{
    public class TransformData
    {
        private Vector2 position;
        private float scale;
        private float rotation;

        public TransformData(Vector2 position, float scale, float rotation)
        {
            this.position = position;
            this.scale = scale;
            this.rotation = rotation;
        } 
        public Vector2 Position { get => position; set => position = value; }
        public float Scale { get => scale; set => scale = value; }
        public float Rotation { get => rotation; set => rotation = value; }
    }
}
