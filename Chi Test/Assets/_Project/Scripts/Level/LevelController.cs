using System.Collections.Generic;
using Game.Event;
using Game.Gameplay;
using UnityEngine;


namespace Game.Level
{
    public class LevelController : MonoBehaviour
    {
        private Level level;

        private bool isPlaying;



        private void Awake()
        {
            level = GetComponent<Level>();
        }

        // Use this for initialization
        void Start()
        {
            ListenEvents();
        }

        void ListenEvents()
        {
            EventManager.StartListening(N.Game.Load, OnGameLoad);
            EventManager.StartListening(N.Game.Start, OnGameStart);
            EventManager.StartListening(N.Game.Over, OnGameOver);

            EventManager.StartListening(N.Ball.Reset, OnPlayerReset);
        }

        private void Update()
        {
            if (!isPlaying)
                return;
            CheckProgress();
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.L))
            {
                EventManager.TriggerEvent(N.Game.AllBlocksDestroyed, this);
            }
        }

        void CheckProgress()
        {

        }

        void OnGameLoad(object p_desc)
        {
            LoadLevel();
        }

        void OnPlayerReset(object p_data)
        {

        }

        void OnGameStart(object p_data)
        {
            isPlaying = true;
        }

        void OnGameOver(object p_data)
        {
            isPlaying = false;
        }

        void LoadLevel()
        {
            level.Load();
        }



    }
}