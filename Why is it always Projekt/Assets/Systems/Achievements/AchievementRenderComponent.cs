using System.Collections.Generic;
using SystemBase.Core;
using UnityEngine;

namespace Systems.Achievements
{
    public class AchievementRenderComponent : GameComponent
    {
        [SerializeField] public List<Achievement> achievements = new List<Achievement>();
    }
}