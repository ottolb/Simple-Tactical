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

            EventManager.StartListening(N.Unit.ActionTaken, OnUnitActionTaken);
            EventManager.StartListening(N.Unit.Died, OnUnitDied);
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
            else
                isMyTurn = false;

        }

        void OnUnitActionTaken(object p_data)
        {
            BaseCharacter character = (BaseCharacter)p_data;
            if (units.Contains(character))
            {
                ActionTakenByUnit(character);
            }
        }

        protected virtual void ActionTakenByUnit(BaseCharacter p_unit)
        {

        }

        protected virtual void SelectUnit(BaseCharacter p_unit)
        {
            selectedUnit = p_unit;
            EventManager.TriggerEvent(N.Unit.SelectUnit, p_unit.transform);
            isUnitSelected = true;
        }

        protected virtual void CreateUnits()
        {
            units = new List<BaseCharacter>();
        }

        void OnUnitDied(object p_data)
        {
            BaseCharacter character = (BaseCharacter)p_data;
            if (units.Contains(character))
            {
                units.Remove(character);
            }
            if (units.Count == 0)
            {
                EventManager.TriggerEvent(N.Team.Defeat, false);
            }
        }
    }
}