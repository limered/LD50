using UnityEngine;

namespace Systems.Plant
{
    [CreateAssetMenu(menuName = "aaa/plant")]
    public class PlantDefinition : ScriptableObject
    {
        public GameObject plant;
        public float neededLightValue;
        public float maxLightValue;
        public float maxBurnPoints;
    }
}