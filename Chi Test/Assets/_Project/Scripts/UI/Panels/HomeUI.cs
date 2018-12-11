using TMPro;
using Game.Event;

namespace Game.UI
{
    public class HomeUI : BaseUI
    {
        void OnPlayClicked(ButtonView p_button)
        {
            EventUIManager.TriggerEvent(NUI.Home.Play);
        }
    }
}