using System;

namespace Game
{
    public static class CollisionsUtilities
    {
        public static bool IsBoxColliding(Vector2 positionA, Vector2 sizeA, Vector2 positionB, Vector2 sizeB)
        {
            // Calculamos la distancia absoluta en X e Y entre las dos cajas
            float distanceX = Math.Abs(positionA.X - positionB.X);
            float distanceY = Math.Abs(positionA.Y - positionB.Y);

            // Sumamos la mitad de los anchos y altos de ambas cajas
            float sumHalfWidths = sizeA.X / 2 + sizeB.X / 2;
            float sumHalfHeights = sizeA.Y / 2 + sizeB.Y / 2;

            return distanceX <= sumHalfWidths && distanceY <= sumHalfHeights;
        }

        public static bool IsCircleColliding(Vector2 positionA, float radiusA, Vector2 positionB, float radiusB)
        {
            float distanceX = positionA.X - positionB.X;
            float distanceY = positionA.Y - positionB.Y;

            float totalDistance = (float) Math.Sqrt(distanceX * distanceX + distanceY * distanceY);

            return totalDistance < radiusA + radiusB;
        }

        public static bool IsBoxWithCircleColliding(Vector2 boxPosition, Vector2 boxSize, Vector2 circlePosition, float circleRadius)
        {
            float px = circlePosition.X;
            if (circlePosition.X < boxPosition.X) px = boxPosition.X;
            else if (circlePosition.X > boxPosition.X + boxSize.X) px = boxPosition.X + boxSize.X;

            float py = circlePosition.Y;
            if (circlePosition.Y < boxPosition.Y) py = boxPosition.Y;
            else if (circlePosition.Y > boxPosition.Y + boxSize.Y) py = boxPosition.Y + boxSize.Y;

            float distanceX = circlePosition.X - px;
            float distanceY = circlePosition.Y - py;

            float distance = (float) Math.Sqrt(distanceX * distanceX + distanceY * distanceY);

            return distance <= circleRadius;
        }
    }
}
