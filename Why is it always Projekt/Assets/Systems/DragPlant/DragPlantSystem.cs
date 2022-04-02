using System.Collections;
using SystemBase.Core;
using UniRx;
using UnityEngine;

namespace Systems.DragPlant
{
    [GameSystem]
    public class DragPlantSystem : GameSystem<DraggablePlantComponent>
    {
        private const float DragPositionY = 2f;
        private const float BasePositionY = 0.5f;
        
        public override void Register(DraggablePlantComponent plant)
        {
            plant.StartDrag
                .Subscribe(_ => StartDragging(plant))
                .AddTo(plant);
            
            plant.StopDrag
                .Subscribe(_ => StopDragging(plant))
                .AddTo(plant);
        }

        private void StopDragging(DraggablePlantComponent plant)
        {
            plant.isDragged = false;
            plant.StartCoroutine(AnimateDown(plant));
        }

        private void StartDragging(DraggablePlantComponent plant)
        {
            plant.isDragged = true;
            plant.StartCoroutine(AnimateUp(plant));
        }

        private static IEnumerator AnimateUp(DraggablePlantComponent plant)
        {
            plant.isAnimating = true;
            for (var i = 0; i < 10; i++)
            {
                var pos = plant.transform.position;
                var topPos = new Vector3(pos.x, DragPositionY, pos.z);
                plant.transform.position = Vector3.Lerp(pos, topPos, i / 10f);
                yield return null;
            }
            plant.isAnimating = false;
        }
        
        private static IEnumerator AnimateDown(DraggablePlantComponent plant)
        {
            plant.isAnimating = true;
            for (var i = 0; i < 10; i++)
            {
                var pos = plant.transform.position;
                var topPos = new Vector3(pos.x, BasePositionY, pos.z);
                plant.transform.position = Vector3.Lerp(pos, topPos, i / 10f);
                yield return null;
            }
            plant.isAnimating = false;
        }
    }
}