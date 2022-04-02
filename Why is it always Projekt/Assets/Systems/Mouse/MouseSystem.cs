using SystemBase.Core;
using Systems.DragPlant;
using UniRx;
using UnityEngine;

namespace Systems.Mouse
{
    [GameSystem(typeof(PotSystem))]
    public class MouseSystem : GameSystem<MouseComponent>
    {
        public override void Register(MouseComponent component)
        {
            SystemUpdate(component).Subscribe(UpdateMouse).AddTo(component);
        }

        private void UpdateMouse(MouseComponent mouse)
        {
            if(UnityEngine.Camera.main == null) return;
            var ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out var hit)) return;

            if (mouse.draggedPlant)
            {
                MovePlantWithMouse(mouse, hit);
            }
            
            if (Input.GetAxis("Fire1") > 0)
            {
                SwitchDrag(mouse, hit);
            }
        }

        private static void MovePlantWithMouse(MouseComponent mouse, RaycastHit hit)
        {
            var pos = mouse.draggedPlant.transform.position;
            var targetPos = new Vector3
            {
                x = Mathf.Clamp(hit.point.x, -8, 8),
                z = Mathf.Clamp(hit.point.z, -16, 15),
                y = pos.y
            };

            mouse.draggedPlant.transform.position = Vector3.Lerp(pos, targetPos, 0.7f);
        }

        private static void SwitchDrag(MouseComponent mouse, RaycastHit hit)
        {
            var plant = hit.transform.gameObject.GetComponent<PotComponent>();
            if (!plant || plant.isAnimating) return;

            if (plant.isDragged)
            {
                mouse.dragPlane.SetActive(false);
                mouse.draggedPlant = null;
                plant.StopDrag.Execute();
            }
            else
            {
                mouse.dragPlane.SetActive(true);
                mouse.draggedPlant = plant;
                plant.StartDrag.Execute();
            }
        }
    }
}