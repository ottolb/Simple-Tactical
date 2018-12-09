
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Advertisements;
using Game.Event;

namespace Game.UI
{
    public class EndGameUI : BaseUI
    {
        private GameObject recordCtr;
        private TextMeshProUGUI score, levelProgressT;

        float levelProgress;

        protected override void Awake()
        {
            base.Awake();
            recordCtr = transform.Find("Record").gameObject;
            levelProgressT = transform.Find("Level Progress T").GetComponent<TextMeshProUGUI>();
            score = transform.Find("Score Text").GetComponent<TextMeshProUGUI>();

        }

        private void Start()
        {
            //EventManager.StartListening(N.Level.MaxLevel, OnMaxLevelSet);
        }

        public void Setup(int p_score, bool p_new, int p_progress)
        {
            score.text = p_score.ToString();
            recordCtr.SetActive(p_new);


            levelProgressT.text = string.Format("{0}% COMPLETED", p_progress);
        }

        void OnLevelProgress(object p_progress)
        {
            levelProgress = ((float)p_progress);
        }

        void OnPlayClicked(ButtonView p_button)
        {
            EventUIManager.TriggerEvent(NUI.EndGame.Restart);
        }

        void OnHelpClicked(ButtonView p_button)
        {
            EventUIManager.TriggerEvent(NUI.Home.ShowTutorial);
        }

        void OnLeaderboardsClicked(ButtonView p_button)
        {
            EventUIManager.TriggerEvent(NUI.Home.ShowLeaderboards);
        }
    }
}