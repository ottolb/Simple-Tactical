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

        private int currentUnitID;

        protected override void Start()
        {
            base.Start();

            EventManager.StartListening(N.Unit.PlayerUnits, OnPlayerUnitsSet);
            EventManager.StartListening(N.Team.StopAll, OnStopUnits);
        }

        protected override void Init()
        {
            base.Init();

            isUnitSelected = false;
            CreateUnits();
            SetupUnits();
        }

        protected override void CreateUnits()
        {
            base.CreateUnits();
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
            go.SetActive(true);
            NPC_Character unit = go.GetComponent<NPC_Character>();
            units.Add(unit);
        }

        void SetupUnits()
        {
            //Init all spawned units with a random preset
            foreach (var unit in units)
            {
                unit.Init(characterPresets[Random.Range(0, characterPresets.Length)]);
            }
        }

        protected override void StartTurn()
        {
            base.StartTurn();

            foreach (var unit in units)
            {
                unit.StartTurn();
            }

            EventUIManager.TriggerEvent(NUI.HUD.NPCTurn);

            currentUnitID = -1;
            CommandNextUnit();
        }

        void CommandNextUnit()
        {
            currentUnitID++;
            if (currentUnitID >= units.Count)
            {
                Debug.Log("#NPC TEAM# NPC Team finished turn");
                EventManager.TriggerEvent(N.Game.TurnFinished);
            }
            else
            {
                Debug.Log("#NPC TEAM# Command next unit " + currentUnitID);
                SelectUnit(units[currentUnitID]);
            }
        }

        void OnPlayerUnitsSet(object p_data)
        {
            playerUnits = (List<BaseCharacter>)p_data;
            foreach (var unit in units)
            {
                (unit as NPC_Character).PlayerUnits = playerUnits;
                (unit as NPC_Character).Allies = units;
            }
        }

        protected override void UnitEndedTurn(BaseCharacter p_unit)
        {
            base.UnitEndedTurn(p_unit);
            this.WaitForSecondsAndDo(0.5f, CommandNextUnit);
        }

        protected override void TeamDefeated()
        {
            base.TeamDefeated();
            EventManager.TriggerEvent(N.Team.Defeat, false);
        }

        void OnStopUnits(object p_data)
        {
            foreach (var unit in units)
            {
                (unit as NPC_Character).Stop();
            }
        }
    }
}