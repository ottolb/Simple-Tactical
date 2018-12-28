using System.Collections.Generic;
using Game.Event;
using UnityEngine;

namespace Game.Gameplay
{

    /// <summary>
    /// Handle Teams Controller turn
    /// </summary>
    public class TurnController : MonoBehaviour
    {

        private bool isPlaying, isWaiting;

        private List<BaseTeamController> unitsController;

        private int turnIndex;

        private void Awake()
        {
            unitsController = new List<BaseTeamController>();
        }

        void Start()
        {
            EventManager.StartListening(N.Game.Start, OnGameStarted);
            EventManager.StartListening(N.Game.Over, OnGameOver);

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

                Debug.LogFormat("#Turn Controller# Turn Changed to {0}", unitsController[turnIndex].name);
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
            Debug.LogFormat("#Turn Controller# {0}'s turn finished", unitsController[turnIndex].name);
            isWaiting = false;
            turnIndex++;
            if (turnIndex >= unitsController.Count)
                turnIndex = 0;
        }
    }
}