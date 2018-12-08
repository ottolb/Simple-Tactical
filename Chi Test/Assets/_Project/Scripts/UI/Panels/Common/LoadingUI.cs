//
// LoadingUI.cs
//
// Author:
//       Otto Lopes <otto@buildandrungames.com>
//
// Copyright (c) 2018 Build and Run Games 
//



using Game.Event;
using UnityEngine.UI;

namespace Game.UI
{
    public class LoadingUI : BaseUI
    {
        public Text loadText;
        public Image progressBar;
        public Text progressText;

        void Start()
        {
            EventUIManager.StartListening(NUI.Loading.UploadProgress, OnUploadProgress);
        }


        public void Setup(string p_desc)
        {
            loadText.text = p_desc;
            progressBar.gameObject.SetActive(false);
        }


        void OnUploadProgress(object p_progress)
        {
            long p = (long)p_progress;
            if (!progressBar.gameObject.activeSelf)
                progressBar.gameObject.SetActive(true);
            progressBar.fillAmount = p * 0.01f;
            progressText.text = p + "%";
        }
    }
}