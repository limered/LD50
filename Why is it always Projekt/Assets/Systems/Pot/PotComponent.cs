using SystemBase.Core;
using UniRx;
using UnityEngine;

namespace Systems.DragPlant
{
    public class PotComponent : GameComponent
    {
        public readonly ReactiveCommand StartDrag = new ReactiveCommand();
        public readonly ReactiveCommand StopDrag = new ReactiveCommand();
        public bool isDragged;
        public bool isAnimating;

        public GameObject plantLocation;
    }
}
