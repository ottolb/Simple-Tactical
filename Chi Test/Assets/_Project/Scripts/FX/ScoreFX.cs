using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game.Event;
using TMPro;
using UnityEngine;

namespace Game
{
    public class ScoreFX : MonoBehaviour
    {
        TextMeshPro textMeshPro;

        [Range(0, 1)]
        public float finalAlpha;
        float initAlpha;

        public float speed;

        private void Awake()
        {
            textMeshPro = GetComponentInChildren<TextMeshPro>();
            initAlpha = textMeshPro.alpha;
        }


        public void Show(string p_text, float p_duration)
        {
            textMeshPro.text = p_text;
            textMeshPro.alpha = initAlpha;
            textMeshPro.DOFade(0, p_duration);
        }

        private void Update()
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
    }
}