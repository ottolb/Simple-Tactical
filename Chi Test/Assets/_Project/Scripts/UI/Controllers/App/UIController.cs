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
        float levelProgress;

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void ListenEvents()
        {

            EventManager.StartListening(N.App.Loaded, OnAppLoaded);
            EventManager.StartListening(N.Game.Over, OnGameOver);
            EventManager.StartListening(N.Level.NextLevel, OnLevelChanged);

            EventManager.StartListening(N.Level.Passed, OnLevelPassed);

            EventUIManager.StartListening(NUI.Home.Continue, OnContinueClicked);
            EventUIManager.StartListening(NUI.Home.Play, OnPlayClicked);
            EventUIManager.StartListening(NUI.EndGame.Restart, OnRestartClicked);

            EventUIManager.StartListening(NUI.Home.ShowTutorial, OnShowTutorial);

            EventManager.StartListening(N.Level.Progress, OnLevelProgress);

            EventUIManager.StartListening(NUI.HUD.Blocks, OnBlocksChanged);
            EventUIManager.StartListening(NUI.HUD.MoreBlocks, OnMoreBlocksChanged);
        }

        void OnContinueClicked(object p_desc)
        {
            endGameUI.Hide(true);
            hudUI.Show(true);
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
            int p = (int)(levelProgress * 100);
            this.WaitForSecondsAndDo(0.5f, () =>
            {
                hudUI.Hide(false);
                endGameUI.Show(true);
                endGameUI.Setup(ScoreController.Instance.Score,
                                ScoreController.Instance.HasNewHighScore,
                                p);
            });



            //int maxLevels = 50;//GameController.instance.levelUpConfig.levels.Length;
            //float level = (int)GameController.GetLevelId();
            //Debug.Log("GC Level " + level +  " / SM Level" + ScoreManager.Instance.Level);
            //float progress = level / (float)maxLevels;
            //Debug.Log("progress " + progress);
            //endGameUI.SetLevelProgress(progress, (int)level + 1);

            //progress = ScoreManager.Instance.HighLevel / (float)maxLevels;
            //Debug.Log("progress " + progress);
            //endGameUI.SetHighLevelProgress(progress, ScoreManager.Instance.HighLevel + 1);
            //GameController.instance.enabled = false;
        }

        void OnLevelChanged(object p_data)
        {
            //LevelConfiguration level = (LevelConfiguration)p_data;
            //endGameUI.SetMessage(level.textMessage);
            hudUI.FadeOut();
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
            this.WaitForSecondsAndDo(0.2f, delegate
            {
                homeUI.Show(true);
                hudUI.Hide(true);
                endGameUI.Hide(true);
            });

            this.WaitForSecondsAndDo(0.5f, delegate
            {
                UICommonController.Instance.fadeUI.FadeOut();
            });


        }

        void OnShowTutorial(object p_null)
        {
            tutoUI.Show(false);
        }

        void OnLevelPassed(object p_data)
        {
            hudUI.LevelPassed();
        }

        void OnLevelProgress(object p_progress)
        {
            levelProgress = ((float)p_progress);
        }

        void OnBlocksChanged(object p_blocks)
        {
            hudUI.OnBlocksChanged((int)p_blocks);
        }

        void OnMoreBlocksChanged(object p_blocks)
        {
            hudUI.OnMoreBlocksChanged((int)p_blocks);
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