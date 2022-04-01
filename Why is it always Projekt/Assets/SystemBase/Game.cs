using SystemBase.Adapter;
using SystemBase.CommonSystems.Audio.Helper;
using SystemBase.Core;
using SystemBase.GameState.Messages;
using SystemBase.GameState.States;
using SystemBase.Utils;
using UniRx;
using UnityEngine;

namespace SystemBase
{
    public class Game : GameBase
    {
        // ReSharper disable once MemberCanBePrivate.Global
        public readonly StateContext<Game> gameStateContext = new();

        private void Awake()
        {
            gameStateContext.Start(new Loading());

            Init();

            MessageBroker.Default.Publish(new GameMsgFinishedLoading());
            MessageBroker.Default.Publish(new GameMsgStart());
            Cursor.visible = true;
        }

        private void Start()
        {
           // MessageBroker.Default.Publish(new GameMsgStart());
            QualitySettings.vSyncCount = 1;
            Application.targetFrameRate = 60;
        }

        public override void Init()
        {
            base.Init();
            
            IoC.RegisterSingleton(this);
            IoC.RegisterSingleton<ISFXComparer, SFXComparer>();
        }
    }
}