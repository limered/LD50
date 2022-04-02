using System;
using SystemBase.Core;
using UniRx;
using UnityEngine;

namespace Systems.Plant
{
    [GameSystem]
    public class PlantSystem : GameSystem<NeedsLightComponent, PlantLifeComponent>
    {
        private const float LightIncrease = 1f;
        private const float LightDecrease = 0.2f;
        private const float BurnPointsIncrease = 1f;

        public override void Register(NeedsLightComponent component)
        {
            component.currentLightValue = (component.maxLightValue + component.neededLightValue) * 0.5f;
            SystemUpdate(component).Subscribe(UpdateLightValues).AddTo(component);
        }
        
        public override void Register(PlantLifeComponent component)
        {
            SystemUpdate(component).Subscribe(plant =>
            {
                if (plant.lifePoints <= 0)
                {
                    // is dead
                }
            }).AddTo(component);
        }

        private void UpdateLightValues(NeedsLightComponent plant)
        {
            var lifeComponent = plant.GetComponent<PlantLifeComponent>();
            if (lifeComponent.lifePoints <= 0) return;
            
            if (IsLit(plant))
            {
                plant.currentLightValue += LightIncrease * Time.deltaTime;
                
                if (plant.currentLightValue > plant.maxLightValue)
                {
                    plant.burnPoints += BurnPointsIncrease * Time.deltaTime;
                    
                    if (plant.burnPoints > plant.maxBurnPoints)
                    {
                        lifeComponent.lifePoints -= 10 * Time.deltaTime;
                    }
                }
            }
            else
            {
                plant.currentLightValue -= LightDecrease * Time.deltaTime;
                
                if (plant.currentLightValue < plant.neededLightValue)
                {
                    lifeComponent.lifePoints -= 1 * Time.deltaTime;
                }
            }

            plant.currentLightValue = Mathf.Min(0, Mathf.Max(plant.maxLightValue, plant.currentLightValue));
        }

        private static bool IsLit(NeedsLightComponent plant)
        {
            var rayDirection = -plant.sun.transform.forward;
            return !Physics.Raycast(plant.transform.position, rayDirection, 40f);
        }
    }
}