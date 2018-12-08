//
// UIOrderManager.cs
//
// Author:
//       Otto Lopes <otto@buildandrungames.com>
//
// Copyright (c) 2018 Build and Run Games 
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
	public class UIOrderManager : MonoBehaviour
	{
		static Transform _tr;
		static UIOrderManager _instance;
		public List<UIRect> canvasList;

		void Awake()
		{
			_tr = transform;
			_instance = this;
			if(canvasList == null)
				canvasList = new List<UIRect>();
		}

		void Sort()
		{
			canvasList.Sort(delegate (UIRect uiRect1, UIRect uiRect2)
			{
				return uiRect1.order.CompareTo(uiRect2.order);
			});

			foreach(var canvas in canvasList)
			{
				canvas.canvas.SetAsLastSibling();
			}
		}

		public static RectTransform GetCanvas(string p_name, int p_order = 0)
		{
			RectTransform _canvas;
			_canvas = (RectTransform)_tr.Find(p_name);
			if(_canvas == null)
			{
				_canvas = AddCanvas(p_name);
				_instance.canvasList.Add(new UIRect() { canvas = _canvas, order = p_order });
			}


			_instance.Sort();

			return _canvas;
		}

		public static RectTransform AddCanvas(string p_name)
		{
			RectTransform _canvas = (RectTransform)Instantiate(AssetsManager.GetPrefabs("Default Panel")).transform;
			_canvas.transform.SetParent(_tr, false);
			_canvas.name = p_name;

			return _canvas;
		}

		[System.Serializable]
		public class UIRect
		{
			public RectTransform canvas;
			public int order;
		}
	}
}