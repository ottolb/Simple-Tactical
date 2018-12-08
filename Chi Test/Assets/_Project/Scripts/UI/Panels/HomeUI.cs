using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Game.Event;

namespace Game.UI
{
    public class HomeUI : BaseUI
    {
        public TextMeshProUGUI score;

        public void Setup(int p_score)
        {
            //EventUIManager.StartListening(NUI.Loading.UploadProgress, OnUploadProgress);
            score.text = p_score.ToString();
        }

        void OnPlayClicked(ButtonView p_button)
        {
            EventUIManager.TriggerEvent(NUI.Home.Play);
        }

        void OnHelpClicked(ButtonView p_button)
        {
            EventUIManager.TriggerEvent(NUI.Home.ShowTutorial);
        }

        void OnLeaderboardsClicked(ButtonView p_button)
        {
            EventUIManager.TriggerEvent(NUI.Home.ShowLeaderboards);
        }

        void OnSettingsClicked(ButtonView p_button)
        {
            //EventUIManager.TriggerEvent(NUI.Home.ShowLeaderboards);
        }

        void OnResetClicked(ButtonView p_button)
        {
            PlayerPrefs.DeleteKey("current_level");
            EventUIManager.TriggerEvent(NUI.EndGame.LevelSlider, 1);
        }

    }
}