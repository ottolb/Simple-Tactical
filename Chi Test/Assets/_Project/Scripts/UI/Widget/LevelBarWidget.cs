using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Game.Event;

namespace Game.UI
{
    public class LevelBarWidget : MonoBehaviour
    {
        private TextMeshProUGUI currentLevelT, nextLevelT;

        private Image levelBar;
        private Tween barTween;
        public float speedBarFx;


        void Awake()
        {
            Transform t = transform;
            currentLevelT = t.Find("Current Level T").GetComponent<TextMeshProUGUI>();
            nextLevelT = t.Find("Next Level T").GetComponent<TextMeshProUGUI>();
            levelBar = t.Find("Fill").GetComponent<Image>();

            EventManager.StartListening(N.Game.Start, OnGameStarted);
            EventManager.StartListening(N.Level.Load, OnLevelLoaded);
            EventManager.StartListening(N.Level.Progress, OnLevelProgress);

            UpdateLevelText();
        }

        void OnLevelLoaded(object p_data)
        {
            UpdateLevelText();
            UpdateLevelBar(0);
        }

        void OnGameStarted(object p_data)
        {
            UpdateLevelBar(0);
        }

        void OnLevelProgress(object p_progress)
        {
            UpdateLevelBar((float)p_progress);
        }

        void UpdateLevelBar(float p_progress)
        {
            //levelBar.fillAmount = (float)p_progress;
            if (barTween != null)
                barTween.Complete();

            barTween = DOTween.To(() => levelBar.fillAmount, x => levelBar.fillAmount = x, p_progress, speedBarFx);
        }

        void UpdateLevelText()
        {
            int level = Level.LevelController.CurrentLevel + 1;
            Debug.Log("UpdateLevelText " + level);
            currentLevelT.text = string.Format("Level " + level.ToString());
            nextLevelT.text = (level + 1).ToString();
        }
    }
}