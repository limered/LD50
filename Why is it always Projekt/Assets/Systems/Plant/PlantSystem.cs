using System;
using SystemBase.Core;
using Systems.Achievements;
using UniRx;
using UnityEngine;

namespace Systems.Plant
{
    public enum BubbleImages
    {
        SunMore,
        SunLess,
        Fire,
        WaterMore,
        WaterLess,
    }
    
    [GameSystem]
    public class PlantSystem : GameSystem<NeedsLightComponent, PlantLifeComponent>
    {
        private const float LightIncrease = 1f;
        private const float LightDecrease = 1f;
        private const float BurnPointsIncrease = 1f;

        public override void Register(NeedsLightComponent component)
        {
            
            SystemUpdate(component).Subscribe(UpdateLightValues).AddTo(component);
        }
        
        public override void Register(PlantLifeComponent component)
        {
            component.lifePoints = 100;
            SystemUpdate(component).Subscribe(plant =>
            {
                if (plant.isDead || plant.lifePoints > 0) return;
                plant.isDead = true;
                var lifeComponent = plant.GetComponent<PlantLifeComponent>();
                plant.GetComponent<MeshFilter>().mesh =
                    lifeComponent.deadPlant.GetComponent<MeshFilter>().sharedMesh;
            }).AddTo(component);
        }

        private void UpdateLightValues(NeedsLightComponent plant)
        {
            var lifeComponent = plant.GetComponent<PlantLifeComponent>();
            if (lifeComponent.isDead)
            {
                lifeComponent.bubble.SetActive(false);
                return;
            }
            
            if (IsLit(plant))
            {
                plant.currentLightValue += LightIncrease * Time.deltaTime;
                
                if (plant.currentLightValue > plant.maxLightValue)
                {
                    MessageBroker.Default.Publish(new AchievementMessage{Achievement = new Achievement{name = "Too much sun!"}});
                    plant.burnPoints += BurnPointsIncrease * Time.deltaTime;
                    
                    lifeComponent.bubble.SetActive(true);
                    lifeComponent.bubbleContent.material = lifeComponent.bubbleContents[(int)BubbleImages.SunLess];
                    
                    if (plant.burnPoints > plant.maxBurnPoints)
                    {
                        MessageBroker.Default.Publish(new AchievementMessage{Achievement = new Achievement{name = "Burn baby burn!"}});
                        lifeComponent.bubbleContent.material = lifeComponent.bubbleContents[(int)BubbleImages.Fire];
                        lifeComponent.lifePoints -= 10 * Time.deltaTime;
                    }
                }
                else
                {
                    lifeComponent.bubble.SetActive(false);
                }
            }
            else
            {
                plant.currentLightValue -= LightDecrease * Time.deltaTime;
                plant.burnPoints = 0;
                
                if (plant.currentLightValue < plant.neededLightValue)
                {
                    lifeComponent.bubble.SetActive(true);
                    lifeComponent.bubbleContent.material = lifeComponent.bubbleContents[(int)BubbleImages.SunMore];
                    lifeComponent.lifePoints -= 1 * Time.deltaTime;
                }
                else
                {
                    lifeComponent.bubble.SetActive(false);
                }
            }

            plant.currentLightValue = Mathf.Clamp(plant.currentLightValue, 0, plant.maxLightValue);
        }

        private static bool IsLit(NeedsLightComponent plant)
        {
            var rayDirection = -plant.sun.transform.forward;
            if (Physics.Raycast(plant.transform.position, rayDirection, out var hit, 40f))
            {
                return !hit.transform.CompareTag("room");
            }

            return true;
        }
    }
}