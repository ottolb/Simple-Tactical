//
// UICommonController.cs
//
// Author:
//       Otto Lopes <otto@buildandrungames.com>
//
// Copyright (c) 2018 Build and Run Games 
//


using System.Collections;
using System.Collections.Generic;
using Game.Event;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{

    public class UICommonController : BaseUIController
    {
        public static UICommonController Instance;




        protected override void Awake()
        {
            base.Awake();
            Instance = this;
        }



        protected override void ListenEvents()
        {
            /*	EventUIManager.StartListening(NUI.Loading.Show, OnShowLoading);
				EventUIManager.StartListening(NUI.Loading.Hide, OnHideLoading);*/

            EventManager.StartListening(N.App.Opened, OnAppOpened);

            EventUIManager.StartListening(NUI.Error.Show, OnShowError);

            EventUIManager.StartListening(NUI.Message.Show, OnShowMessage);

            EventUIManager.StartListening(NUI.Debug.Show, OnShowDebug);
        }

        void OnAppOpened(object p_desc)
        {
            fadeUI.Show(false);
            this.WaitForSecondsAndDo(1, delegate
            {
                fadeUI.FadeOut();
            });

        }

        void OnShowLoading(object p_desc)
        {
            loadingUI.Show(false);
            loadingUI.Setup((string)p_desc);
        }

        void OnHideLoading(object p_desc)
        {
            //loadingUI.Hide((string)p_desc);;
            loadingUI.Hide(false);
        }

        public static void HideLoading(string p_desc, float p_time)
        {
            Instance.WaitForSecondsAndDo(p_time, delegate
            {
                Instance.OnHideLoading(p_desc);
            });
        }

        void OnShowDebug(object p_version)
        {
            debugUI.Show(false);
            debugUI.Setup((string)p_version);
        }

        void OnShowError(object p_errorModel)
        {
            ErrorModel em = (ErrorModel)p_errorModel;
            errorUI.Setup(em);
            errorUI.Show(false);
        }

        void OnShowMessage(object p_messageModel)
        {
            MessageModel message = (MessageModel)p_messageModel;
            messagePopUp.Setup(message);
            messagePopUp.Show(false);
        }





        #region Properties

        private LoadingUI _loadingUI;

        public LoadingUI loadingUI
        {
            get
            {
                if (_loadingUI == null)
                    _loadingUI = CreateModal("LoadingUI", "LoadingUI", Canvas).GetComponent<LoadingUI>();
                return _loadingUI;
            }
        }

        private DebugUI _debugUI;

        public DebugUI debugUI
        {
            get
            {
                if (_debugUI == null)
                    _debugUI = CreateModal("DebugUI", "DebugUI", Canvas).GetComponent<DebugUI>();
                return _debugUI;
            }
        }

        private ErrorUI _errorUI;

        public ErrorUI errorUI
        {
            get
            {
                if (_errorUI == null)
                    _errorUI = CreateModal("Error PopUp", "ErrorUI", Canvas).GetComponent<ErrorUI>();
                return _errorUI;
            }
        }


        private MessagePopUp _messagePopUp;

        public MessagePopUp messagePopUp
        {
            get
            {
                if (_messagePopUp == null)
                    _messagePopUp = CreateModal("Message PopUp", "MessagePopUp", Canvas).GetComponent<MessagePopUp>();
                return _messagePopUp;
            }
        }

        private FadeUI _fadeUI;

        public FadeUI fadeUI
        {
            get
            {
                if (_fadeUI == null)
                    _fadeUI = CreateModal("FadeUI", "FadeUI", Canvas).GetComponent<FadeUI>();
                return _fadeUI;
            }
        }

        #endregion
    }
}