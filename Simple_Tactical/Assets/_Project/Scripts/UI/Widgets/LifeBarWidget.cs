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

        Billboard billboard;
        public AnimationCurve scaleByDistance;
        Transform ctr;
        public float maxCameraDistance;


        void Awake()
        {
            ctr = transform.Find("Ctr");
            lifeT = ctr.Find("Life Text").GetComponent<TextMeshProUGUI>();
            lifeBar = ctr.Find("Life Bar/Filler").GetComponent<Image>();
            billboard = GetComponent<Billboard>();
        }

        private void Update()
        {
            if (billboard && billboard.cameraToLook)
            {
                float distance = Vector3.Distance(transform.position, billboard.cameraToLook.transform.position);
                ctr.localScale = Vector3.one * scaleByDistance.Evaluate(distance / maxCameraDistance);
            }
        }

        public void UpdateLife(int p_current, int p_total)
        {
            if (barTween != null)
                barTween.Complete();

            float clamped = p_current / (float)p_total;

            barTween = DOTween.To(() => lifeBar.fillAmount, x => lifeBar.fillAmount = x, clamped, speedBarFx);

            lifeT.text = string.Format("{0}/{1}", p_current, p_total);
        }

    }
}