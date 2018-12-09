﻿using System.Collections;
using System.Collections.Generic;
using Game.Event;
using Game.GameInput;
using UnityEngine;

namespace Game.Gameplay
{

    public class TurnController : MonoBehaviour
    {

        bool isPlaying, isWaiting;

        public List<BaseTeamController> unitsController;

        public int turnIndex;

        void Start()
        {
            EventManager.StartListening(N.GameBalance.Updated, OnBalanceUpdated);

            EventManager.StartListening(N.Game.Start, OnGameStarted);
            EventManager.StartListening(N.Game.Over, OnGameStarted);

            EventManager.StartListening(N.Game.RegisterUnitController, OnRegisterUnitController);
            EventManager.StartListening(N.Game.TurnFinished, OnTurnFinished);
        }

        private void Update()
        {
            if (isPlaying)
            {
                //todo: Perhaps adding a timer to force player end his turn after some time
                if (isWaiting)
                    return;

                Debug.LogFormat("#Turn Controller# TurnChanged", unitsController[turnIndex].name);
                EventManager.TriggerEvent(N.Game.TurnChanged, unitsController[turnIndex]);
                isWaiting = true;
            }
        }


        void OnGameStarted(object p_data)
        {
            isWaiting = false;
            isPlaying = true;
            turnIndex = 0;
        }

        void OnGameOver(object p_data)
        {
            isPlaying = false;
        }

        void OnRegisterUnitController(object p_data)
        {
            BaseTeamController controller = (BaseTeamController)p_data;
            if (!unitsController.Contains(controller))
                unitsController.Add(controller);

            Debug.LogFormat("#Turn Controller# Unit Controller {0} registered", controller.name);

            unitsController.Sort(delegate (BaseTeamController p_controller1, BaseTeamController p_controller2)
            {
                return p_controller1.turnPriority.CompareTo(p_controller2.turnPriority);
            });
        }

        void OnTurnFinished(object p_data)
        {
            Debug.Log("#Turn Controller# Turn finished");
            isWaiting = false;
            turnIndex++;
            if (turnIndex >= unitsController.Count)
                turnIndex = 0;
        }

        void OnBalanceUpdated(object p_data)
        {

        }



    }
}