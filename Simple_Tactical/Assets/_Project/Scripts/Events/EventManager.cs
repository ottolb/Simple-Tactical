//
// EventManager.cs
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
    /// Generic App alerts events
    /// </summary>
    public class EventManager : MonoBehaviour
    {
        public bool logEvent;
        private Dictionary<string, DefaultEvent> eventDictionary;

        private static EventManager eventManager;

        public static EventManager instance
        {
            get
            {
                if (!eventManager)
                {
                    eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                    if (!eventManager)
                    {
                        Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
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
                eventDictionary = new Dictionary<string, DefaultEvent>();
            }
        }

        public static void StartListening(string eventName, UnityAction<object> listener)
        {
            DefaultEvent thisEvent = null;
            if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.AddListener(listener);
            }
            else
            {
                thisEvent = new DefaultEvent();
                thisEvent.AddListener(listener);
                instance.eventDictionary.Add(eventName, thisEvent);
            }
        }

        public static void StopListening(string eventName, UnityAction<object> listener)
        {
            if (eventManager == null)
                return;
            DefaultEvent thisEvent = null;
            if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
        }

        public static void TriggerEvent(string eventName, object p_object = null)
        {
            DefaultEvent thisEvent = null;
            if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                if (instance.logEvent)
                    Debug.Log("#Event# TriggerEvent: " + eventName);
                thisEvent.Invoke(p_object);
            }
        }
    }

    [System.Serializable]
    public class DefaultEvent : UnityEvent<object>
    {
    }
}