//
// BaseUIController.cs
//
// Author:
//       Otto Lopes <otto@buildandrungames.com>
//
// Copyright (c) 2018 Build and Run Games 
//


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{

    public class BaseUIController : MonoBehaviour
    {
        public string canvasName;

        public int order;

        private RectTransform _canvas;

        public bool autoHideAllPanels;

        public List<BaseUI> canvasList;


        public Dictionary<string, ICanvas> canvasDict;


        protected virtual void Awake()
        {
            ListenEvents();
            canvasDict = new Dictionary<string, ICanvas>();
        }


        protected virtual void Start()
        {
            foreach (var canvas in canvasList)
            {
                canvasDict.Add(canvas.name, canvas);
            }
            if (autoHideAllPanels)
                HideAll();
        }

        protected virtual void ListenEvents()
        {
        }

        void HideAll()
        {
            foreach (var item in canvasList)
            {
                item.Hide(false);
            }
        }

        public bool IsShowing()
        {
            foreach (var item in canvasList)
            {
                if (!item.IsHided)
                    return true;
            }

            return false;
        }


        public virtual void UIShow(BaseUI p_ui)
        {
            if (p_ui.isStack)
                UIStackController.Instance.UIShow(p_ui);
        }

        public virtual void UIHide(BaseUI p_ui)
        {
            /*if(p_ui.isStack)
				UIStackController.Instance.UIHide(p_ui);*/
        }

        public GameObject CreateModal(string p_prefab, string p_canvasKey = null, Transform p_parent = null)
        {
            GameObject go = null;
            BaseUI canvas;
            if (p_canvasKey != null && canvasDict.ContainsKey(p_canvasKey))
            {
                canvas = (canvasDict[p_canvasKey] as BaseUI);
                go = canvas.gameObject;
            }
            else
            {
                go = Instantiate(AssetsManager.GetPrefabs(p_prefab));
                go.transform.SetParent(p_parent, false);/*
				go.transform.localScale = Vector3.one;
				go.transform.localPosition = Vector3.zero;*/
                canvas = go.GetComponent<BaseUI>();
            }
            canvas.SetController(this);
            canvasList.Add(canvas);
            return go;
        }

        protected RectTransform Canvas
        {
            get
            {
                if (_canvas == null)
                {
                    _canvas = UIOrderManager.GetCanvas(canvasName, order);
                }
                return _canvas;
            }
        }
    }
}