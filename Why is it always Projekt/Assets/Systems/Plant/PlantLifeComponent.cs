using SystemBase.Core;
using UnityEngine;

namespace Systems.Plant
{
    public class PlantLifeComponent : GameComponent
    {
        public float lifePoints;
        public PlantDefinition plantDefinition;

        public GameObject bubble;
        public GameObject bubbleContent;
        public Material[] bubbleContents;
    }
}