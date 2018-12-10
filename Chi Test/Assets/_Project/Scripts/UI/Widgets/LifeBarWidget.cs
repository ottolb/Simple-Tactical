using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Game.Event;

namespace Game.UI
{
    public class LifeBarWidget : MonoBehaviour
    {
        private TextMeshProUGUI lifeT;

        private Image lifeBar;
        private Tween barTween;
        public float speedBarFx;


        void Awake()
        {
            Transform t = transform;
            lifeT = t.Find("Life Text").GetComponent<TextMeshProUGUI>();
            lifeBar = t.Find("Life Bar/Filler").GetComponent<Image>();

            //EventManager.StartListening(N.Game.Start, OnGameStarted);
            //EventManager.StartListening(N.Level.Load, OnLevelLoaded);
            //EventManager.StartListening(N.Level.Progress, OnLevelProgress);
        }

        //void OnLevelLoaded(object p_data)
        //{
        //    UpdateLevelText();
        //    UpdateLife(0);
        //}

        public void UpdateLife(int p_current, int p_total)
        {
            //levelBar.fillAmount = (float)p_progress;
            if (barTween != null)
                barTween.Complete();

            float clamped = p_current / (float)p_total;

            barTween = DOTween.To(() => lifeBar.fillAmount, x => lifeBar.fillAmount = x, clamped, speedBarFx);

            lifeT.text = string.Format("{0}/{1}", p_current, p_total);
        }

    }
}