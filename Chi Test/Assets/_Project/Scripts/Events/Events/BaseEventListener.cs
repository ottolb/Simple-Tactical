using UnityEngine;

namespace Game.Event
{
    public class BaseEventListener : MonoBehaviour
    {

        public string eventId;
        public enum Type
        {
            UI,
            App,
            Server
        }
        public Type type;

        protected virtual void Start()
        {
            if (string.IsNullOrEmpty(eventId))
                return;

            switch (type)
            {
                case Type.App:
                    EventManager.StartListening(eventId, OnEvent);
                    break;
                case Type.UI:
                    EventUIManager.StartListening(eventId, OnEvent);
                    break;
                case Type.Server:
                    //EventServerMgr.StartListening(eventId, OnEvent);
                    break;

            }
        }

        public virtual void OnEvent(object p_data)
        {
            Debug.Log("#EventListener# OnEvent " + eventId);
        }


    }
}
