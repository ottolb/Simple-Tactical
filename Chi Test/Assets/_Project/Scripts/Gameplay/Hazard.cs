using System.Collections;
using System.Collections.Generic;
using Game.GameInput;
using UnityEngine;
using Game.Event;


namespace Game.Gameplay
{

    public class Hazard : MonoBehaviour
    {
        public float speed;

        private Rigidbody _rigidbody;

        bool isForward;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        void Start()
        {
            EventManager.StartListening(N.Level.Passed, OnLevelPassed);
            EventManager.StartListening(N.Level.Clean, OnLevelClean);
            EventManager.StartListening(N.Game.Start, OnGameStart);

            isForward = true;
        }

        private void OnDisable()
        {
            _rigidbody.velocity = Vector3.zero;
        }

        void Reset()
        {
            gameObject.SetActive(false);
        }

        void OnLevelPassed(object p_data)
        {
            Reset();
        }

        void OnLevelClean(object p_data)
        {
            Reset();
        }

        void OnGameStart(object p_data)
        {
            ApplyForce();
        }

        void OnTriggerEnter(Collider p_collider)
        {
            //Debug.Log("#Hazard# Path Trigger");
            if (p_collider.tag.Equals("Path Trigger"))
            {
                isForward = !isForward;
                ApplyForce();
            }
        }

        void ApplyForce()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce((isForward ? speed : -speed) * transform.forward, ForceMode.Impulse);
        }
    }
}