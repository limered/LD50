using SystemBase.Core;
using UniRx;

namespace Systems.DragPlant
{
    public class PotComponent : GameComponent
    {
        public readonly ReactiveCommand StartDrag = new ReactiveCommand();
        public readonly ReactiveCommand StopDrag = new ReactiveCommand();
        public bool isDragged;
        public bool isAnimating;
    }
}