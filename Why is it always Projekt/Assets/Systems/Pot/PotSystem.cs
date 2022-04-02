using System.Collections;
using SystemBase.Core;
using UniRx;
using UnityEngine;

namespace Systems.DragPlant
{
    [GameSystem]
    public class PotSystem : GameSystem<PotComponent>
    {
        private const float DragPositionY = 1f;
        private const float BasePositionY = 0f;
        
        public override void Register(PotComponent plant)
        {
            plant.StartDrag
                .Subscribe(_ => StartDragging(plant))
                .AddTo(plant);
            
            plant.StopDrag
                .Subscribe(_ => StopDragging(plant))
                .AddTo(plant);
        }

        private void StopDragging(PotComponent plant)
        {
            plant.isDragged = false;
            plant.StartCoroutine(AnimateDown(plant));
        }

        private void StartDragging(PotComponent plant)
        {
            plant.isDragged = true;
            plant.StartCoroutine(AnimateUp(plant));
        }

        private static IEnumerator AnimateUp(PotComponent plant)
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
        
        private static IEnumerator AnimateDown(PotComponent plant)
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