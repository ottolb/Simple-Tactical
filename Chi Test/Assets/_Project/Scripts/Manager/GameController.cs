using System.Collections;
using System.Collections.Generic;
using Game.Event;
using UnityEngine;

namespace Game
{
    public class GameController : BaseController
    {
        public static GameController Instance;


        // Use this for initialization
        void Start()
        {
            Instance = this;
        }

        protected override void ListenEvents()
        {
            base.ListenEvents();
            EventManager.StartListening(N.App.Loaded, OnAppLoaded);
            EventManager.StartListening(N.Level.NextLevel, OnLevelChanged);


            EventUIManager.StartListening(NUI.Home.Play, OnPlayClicked);
            EventUIManager.StartListening(NUI.EndGame.Restart, OnRestartClicked);
        }

        void OnPlayClicked(object p_data)
        {
            LoadGame();
        }

        void OnRestartClicked(object p_data)
        {
            EventManager.TriggerEvent(N.Level.Clean);
        }

        void LoadGame()
        {
            EventManager.TriggerEvent(N.Game.Load);

            //todo: should wait for other components initialization events...
            EventManager.TriggerEvent(N.Game.Setup);

            //Add a short delay to start the game
            this.WaitForSecondsAndDo(0.5f, StartGame);
        }

        void StartGame()
        {
            EventManager.TriggerEvent(N.Game.Start);
        }

        void OnAppLoaded(object p_desc)
        {
            /*this.WaitForSecondsAndDo(0.5f, delegate
            {
                EventManager.TriggerEvent(N.Level.Load);
            });*/
        }

        void OnLevelChanged(object p_data)
        {
            this.WaitForSecondsAndDo(1.4f, delegate
            {
                EventManager.TriggerEvent(N.Game.Start);
            });
        }

    }
}