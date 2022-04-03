using System;
using SystemBase.Core;
using UniRx;

namespace Systems.Achievements
{
    [GameSystem]
    public class AchievementSystem : GameSystem<AchievementRenderComponent>
    {
        public override void Register(AchievementRenderComponent component)
        {
            MessageBroker.Default.Receive<AchievementMessage>().Subscribe(msg =>
            {
                var achievement =
                    component.achievements.Find(achievement1 => achievement1.name == msg.Achievement.name);
                if(achievement != null)
                {
                    achievement.achieved = true;
                }
            }).AddTo(component);
        }
    }

    public class AchievementMessage
    {
        public Achievement Achievement { get; set; }
    }
    
    [Serializable]
    public class Achievement
    {
        public string name;
        public string text;
        public bool achieved;
    }
}