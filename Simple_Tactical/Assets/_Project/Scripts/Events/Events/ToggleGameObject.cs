using UnityEngine;

namespace Game.Event
{
    public class ToggleGameObject : BaseEventListener
    {
        public bool activate;

        public override void OnEvent(object p_data)
        {
            gameObject.SetActive(activate);
        }
    }
}