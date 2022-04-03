using SystemBase.Core;
using UniRx;
using UnityEngine.SceneManagement;

namespace Systems.GameFlow
{
    public class RestartComponent : GameComponent
    {
        public void Restart()
        {
            MessageBroker.Default.Publish(new RestartMessage());            
            
            SceneManager.LoadScene("main");
        }
    }
}