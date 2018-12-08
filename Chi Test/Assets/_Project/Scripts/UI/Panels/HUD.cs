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
        private TextMeshProUGUI scoreT, levelPassedT, blocksT, moreBlocksT;
        private LevelBarWidget levelBarWidget;


        private Image fadeImg;
        private DOTweenAnimation fadeAnim;


        protected override void Awake()
        {
            base.Awake();
            fadeImg = transform.Find("Bkg").GetComponent<Image>();
            fadeAnim = fadeImg.GetComponent<DOTweenAnimation>();

            Transform t = transform.Find("Top/Level Bar");
            levelBarWidget = t.GetComponent<LevelBarWidget>();

            scoreT = transform.Find("Top/Score T").GetComponent<TextMeshProUGUI>();

            levelPassedT = transform.Find("Level Passed/Text").GetComponent<TextMeshProUGUI>();

            blocksT = transform.Find("Blocks T").GetComponent<TextMeshProUGUI>();
            moreBlocksT = blocksT.transform.Find("MoreBlocks T").GetComponent<TextMeshProUGUI>();
        }

        void Start()
        {
            EventManager.StartListening(N.Game.Points, OnPointsSet);
            EventManager.StartListening(N.Score.NewScore, OnScoreChanged);
        }

        public override void Show(bool p_animated)
        {
            base.Show(p_animated);
            fadeAnim.DORestartById("FadeOut");
        }

        public void Setup()
        {
            scoreT.text = "0";
        }

        void OnPointsSet(object p_data)
        {
            int pts = (int)p_data;
            scoreT.text = pts.ToString();
        }

        void OnScoreChanged(object p_score)
        {
            int s = (int)p_score;
            scoreT.text = s.ToString();
        }

        public void OnBlocksChanged(int p_blocks)
        {
            blocksT.text = p_blocks.ToString();
        }

        public void OnMoreBlocksChanged(int p_blocks)
        {
            moreBlocksT.text = string.Format("+{0}", p_blocks);
        }

        public void LevelPassed()
        {
            fadeAnim.DORestartById("FadeIn");
            anim.Play("Level Passed", 0, 0);
            anim.SetInteger("star", ScoreController.Instance.starsPicked);
            levelPassedT.text = string.Format("Level {0} Passed!", Level.LevelController.CurrentLevel + 1);
        }

        public void FadeOut()
        {
            fadeAnim.DORestartById("FadeOut");
        }
    }
}
