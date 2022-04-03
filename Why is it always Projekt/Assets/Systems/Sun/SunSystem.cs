using SystemBase.Core;
using SystemBase.GameState.States;
using SystemBase.Utils;
using Systems.GameFlow;
using Systems.Plant.Messages;
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

        private static void ProgressSun(SunComponent sun)
        {
            if (IoC.Game.gameStateContext.CurrentState.Value is not Running) return;
            sun.hour += sun.hoursPerSecond * Time.deltaTime;
            if (sun.hour > 16)
            {
                MessageBroker.Default.Publish(new SpawnPlantMessage());
                MessageBroker.Default.Publish(new SaySomethingMessage());
            }
            sun.hour = sun.hour > 16 ? 0 : sun.hour;

            var rotation = sun.hour / 8f;
            sun.transform.eulerAngles = sun.noon * rotation;
        }
    }
}