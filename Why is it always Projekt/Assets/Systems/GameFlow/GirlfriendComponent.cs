using System.Collections.Generic;
using SystemBase.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.GameFlow
{
    public class GirlfriendComponent : GameComponent
    {
        public GameObject bubble;
        public TextMeshProUGUI messageArea;
        
        public List<string> messages;
        public string endText;
    }
}