using System.Collections;
using System.Collections.Generic;
using Game.Event;
using UnityEngine;

namespace Game
{
    public class BasePowerup : MonoBehaviour
    {

        BaseEventDispatcher eventDispatcher;
        public GameObject pickFX;
        public string audioFX;


        void Awake()
        {
            eventDispatcher = GetComponent<BaseEventDispatcher>();
        }

        void Start()
        {
            EventManager.StartListening(N.Level.Passed, OnLevelPassed);
            EventManager.StartListening(N.Level.Clean, OnLevelClean);
        }

        private void OnTriggerEnter()
        {
            Activate();
        }

        protected virtual void Activate()
        {
            eventDispatcher.Dispatch();

            GameObject go = CFX_SpawnSystem.GetNextObject(pickFX);
            go.transform.position = transform.position;

            AudioController.Play(audioFX);

            Reset();
        }

        void OnLevelPassed(object p_data)
        {
            Reset();
        }

        void OnLevelClean(object p_data)
        {
            Reset();
        }

        void Reset()
        {
            gameObject.SetActive(false);
        }

    }
}