//
// ErrorUI.cs
//
// Author:
//       Otto Lopes <otto@buildandrungames.com>
//
// Copyright (c) 2018 Build and Run Games 
//



using System;
using UnityEngine.UI;

namespace Game.UI
{
	public class ErrorUI : BaseUI
	{
		public Text titleText;
		public Text messageText;


		private Action _callback;

		public void Setup(ErrorModel p_errorModel)
		{
			Setup(p_errorModel.title, p_errorModel.message);
			_callback = p_errorModel.callback;
		}

		public void Setup(string p_title, string p_message)
		{
			titleText.text = p_title;
			messageText.text = p_message;
		}


		#region Button Events

		void OnCloseClicked(ButtonView p_button)
		{
			Hide(false);
			if(_callback != null)
				_callback();
		}
		#endregion
	}

	public class ErrorModel
	{
		public string title;
		public string message;

		public Action callback;

		public ErrorModel(string p_m, string p_t = "Error")
		{
			title = p_t;
			message = p_m;
		}
	}
}