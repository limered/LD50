using SystemBase.Core;
using UnityEngine;

namespace Systems.Sun
{
    public class SunComponent : GameComponent
    {
        [Range(0, 16)]
        public float hour;

        public float hoursPerSecond;
        public Vector3 noon;
    }
}