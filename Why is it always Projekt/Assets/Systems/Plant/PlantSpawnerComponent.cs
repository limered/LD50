using System.Collections.Generic;
using SystemBase.Core;
using UnityEngine;

namespace Systems.Plant
{
    public class PlantSpawnerComponent : GameComponent
    {
        public GameObject plantPrefab;
        public int spawnCount;
        public GameObject sun;
        
        public List<GameObject> potsPrefabs;
        public List<Vector3> spawnPositions;
        public List<PlantDefinition> plantDefinitions;
    }
}