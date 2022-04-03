using SystemBase.Core;
using UnityEngine;

namespace Systems.Plant
{
    public class PlantLifeComponent : GameComponent
    {
        public float lifePoints;
        public PlantDefinition plantDefinition;

        public GameObject bubble;
        public MeshRenderer bubbleContent;
        public Material[] bubbleContents;

        public GameObject deadPlant;
        public bool isDead;
    }
}