using System.Collections;
using System.Collections.Generic;
using Game.Event;
using Game.GameInput;
using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Player Units controllers
    /// </summary>
    public class NPCTeamController : BaseTeamController
    {
        public GameObject playerPrefab;

        public List<BaseCharacter> playerUnits;

        private BaseCharacter selectedUnit;

        protected override void Start()
        {
            base.Start();

            EventManager.StartListening(N.Unit.PlayerUnits, OnPlayerUnitsSet);
            //EventManager.StartListening(N.GameBalance.Updated, OnBalanceUpdated);

            //UI events
            //EventUIManager.StartListening(NUI.HUD.EndTurn, OnPlayerTurnEndAction);
        }

        protected override void Init()
        {
            base.Init();

            isUnitSelected = false;
            units = new List<BaseCharacter>();
            EventManager.StartListening(N.Level.SetNPC_SP, OnSpawnPointSet);
            for (int i = 0; i < totalUnits; i++)
            {
                EventManager.TriggerEvent(N.Level.RequestNPC_SP);
            }
        }

        void OnSpawnPointSet(object p_data)
        {
            Transform point = (UnityEngine.Transform)p_data;
            GameObject go = Instantiate(playerPrefab, point.position, point.rotation);
            units.Add(go.GetComponent<BaseCharacter>());
        }

        protected override void StartTurn()
        {
            base.StartTurn();

            foreach (var unit in units)
            {
                unit.Init();
            }

            EventUIManager.TriggerEvent(NUI.HUD.NPCTurn);

            selectedUnit = units[0];
            SelectUnit(selectedUnit.transform);

            FindNearestPlayerUnit();

        }

        void FindNearestPlayerUnit()
        {
            float distance = Mathf.Infinity;
            float auxD;
            BaseCharacter nearestPlayerUnit = null;
            foreach (var unit in playerUnits)
            {
                auxD = Vector3.Distance(unit.transform.position, selectedUnit.transform.position);
                if (auxD < distance)
                {
                    distance = auxD;
                    nearestPlayerUnit = unit;
                }
            }

            if (nearestPlayerUnit)
                MoveUnit(nearestPlayerUnit.transform.position);
        }


        void MoveUnit(Vector3 p_pos)
        {
            selectedUnit.Move(p_pos);
        }


        void OnPlayerUnitsSet(object p_data)
        {
            playerUnits = (List<BaseCharacter>)p_data;
        }
    }
}