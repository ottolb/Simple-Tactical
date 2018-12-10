using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Game.Event;

namespace Game.UI
{
    public class HUD : BaseUI
    {
        private TextMeshProUGUI playerTurnT, enemyTurnT;

        private GameObject turnButtonsCtr;
        private Button waitBt, endTurnBt;

        private TextMeshProUGUI actionT;

        private Image fadeImg;
        private DOTweenAnimation fadeAnim;


        protected override void Awake()
        {
            base.Awake();
            fadeImg = transform.Find("Bkg").GetComponent<Image>();
            fadeAnim = fadeImg.GetComponent<DOTweenAnimation>();


            playerTurnT = transform.Find("Turn Info/Player Turn T").GetComponent<TextMeshProUGUI>();
            enemyTurnT = transform.Find("Turn Info/Enemy Turn T").GetComponent<TextMeshProUGUI>();

            turnButtonsCtr = transform.Find("Turn Buttons").gameObject;
            waitBt = transform.Find("Turn Buttons/Wait Button").GetComponent<Button>();
            endTurnBt = transform.Find("Turn Buttons/End Turn Button").GetComponent<Button>();

            actionT = transform.Find("Actions Info/Amount T").GetComponent<TextMeshProUGUI>();
        }

        void Start()
        {

        }

        public override void Show(bool p_animated)
        {
            base.Show(p_animated);
            fadeAnim.DORestartById("FadeOut");
        }

        public void Setup()
        {

        }

        public void TurnChanged(bool p_playerTurn)
        {
            playerTurnT.gameObject.SetActive(p_playerTurn);
            enemyTurnT.gameObject.SetActive(!p_playerTurn);
            turnButtonsCtr.SetActive(p_playerTurn);
        }

        public void UpdateActionButtons(bool p_hasAction)
        {
            waitBt.gameObject.SetActive(p_hasAction);
            endTurnBt.gameObject.SetActive(!p_hasAction);
        }

        public void SetRemainingActions(int p_current, int p_total)
        {
            actionT.text = string.Format("{0}/{1}", p_current, p_total);
        }

        public void FadeOut()
        {
            fadeAnim.DORestartById("FadeOut");
        }

        #region Buttons
        void OnWaitClicked(ButtonView p_button)
        {
            EventUIManager.TriggerEvent(NUI.HUD.WaitAction);
        }

        void OnEndTurnClicked(ButtonView p_button)
        {
            EventUIManager.TriggerEvent(NUI.HUD.EndTurn);
        }

        #endregion
    }
}
