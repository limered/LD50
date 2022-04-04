using System;
using System.Collections;
using SystemBase.CommonSystems.Audio;
using SystemBase.Core;
using UniRx;
using UnityEngine;

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
                if(achievement is { achieved: false })
                {
                    "achievement".Play();
                    achievement.achieved = true;
                    component.achievementText.text = achievement.name;
                    component.StartCoroutine(AnimateAchievement(component.achievementPanel));
                }
            }).AddTo(component);
        }

        private IEnumerator AnimateAchievement(GameObject panel)
        {
            for (var i = 0; i < 10; i++)
            {
                var pos = panel.transform.position;
                var newY = Mathf.Lerp(pos.y, 75f, i / 10f);
                panel.transform.position = new Vector3(pos.x, newY, pos.z);
                yield return null;
            }

            yield return new WaitForSeconds(3);
            
            for (var i = 0; i < 10; i++)
            {
                var pos = panel.transform.position;
                var newY = Mathf.Lerp(pos.y, -50f, i / 10f);
                panel.transform.position = new Vector3(pos.x, newY, pos.z);
                yield return null;
            }
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