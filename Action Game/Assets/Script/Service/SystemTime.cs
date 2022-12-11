using UnityEngine;

namespace Service.SystemBase
{
    public readonly struct SystemTime
    {
        public static float DeltaTime { get; private set; }

        public static void SetTime()
        {
            DeltaTime = Time.deltaTime;
        }
    }
}
