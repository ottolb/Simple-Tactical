using System.Collections;
using System.Collections.Generic;
using Game.Event;
using Game.GameInput;
using UnityEngine;

namespace Game.Gameplay
{

    public class BaseTeamController : MonoBehaviour
    {

        public int turnPriority;

        public int totalUnits;

        protected bool isPlaying, isMyTurn, isUnitSelected;

        protected List<BaseCharacter> units;

        protected BaseCharacter selectedUnit;


        protected virtual void Start()
        {
            EventManager.StartListening(N.Game.Setup, OnGameSetup);
            EventManager.StartListening(N.Game.Over, OnGameOver);
            EventManager.StartListening(N.Game.TurnChanged, OnTurnChanged);
        }

        protected virtual void Init()
        {
            isPlaying = true;
            EventManager.TriggerEvent(N.Game.RegisterUnitController, this);
        }

        protected virtual void Stop()
        {
            isPlaying = false;
            //todo: also unregister from controller??
            //EventManager.TriggerEvent(N.Game.UnregisterUnitController, this);
        }

        protected virtual void StartTurn()
        {
            isMyTurn = true;
        }

        void OnGameSetup(object p_data)
        {
            Init();
        }

        void OnGameOver(object p_data)
        {
            Stop();
        }

        void OnTurnChanged(object p_data)
        {
            if (p_data as BaseTeamController == this)
            {
                StartTurn();
            }
        }

        protected virtual void SelectUnit(BaseCharacter p_unit)
        {
            selectedUnit = p_unit;
            EventManager.TriggerEvent(N.Unit.SelectUnit, p_unit.transform);
            isUnitSelected = true;
        }

    }
}