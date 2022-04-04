using System;
using SystemBase.CommonSystems.Audio;
using SystemBase.Core;
using SystemBase.Utils;
using Systems.DragPlant;
using Systems.GameFlow;
using Systems.Plant.Messages;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Systems.Plant
{
    [GameSystem]
    public class PlantCreationSystem : GameSystem<PlantSpawnerComponent>
    {
        public override void Register(PlantSpawnerComponent component)
        {
            MessageBroker.Default.Receive<SpawnPlantMessage>()
                .Subscribe(_ => SpawnPlants(component))
                .AddTo(component);

            MessageBroker.Default.Publish(new SpawnPlantMessage());
        }

        private void SpawnPlants(PlantSpawnerComponent spawner)
        {
            "newplant".Play();
            var spawnPositions = spawner.spawnPositions.Randomize();
            var potPrefabs = spawner.potsPrefabs.Randomize();
            var plantDefinitions = spawner.plantDefinitions.Randomize();
            for(var i = 0; i < spawner.spawnCount; i++)
            {
                var potNr = i % potPrefabs.Count;
                var spawnNr = i % spawnPositions.Count;
                var plantNr = i % plantDefinitions.Count;
                SpawnPlant(spawner, spawnPositions[spawnNr], potPrefabs[potNr], plantDefinitions[plantNr]);
            }
        }

        private void SpawnPlant(PlantSpawnerComponent spawner, Vector3 pos, GameObject potPrefab, PlantDefinition plantDefinition)
        {
            var pot = Object.Instantiate(potPrefab, pos, Quaternion.identity);
            var plantPosition = pot.GetComponent<PotComponent>().plantLocation;
            var plant = Object.Instantiate(
                spawner.plantPrefab, 
                Vector3.zero, 
                Quaternion.identity,
                plantPosition.transform);
            plant.transform.localPosition = Vector3.zero;
            plant.GetComponent<MeshFilter>().mesh = plantDefinition.plant.GetComponent<MeshFilter>().sharedMesh;
            var lifeComponent = plant.GetComponent<PlantLifeComponent>();
            lifeComponent.plantDefinition = plantDefinition;
            
            var needsLightComponent = plant.GetComponent<NeedsLightComponent>();
            needsLightComponent.sun = spawner.sun;
            needsLightComponent.neededLightValue = plantDefinition.neededLightValue;
            needsLightComponent.maxLightValue = plantDefinition.maxLightValue;
            needsLightComponent.maxBurnPoints = plantDefinition.maxBurnPoints;
            
            lifeComponent.lifePoints = 100;
            needsLightComponent.currentLightValue = needsLightComponent.neededLightValue;
            
            MessageBroker.Default.Publish(new PlantSpawnMessage());
        }
    }
}