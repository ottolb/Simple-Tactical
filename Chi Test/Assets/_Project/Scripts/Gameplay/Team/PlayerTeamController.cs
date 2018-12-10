﻿using System.Collections;
using System.Collections.Generic;
using Game.Event;
using Game.GameInput;
using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Player Units controllers
    /// </summary>
    public class PlayerTeamController : BaseTeamController
    {
        BaseInput _input;
        Camera cam;

        public LayerMask floorLayerMask, unitSelectionLayerMask;

        public GameObject playerPrefab;





        protected override void Start()
        {
            base.Start();
            _input = BaseInput.SelectInput(gameObject);
            cam = Camera.main;
            EventManager.StartListening(N.GameBalance.Updated, OnBalanceUpdated);

            //UI events
            EventUIManager.StartListening(NUI.HUD.EndTurn, OnPlayerTurnEndAction);
            EventUIManager.StartListening(NUI.HUD.WaitAction, OnWaitAction);
        }

        protected override void Init()
        {
            base.Init();

            isUnitSelected = false;
            CreateUnits();
        }

        void CreateUnits()
        {
            units = new List<BaseCharacter>();
            EventManager.StartListening(N.Level.SetPlayerSP, OnSpawnPointSet);
            for (int i = 0; i < totalUnits; i++)
            {
                EventManager.TriggerEvent(N.Level.RequestPlayerSP);
            }

            EventManager.TriggerEvent(N.Unit.PlayerUnits, units);
        }

        void OnSpawnPointSet(object p_data)
        {
            Transform point = (UnityEngine.Transform)p_data;
            GameObject go = Instantiate(playerPrefab, point.position, point.rotation);
            units.Add(go.GetComponent<BaseCharacter>());
            (units[units.Count - 1] as PlayerCharacter).Setup(Random.Range(0, 1));
        }

        protected override void StartTurn()
        {
            base.StartTurn();

            foreach (var unit in units)
            {
                unit.Init();
            }

            EventUIManager.TriggerEvent(NUI.HUD.PlayerTurn);

            SelectUnit(units[0]);
        }

        void Update()
        {
            if (!isPlaying || !isMyTurn)
            {
                return;
            }

            if (CheckUnitSelection())
            {
                return;
            }

            if (isUnitSelected && CheckMoveRequest())
            {

            }
        }

        bool CheckMoveRequest()
        {
            Ray ray = cam.ScreenPointToRay(_input.pos);
            //Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000, floorLayerMask))
            {
                var hitPosition = hit.point;

                //cubeHitPos.transform.position = hitPosition;

                if (_input.WasClicked())
                {
                    EventManager.TriggerEvent(N.Unit.MoveUnit, hitPosition);
                }
                else
                    EventManager.TriggerEvent(N.Unit.CheckUnitMovement, hitPosition);


            }
            return false;
        }

        bool CheckUnitSelection()
        {
            Ray ray = cam.ScreenPointToRay(_input.pos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000, unitSelectionLayerMask))
            {
                if (_input.WasClicked())
                {
                    BaseCharacter character = hit.collider.GetComponentInParent<BaseCharacter>();
                    if (units.Contains(character))
                    {
                        SelectUnit(character);
                    }
                    else
                    {
                        AttackEnemy(character);
                    }

                    return true;
                }
                else
                    EventManager.TriggerEvent(N.Unit.HoverUnit, hit.collider.transform.parent);
            }
            return false;
        }

        void OnBalanceUpdated(object p_data)
        {

        }

        void OnPlayerTurnEndAction(object p_data)
        {
            EventManager.TriggerEvent(N.Game.TurnFinished);
        }

        void OnWaitAction(object p_data)
        {
            selectedUnit.isWaiting = true;
            Debug.LogFormat("#Character# Character {0} won't attack ", selectedUnit.name);


            BaseCharacter unit = units.Find(delegate (BaseCharacter p_unit)
            {
                return !p_unit.isWaiting;
            });

            if (unit)
                SelectUnit(unit);

            EventUIManager.TriggerEvent(NUI.HUD.SetActionButton, unit == null);
        }

        void AttackEnemy(BaseCharacter p_unit)
        {
            //selectedUnit.Attack()
            Debug.LogFormat("#Character# Character {0} will attack {1}", selectedUnit.name, p_unit.name);
            if (selectedUnit.CheckAttack(p_unit))
                selectedUnit.Attack(p_unit);
            else
                Debug.LogFormat("#Character# Character {0} is far to attack {1}", selectedUnit.name, p_unit.name);
        }
    }
}