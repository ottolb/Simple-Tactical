//
// BaseUI.cs
//
// Author:
//       Otto Lopes <otto@buildandrungames.com>
//
// Copyright (c) 2018 Build and Run Games 
//


using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

namespace Game.UI
{

    public class BaseUI : MonoBehaviour, ICanvas
    {
        protected Animator anim;

        protected BaseUIController controller;

        protected DOTweenAnimation tweenShow;

        public bool isStack;

        protected RectTransform _rectTransform;

        protected virtual void Awake()
        {
            anim = GetComponent<Animator>();
            tweenShow = GetComponent<DOTweenAnimation>();
            _rectTransform = transform as RectTransform;
            SetupButtonCallback();
        }

        public virtual void Show(bool p_animated)
        {
            gameObject.SetActive(true);
            if (p_animated && anim != null)
                anim.Play("Show");

            controller.UIShow(this);
        }

        public virtual void Hide(bool p_animated)
        {
            if (p_animated && anim != null)
                anim.Play("Hide");
            else
                gameObject.SetActive(false);

            controller.UIHide(this);
        }

        public bool IsHided
        {
            get { return !gameObject.activeInHierarchy; }
        }

        /// <summary>
        /// Automatically find button childs and setup a callback
        /// </summary>
        public void SetupButtonCallback()
        {
            ButtonView[] buttons = gameObject.GetComponentsInChildren<ButtonView>(true);

            UnityAction<ButtonView> action;
            // get the method assigned in the editor and call it
            MethodInfo methodInfo;
            Callback callback;
            foreach (ButtonView button in buttons)
            {
                if (string.IsNullOrEmpty(button.methodName))
                    continue;

                methodInfo = UnityEventBase.GetValidMethodInfo(this, button.methodName,
                    new System.Type[] { typeof(ButtonView) });
                if (methodInfo == null)
                {
                    Debug.LogWarning(string.Format("Can't find method {0} on script {1}", button.methodName, this.name));
                    continue;
                }
                //Debug.Log(methodInfo.Name);

#if NETFX_CORE
			//Windows phone case
			callback = (Callback)methodInfo.CreateDelegate(typeof(Callback), this);
#else
                callback = (Callback)System.Delegate.CreateDelegate(typeof(Callback), this, button.methodName);
#endif
                action = new UnityAction<ButtonView>(callback);
                button.evt.AddListener(action);
            }
        }

        public void SetController(BaseUIController p_controller)
        {
            controller = p_controller;
        }


        public delegate void Callback(ButtonView p_object);

    }
}