//
// UIController.cs
//
// Author:
//       Otto Lopes <otto@buildandrungames.com>
//
// Copyright (c) 2018 Build and Run Games 
//


using System.Collections;
using System.Collections.Generic;
using Game.Event;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{

    public class UIController : BaseUIController
    {

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void ListenEvents()
        {

            EventManager.StartListening(N.App.Loaded, OnAppLoaded);
            EventManager.StartListening(N.Game.Over, OnGameOver);

            EventUIManager.StartListening(NUI.Home.Play, OnPlayClicked);
            EventUIManager.StartListening(NUI.EndGame.Restart, OnRestartClicked);


            EventUIManager.StartListening(NUI.HUD.PlayerTurn, OnPlayerTurn);
            EventUIManager.StartListening(NUI.HUD.NPCTurn, OnNPCTurn);
            EventUIManager.StartListening(NUI.HUD.SetActionButton, OnUpdateActionButtons);
            EventUIManager.StartListening(NUI.HUD.SetAvailableActions, OnSetAvailableActions);

        }

        void OnAppLoaded(object p_desc)
        {
            homeUI.Show(false);
            //Init other UIs since app load
            endGameUI.Hide(false);
            hudUI.Hide(false);
        }

        void OnGameOver(object p_desc)
        {
            bool isPlayerDefeat = (bool)p_desc;
            this.WaitForSecondsAndDo(0.5f, () =>
            {
                hudUI.Hide(false);
                endGameUI.Show(true);
                endGameUI.Setup(isPlayerDefeat);
            });

        }

        void OnPlayClicked(object p_desc)
        {
            homeUI.Hide(true);
            hudUI.Show(true);
            hudUI.Setup();
        }

        void OnRestartClicked(object p_desc)
        {
            UICommonController.Instance.fadeUI.FadeIn();
        }

        void OnPlayerTurn(object p_unit)
        {
            hudUI.TurnChanged(true);
        }

        void OnNPCTurn(object p_unit)
        {
            hudUI.TurnChanged(false);
        }

        void OnUpdateActionButtons(object p_hasActions)
        {
            bool hasAction = (bool)p_hasActions;
            Debug.LogFormat("#HUD# Set action button: {0}", hasAction);
            hudUI.UpdateActionButtons(hasAction);
        }

        void OnSetAvailableActions(object p_hasActions)
        {
            Dictionary<string, int> dict = (Dictionary<string, int>)p_hasActions;
            hudUI.SetRemainingActions(dict["current"], dict["total"]);
        }


        #region Properties

        private HUD _hudUI;

        public HUD hudUI
        {
            get
            {
                if (_hudUI == null)
                    _hudUI = CreateModal("HUD", "HUD", Canvas).GetComponent<HUD>();
                return _hudUI;
            }
        }

        private HomeUI _homeUI;

        public HomeUI homeUI
        {
            get
            {
                if (_homeUI == null)
                    _homeUI = CreateModal("Main Menu", "HomeUI", Canvas).GetComponent<HomeUI>();
                return _homeUI;
            }
        }

        private EndGameUI _endGameUI;

        public EndGameUI endGameUI
        {
            get
            {
                if (_endGameUI == null)
                    _endGameUI = CreateModal("GameOver", "EndGameUI", Canvas).GetComponent<EndGameUI>();
                return _endGameUI;
            }
        }

        private TutorialUI _tutoUI;

        public TutorialUI tutoUI
        {
            get
            {

                if (_tutoUI == null)
                    _tutoUI = CreateModal("Help UI", "TutorialUI", Canvas).GetComponent<TutorialUI>();
                return _tutoUI;
            }
        }

        #endregion
    }
}