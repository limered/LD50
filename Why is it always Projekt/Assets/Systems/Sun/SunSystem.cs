using SystemBase.Core;
using UniRx;
using UnityEngine;

namespace Systems.Sun
{
    [GameSystem]
    public class SunSystem : GameSystem<SunComponent>
    {
        public override void Register(SunComponent component)
        {
            SystemUpdate(component).Subscribe(ProgressSun).AddTo(component);
        }

        private void ProgressSun(SunComponent sun)
        {
            sun.hour += sun.hoursPerSecond * Time.deltaTime;
            sun.hour = (sun.hour > 16) ? 0 : sun.hour;

            var rotation = sun.hour / 8f;
            sun.transform.eulerAngles = sun.noon * rotation;
        }
    }
}