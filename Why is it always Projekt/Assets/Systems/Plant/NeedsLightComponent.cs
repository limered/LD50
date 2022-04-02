using SystemBase.Core;
using UnityEngine;

namespace Systems.Plant
{
    public class NeedsLightComponent : GameComponent
    {
        public float currentLightValue;
        public float neededLightValue;
        public float maxLightValue;
        public float burnPoints;
        public float maxBurnPoints;

        public GameObject sun;
    }
}