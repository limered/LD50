using System;
using System.Linq;
using SystemBase.Core;
using SystemBase.Utils;
using Systems.Achievements;
using TMPro;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Systems.GameFlow
{
    [GameSystem]
    public class GirlfriendSystem : GameSystem<GirlfriendComponent, AchievementRenderComponent>
    {
        private AchievementRenderComponent _achievements;
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

            MessageBroker.Default.Receive<PlantSpawnMessage>()
                .Subscribe(_ =>
                {
                    component.spawnedPlants++;
                    if (component.spawnedPlants == 5)
                    {
                        MessageBroker.Default.Publish(new AchievementMessage{Achievement = new Achievement{name = "High Five!"}});
                    }
                    if (component.spawnedPlants == 10)
                    {
                        MessageBroker.Default.Publish(new AchievementMessage{Achievement = new Achievement{name = "Crazy plant lady."}});
                    }
                })
                .AddTo(component);
            
            MessageBroker.Default.Receive<PlantDiedMessage>()
                .Subscribe(_ =>
                {
                    if (component.endScreen.activeSelf) return;
                    MessageBroker.Default.Publish(new AchievementMessage{Achievement = new Achievement{name = "R.I.P."}});
                    component.endScreen.SetActive(true);
                    var achieved = _achievements.achievements;
                    foreach (var achievement in achieved)
                    {
                        var ach = Object.Instantiate(
                            _achievements.achievementSummaryPrefab, 
                            Vector3.zero, 
                            Quaternion.identity,
                            component.endScreen.transform);
                        var text = ach.GetComponentInChildren<TextMeshProUGUI>();
                        if (achievement.achieved)
                        {
                            text.text = achievement.name + " - " + achievement.text;
                        }
                        else
                        {
                            text.text = "Locked achievement! Play again to unlock.";
                        }
                    }
                })
                .AddTo(component);
        }

        public override void Register(AchievementRenderComponent component)
        {
            _achievements = component;
        }
    }
}