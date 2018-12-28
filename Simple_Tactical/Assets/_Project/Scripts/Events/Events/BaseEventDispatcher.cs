using UnityEngine;

namespace Game.Event
{
    public class BaseEventDispatcher : MonoBehaviour
    {
        [Tooltip("The event id")]
        public string eventId;
        public enum Type
        {
            UI,
            App,
            Server
        }
        public Type type;

        public bool onStart;

        protected virtual void Start()
        {
            if (onStart)
                Dispatch();
        }

        public virtual void Dispatch()
        {
            if (string.IsNullOrEmpty(eventId))
                return;

            switch (type)
            {
                case Type.App:
                    EventManager.TriggerEvent(eventId);
                    break;
                case Type.UI:
                    EventUIManager.TriggerEvent(eventId);
                    break;
                case Type.Server:
                    //EventServerMgr.TriggerEvent(eventId);
                    break;

            }
        }
    }
}
