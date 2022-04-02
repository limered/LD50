using SystemBase.Core;
using Systems.DragPlant;
using UnityEngine;

namespace Systems.Mouse
{
    public class MouseComponent : GameComponent
    {
        public GameObject dragPlane;
        public PotComponent draggedPlant;
    }
}