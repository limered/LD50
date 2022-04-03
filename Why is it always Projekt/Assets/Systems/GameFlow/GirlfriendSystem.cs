using System;
using SystemBase.Core;
using SystemBase.Utils;
using UniRx;

namespace Systems.GameFlow
{
    [GameSystem]
    public class GirlfriendSystem : GameSystem<GirlfriendComponent>
    {
        public override void Register(GirlfriendComponent component)
        {
            Observable.Timer(TimeSpan.FromSeconds(5))
                .Subscribe(_ => component.bubble.gameObject.SetActive(false))
                .AddTo(component);
            
            MessageBroker.Default.Receive<SaySomethingMessage>()
                .Subscribe(_ =>
                {
                    Observable.Timer(TimeSpan.FromSeconds(5))
                        .Subscribe(_ => component.bubble.gameObject.SetActive(false))
                        .AddTo(component);
                    var text = component.messages.Randomize()[0];
                    component.messageArea.text = text;
                    component.bubble.gameObject.SetActive(true);
                })
                .AddTo(component);
        }
    }
}