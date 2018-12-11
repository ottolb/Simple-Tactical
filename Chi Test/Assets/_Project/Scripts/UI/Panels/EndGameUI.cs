//
// EndGameUI.cs
//
// Author:
//       Otto Lopes <otto@buildandrungames.com>
//
// Copyright (c) 2018 Build and Run Games 
//

using TMPro;
using Game.Event;

namespace Game.UI
{
    public class EndGameUI : BaseUI
    {
        private TextMeshProUGUI messageT;

        protected override void Awake()
        {
            base.Awake();

            messageT = transform.Find("Message T").GetComponent<TextMeshProUGUI>();
        }

        public void Setup(bool p_isPlayerDefeat)
        {
            if (p_isPlayerDefeat)
                messageT.text = "You Lose";
            else
                messageT.text = "You Win";
        }

        void OnPlayClicked(ButtonView p_button)
        {
            EventUIManager.TriggerEvent(NUI.EndGame.Restart);
        }
    }
}
