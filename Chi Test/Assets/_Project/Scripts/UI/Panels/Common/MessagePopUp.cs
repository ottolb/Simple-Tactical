//
// MessagePopUp.cs
//
// Author:
//       Otto Lopes <otto@buildandrungames.com>
//
// Copyright (c) 2018 Build and Run Games 
//
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
	public class MessagePopUp : BaseUI
	{
		public Text titleText;
		public Text messageText;

		public Text okBt;

		public Text yesBt, noBt;

		public Transform ctr1Button, ctr2Buttons;

		private Action<MessageModel.Result> _callback;


		public void Setup(MessageModel p_model)
		{
			Setup(p_model.title, p_model.message);

			//Toggle button containers based on type
			ctr1Button.gameObject.SetActive(p_model.type == MessageModel.Type.OneOption);
			ctr2Buttons.gameObject.SetActive(p_model.type == MessageModel.Type.TwoOptions);

			_callback = p_model.callback;

			SetupButtonText(p_model);
		}

		public void Setup(string p_title, string p_message)
		{
			titleText.text = p_title;
			messageText.text = p_message;
		}

		void SetupButtonText(MessageModel p_model)
		{
			yesBt.text = p_model.yesBtText;
			noBt.text = p_model.noBtText;
			okBt.text = p_model.okBtText;
		}

		#region Button Events

		void OnYesClicked(ButtonView p_button)
		{
			Hide(false);
			if(_callback != null)
				_callback(MessageModel.Result.YES);
		}

		void OnNoClicked(ButtonView p_button)
		{
			Hide(false);
			if(_callback != null)
				_callback(MessageModel.Result.NO);
		}

		void OnOkClicked(ButtonView p_button)
		{
			Hide(false);
			if(_callback != null)
				_callback(MessageModel.Result.OK);
		}
		#endregion
	}

	public class MessageModel
	{
		public string title;
		public string message;

		public string okBtText;
		public string yesBtText;
		public string noBtText;

		public Type type;

		public Action<Result> callback;

		public enum Type
		{ OneOption, TwoOptions }

		public enum Result
		{ YES, NO, OK }

		public MessageModel()
		{

		}

		public MessageModel(string p_message, string p_title = "Error")
		{
			title = p_title;
			message = p_message;

			okBtText = "OK";
			yesBtText = "Yes";
			noBtText = "No";
		}
	}
}