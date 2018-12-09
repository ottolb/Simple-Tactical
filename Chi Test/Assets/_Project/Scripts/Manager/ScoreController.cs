using UnityEngine;
using System;
using System.Collections;
using Game.Event;
using TMPro;
using System.Collections.Generic;
using DG.Tweening;

namespace Game
{
    public class ScoreController : BaseController
    {
        public static ScoreController Instance { get; private set; }

        public int Score { get; private set; }

        public int HighScore { get; private set; }

        public bool HasNewHighScore { get; private set; }


        // key name to store high score in PlayerPrefs
        private const string HIGHSCORE = "HIGHSCORE";
        private const string LEVEL = "LEVEL";

        private Transform ball;

        [Header("Score")]
        public int blockScore;
        public int multiplier;

        [Header("Star")]
        public int starScore;


        [Header("Reward")]
        public List<string> phrases;

        AudioSource audioSource;
        public float pitchIncrease;

        public int starsPicked;

        public GameObject scoreFX, rewardText;
        public float scoreFXFade;

        protected override void Awake()
        {
            base.Awake();
            if (Instance)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
            audioSource = GetComponent<AudioSource>();
        }

        protected override void ListenEvents()
        {
            //EventManager.StartListening(N.Block.Captured, OnBlockCaptured);

            EventManager.StartListening(N.Score.CoinPick, OnCoinPick);
            EventManager.StartListening(N.Game.Start, OnGameStarted);
            EventManager.StartListening(N.Level.NextLevel, OnNextLevel);
            EventManager.StartListening(N.Game.Over, OnGameOver);
        }

        void Start()
        {
            Reset();
        }

        public void Reset()
        {
            // Initialize score
            Score = 0;
            starsPicked = 0;
            audioSource.pitch = 1;


            // Initialize highscore
            HighScore = PlayerPrefs.GetInt(HIGHSCORE, 0);

            HasNewHighScore = false;
        }

        void AddScore(int amount)
        {
            Score += amount;

            // Fire event
            EventManager.TriggerEvent(N.Score.NewScore, Score);
            EventManager.TriggerEvent(N.Score.ScoreAdded, amount);

            if (Score > HighScore)
            {
                UpdateHighScore(Score);
                HasNewHighScore = true;
            }
            else
            {
                HasNewHighScore = false;
            }
        }

        void UpdateHighScore(int newHighScore)
        {
            // Update highscore if player has made a new one
            if (newHighScore > HighScore)
            {
                HighScore = newHighScore;
                PlayerPrefs.SetInt(HIGHSCORE, HighScore);
                //HighscoreUpdated(HighScore);
                EventManager.TriggerEvent(N.Score.NewHighscore, HighScore);
            }
        }

        void OnBlockCaptured(object p_block)
        {
            //Gameplay.BaseBlock t = (Gameplay.BaseBlock)p_block;
            //AddScore(blockScore);
            //string s = string.Format("+{0}", blockScore);
            //ShowScoreFX(s, t.transform);
        }

        void OnCoinPick(object p_star)
        {
            Transform t = (UnityEngine.Transform)p_star;
            AddScore(starScore);
            string s = string.Format("+{0}\n" + phrases[0], starScore);
            ShowScoreFX(s, t);
            starsPicked++;
        }

        void ShowScoreFX(string p_score, Transform p_transform)
        {
            Vector3 offset = Vector3.up * 2;

            GameObject go = CFX_SpawnSystem.GetNextObject(scoreFX);
            go.transform.position = p_transform.position + offset;

            ScoreFX aux = go.GetComponent<ScoreFX>();
            aux.Show(p_score, scoreFXFade);
        }

        void ShowCoinFX(string p_score, Transform p_transform)
        {
            Vector3 offset = Vector3.right * 2 - Vector3.forward * 3;
            GameObject go = Instantiate(rewardText, p_transform.position + offset, Quaternion.identity);
            TextMeshPro textMeshPro = go.GetComponentInChildren<TextMeshPro>();
            textMeshPro.text = p_score;
            go.SetActive(true);
        }

        void OnGameStarted(object asd)
        {
            starsPicked = 0;
            audioSource.pitch = 1;
        }

        void OnNextLevel(object p_data)
        {
            multiplier = 0;
            audioSource.pitch = 1;
            starsPicked = 0;
        }

        void OnGameOver(object p_data)
        {
            multiplier = 0;
            starsPicked = 0;
            audioSource.pitch = 1;
        }
    }
}
