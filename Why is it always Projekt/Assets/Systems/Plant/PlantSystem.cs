using SystemBase.Core;
using UniRx;
using UnityEngine;

namespace Systems.Plant
{
    [GameSystem]
    public class PlantSystem : GameSystem<NeedsLightComponent>
    {
        private const float LightIncrease = 1f;
        private const float LightDecrease = 0.2f;
        private const float BurnPointsIncrease = 1f;

        public override void Register(NeedsLightComponent component)
        {
            SystemUpdate(component).Subscribe(UpdateLightValues).AddTo(component);
        }

        private void UpdateLightValues(NeedsLightComponent plant)
        {
            if (IsLit(plant))
            {
                plant.currentLightValue += LightIncrease * Time.deltaTime;
            }

            plant.currentLightValue -= plant.currentLightValue <= 0 
                ? 0 
                : LightDecrease * Time.deltaTime;

            if (plant.currentLightValue < plant.neededLightValue)
            {
                // slowly die
                // subtract lifepoints 
            }
            else if (plant.currentLightValue > plant.maxLightValue)
            {
                plant.burnPoints += BurnPointsIncrease * Time.deltaTime;
            }

            if (plant.burnPoints > plant.maxBurnPoints)
            {
                // start burning or die
            }
        }

        private bool IsLit(NeedsLightComponent plant)
        {
            var rayDirection = -plant.sun.transform.forward;
            return !Physics.Raycast(plant.transform.position, rayDirection, 40f);
        }
    }
}