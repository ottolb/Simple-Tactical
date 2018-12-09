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

        bool isUnitSelected;



        protected override void Start()
        {
            base.Start();
            _input = BaseInput.SelectInput(gameObject);
            cam = Camera.main;
            EventManager.StartListening(N.GameBalance.Updated, OnBalanceUpdated);

            //UI events
            EventUIManager.StartListening(NUI.HUD.EndTurn, OnPlayerTurnEndAction);
        }

        protected override void Init()
        {
            base.Init();

            isUnitSelected = false;
            EventManager.StartListening(N.Level.SetPlayerSP, OnSpawnPointSet);
            for (int i = 0; i < totalUnits; i++)
            {
                EventManager.TriggerEvent(N.Level.RequestPlayerSP);
            }
        }

        void OnSpawnPointSet(object p_data)
        {
            Transform point = (UnityEngine.Transform)p_data;
            GameObject go = Instantiate(playerPrefab, point.position, point.rotation);
        }

        protected override void StartTurn()
        {
            base.StartTurn();
            EventUIManager.TriggerEvent(NUI.HUD.PlayerTurn);
        }

        void Update()
        {
            if (!isPlaying)
            {
                return;
            }

            if (isMyTurn)
            {
                if (CheckUnitSelection())
                {
                    return;
                }
                //isMyTurn = false;
                //EventManager.TriggerEvent(N.Game.TurnFinished, this);
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
                    EventManager.TriggerEvent(N.Player.MoveUnit, hitPosition);
                }
                else
                    EventManager.TriggerEvent(N.Player.CheckUnitMovement, hitPosition);


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
                    EventManager.TriggerEvent(N.Player.SelectUnit, hit.collider.transform.parent);
                    isUnitSelected = true;
                    return true;
                }
                else
                    EventManager.TriggerEvent(N.Player.HoverUnit, hit.collider.transform.parent);
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
    }
}