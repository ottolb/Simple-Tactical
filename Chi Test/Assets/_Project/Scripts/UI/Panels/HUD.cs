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

        public void OnTurnChanged(bool p_playerTurn)
        {
            playerTurnT.gameObject.SetActive(p_playerTurn);
            enemyTurnT.gameObject.SetActive(!p_playerTurn);
            turnButtonsCtr.SetActive(p_playerTurn);
        }

        public void FadeOut()
        {
            fadeAnim.DORestartById("FadeOut");
        }
    }
}
