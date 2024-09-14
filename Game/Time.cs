using System;

namespace Game
{
    public static class Time
    {
        private static DateTime startTime;
        private static float lastFrameTime;
        public static float DeltaTime { get; private set; }

        public static void Initialize()
        {
            startTime = DateTime.Now;
            Engine.Debug($"Fecha de inicio: {startTime.Minute}/{startTime.Second}.");
        }

        public static void CalculateDeltaTime()
        {
            TimeSpan currentTime = DateTime.Now - startTime;
            float currentSeconds = (float)currentTime.TotalSeconds;
            DeltaTime = currentSeconds - lastFrameTime;
            lastFrameTime = currentSeconds;
        }
    }
}
