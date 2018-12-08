using System.Collections.Generic;
using Game.Event;
using Game.Gameplay;
using UnityEngine;


namespace Game.Level
{
    public class LevelController : MonoBehaviour
    {
        private Level level;

        public int testLevelId;
        public bool testLevel;

        private bool isPlaying;



        public AnimationCurve blockPWQuantity;


        public AnimationCurve hazardsQuantity;
        public AnimationCurve hazardsScale;


        int capturedBlocks, initialBlocks, nextLevelBlocks, lostBlocks;

        public AnimationCurve moreBlocksQuantity;


        private void Awake()
        {
            level = GetComponent<Level>();

            if (Application.isEditor && testLevel)
            {
                testLevelId = testLevelId - 1;
                CurrentLevel = testLevelId;
                PlayerPrefs.SetInt("current_level", CurrentLevel);
            }
            else
            {
                CurrentLevel = 0;
            }
            LevelSplitCount = 10;
            capturedBlocks = 0;
        }

        // Use this for initialization
        void Start()
        {
            ListenEvents();
        }

        void ListenEvents()
        {
            EventManager.StartListening(N.Level.Load, LoadLevel);
            EventManager.StartListening(N.Game.Start, OnGameStart);
            EventManager.StartListening(N.Game.Over, OnGameOver);

            EventManager.StartListening(N.Ball.Reset, OnPlayerReset);
            EventManager.StartListening(N.Level.Passed, OnLevelPassed);

            EventManager.StartListening(N.Block.Died, BlockDied);
            EventManager.StartListening(N.Block.Captured, BlockCaptured);
            EventManager.StartListening(N.Game.AllBlocksDestroyed, OnAllBlocksDestroyed);

            EventManager.StartListening(N.Powerup.MoreBlocksPicked, OnMoreBlocksPicked);

            EventUIManager.StartListening(NUI.EndGame.LevelSlider, OnLevelSlider);
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

        void LoadLevel(object p_desc)
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
            EventManager.StopListening(N.Block.Created, BlockCreated);
        }

        void LoadLevel()
        {
            if (!Application.isEditor && !testLevel)
                CurrentLevel = PlayerPrefs.GetInt("current_level", 0);

            Debug.Log("InitialBlocks " + GameBalance.Instance.InitialBlocks);
            if (BlocksCaptured == 0)
            {
                initialBlocks = GameBalance.Instance.InitialBlocks;
            }
            else
            {
                initialBlocks = BlocksCaptured;
            }

            lostBlocks = nextLevelBlocks = 0;
            BlocksCaptured = 0;
            EventUIManager.TriggerEvent(NUI.HUD.MoreBlocks, nextLevelBlocks);

            float split = (float)CurrentLevel / (float)LevelSplitCount;
            Debug.Log("#Level#  split: " + split);
            split = hazardsScale.Evaluate(split);
            Debug.Log("#Level#  curve scale: " + split);
            //float min = split * GameBalance.Instance.HazardMinScale;
            float max = split * GameBalance.Instance.HazardMaxScale;
            level.SetHazardScale(GameBalance.Instance.HazardMinScale, max);
            level.Load(initialBlocks, hazardsQuantity.Evaluate(CurrentLevel));

            level.SpawnPowerups(hazardsQuantity.Evaluate(CurrentLevel));

            this.WaitForSecondsAndDo(0.3f, delegate
            {
                EventManager.StartListening(N.Block.Created, BlockCreated);
            });

        }

        void NextLevel()
        {
            isPlaying = true;

            CurrentLevel++;
            PlayerPrefs.SetInt("current_level", CurrentLevel);

            EventManager.StopListening(N.Block.Created, BlockCreated);

            EventManager.TriggerEvent(N.Level.Load);

            EventManager.TriggerEvent(N.Level.NextLevel, CurrentLevel);
        }

        void OnLevelPassed(object p_data)
        {
            //mainCylinder.Toggle(false);
            isPlaying = false;

            Debug.Log("#Level Controller# OnLevelPassed");
            this.WaitForSecondsAndDo(1.9f, NextLevel);
        }

        #region Blocks
        void BlockCreated(object p_block)
        {
            Debug.Log("#Level# BlockCreated: " + initialBlocks);
            initialBlocks++;
            EventUIManager.TriggerEvent(NUI.HUD.Blocks, initialBlocks - lostBlocks);
        }

        void BlockDied(object p_block)
        {
            lostBlocks++;
            EventUIManager.TriggerEvent(NUI.HUD.Blocks, initialBlocks - lostBlocks);
        }

        void BlockCaptured(object p_block)
        {
            BlocksCaptured++;
        }

        void OnAllBlocksDestroyed(object p_data)
        {
            if (capturedBlocks == 0)
                EventManager.TriggerEvent(N.Game.Over);
            else
                EventManager.TriggerEvent(N.Level.Passed);
        }

        void OnMoreBlocksPicked(object p_data)
        {
            nextLevelBlocks++;
            Debug.Log("#Level# More Blocks Picked: " + nextLevelBlocks);
            EventUIManager.TriggerEvent(NUI.HUD.MoreBlocks, nextLevelBlocks);

            level.SpawnExraBlock();
        }

        #endregion

        void OnLevelSlider(object p_data)
        {
            testLevelId = (int)p_data - 1;
            CurrentLevel = testLevelId;
        }

        #region Properties

        public static int LevelSplitCount;

        public static int CurrentLevel;

        public int BlocksCaptured
        {
            get
            {
                return capturedBlocks;
            }
            set
            {
                capturedBlocks = value;
                EventUIManager.TriggerEvent(NUI.HUD.Blocks, initialBlocks - lostBlocks);
            }
        }

        #endregion
    }
}