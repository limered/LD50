using System.Collections.Generic;
using SystemBase.Core;
using TMPro;
using UnityEngine;

namespace Systems.Achievements
{
    public class AchievementRenderComponent : GameComponent
    {
        [SerializeField] public List<Achievement> achievements = new List<Achievement>();

        public GameObject achievementPanel;
        public TextMeshProUGUI achievementText;
    }
}