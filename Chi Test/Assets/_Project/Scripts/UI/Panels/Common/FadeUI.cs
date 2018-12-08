//
// FadeUI.cs
//
// Author:
//       Otto Lopes <otto@buildandrungames.com>
//
// Copyright (c) 2018 Build and Run Games 
//


using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Game.UI
{

    public class FadeUI : BaseUI
    {
        private DOTweenAnimation fadeIn, fadeOut;


        protected override void Awake()
        {
            DOTweenAnimation[] tweens = GetComponentsInChildren<DOTweenAnimation>();
            foreach (var item in tweens)
            {
                if (item.id == "Fade In")
                    fadeIn = item;
                else
                    fadeOut = item;
            }
        }


        public void FadeIn()
        {
            gameObject.SetActive(true);
            fadeIn.DORestartById("Fade In");
        }

        public void FadeOut()
        {
            gameObject.SetActive(true);
            fadeOut.DORestartById("Fade Out");
        }

        public void OnCompleteFadeIn()
        {
            //gameObject.SetActive(false);
        }

        public void OnCompleteFadeOut()
        {
            gameObject.SetActive(false);
        }
    }
}