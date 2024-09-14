namespace Game
{
    public struct Vector2
    {
        public float X { get; private set; }
        public float Y { get; private set; }

        public static Vector2 One => new Vector2(1, 1);
        public static Vector2 Zero => new Vector2(0, 0);

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static Vector2 operator + (Vector2 vector1, Vector2 vector2)
        {
            return new Vector2(vector1.X + vector2.X, vector1.Y + vector2.Y);
        }

        public static Vector2 operator - (Vector2 vector1, Vector2 vector2)
        {
            return new Vector2(vector1.X - vector2.X, vector1.Y - vector2.Y);
        }

        public static Vector2 operator * (Vector2 vector1, float float1)
        {
            return new Vector2(vector1.X * float1, vector1.Y * float1);
        }

        public static Vector2 operator /(Vector2 vector1, float float1)
        {
            return new Vector2(vector1.X / float1, vector1.Y / float1);
        }

        public static bool operator == (Vector2 vector1, Vector2 vector2)
        {
            return vector1.X == vector2.X && vector1.Y == vector2.Y;
        }

        public static bool operator != (Vector2 vector1, Vector2 vector2)
        {
            return vector1.X != vector2.X || vector1.Y != vector2.Y;
        }

        public override string ToString()
        {
            return $"{X}, {Y}";
        }
    }
}
