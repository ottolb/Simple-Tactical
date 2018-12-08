//
// EventUIManager.cs
//
// Author:
//       Otto Lopes <otto@buildandrungames.com>
//
// Copyright (c) 2018 Build and Run Games 
//

using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Game.Event
{
    /// <summary>
    /// Generic UI (only) alerts events
    /// </summary>
    public class EventUIManager : MonoBehaviour
    {
        public bool logEvent;
        private Dictionary<string, UIEvent> eventDictionary;

        private static EventUIManager eventManager;

        public static EventUIManager instance
        {
            get
            {
                if (!eventManager)
                {
                    eventManager = FindObjectOfType(typeof(EventUIManager)) as EventUIManager;

                    if (!eventManager)
                    {
                        Debug.LogError("There needs to be one active EventUIManager script on a GameObject in your scene.");
                    }
                    else
                    {
                        eventManager.Init();
                    }
                }

                return eventManager;
            }
        }

        void Init()
        {
            if (eventDictionary == null)
            {
                eventDictionary = new Dictionary<string, UIEvent>();
            }
        }

        public static void StartListening(string eventName, UnityAction<object> listener)
        {
            UIEvent thisEvent = null;
            if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.AddListener(listener);
            }
            else
            {
                thisEvent = new UIEvent();
                thisEvent.AddListener(listener);
                instance.eventDictionary.Add(eventName, thisEvent);
            }
        }

        public static void StopListening(string eventName, UnityAction<object> listener)
        {
            if (eventManager == null)
                return;
            UIEvent thisEvent = null;
            if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
        }

        public static void TriggerEvent(string eventName, object p_object = null)
        {
            UIEvent thisEvent = null;
            if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                if (instance.logEvent)
                    Debug.Log("#Event# TriggerEvent: " + eventName);
                thisEvent.Invoke(p_object);
            }
        }
    }

    [System.Serializable]
    public class UIEvent : UnityEvent<object>
    {
    }
}