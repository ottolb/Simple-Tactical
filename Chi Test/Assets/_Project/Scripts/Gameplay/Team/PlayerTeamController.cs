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
    public class PlayerTeamController : BaseTeamController
    {
        BaseInput _input;
        Camera cam;

        public LayerMask floorLayerMask, unitSelectionLayerMask;

        public GameObject cubeHitPos;

        public GameObject playerPrefab;

        protected override void Start()
        {
            base.Start();
            _input = BaseInput.SelectInput(gameObject);
            cam = Camera.main;
            EventManager.StartListening(N.GameBalance.Updated, OnBalanceUpdated);
        }

        protected override void Init()
        {
            base.Init();

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

            if (_input.WasClicked() && CheckRay())
            {

            }
        }

        bool CheckRay()
        {
            Ray ray = cam.ScreenPointToRay(_input.pos);
            //Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000, floorLayerMask))
            {
                var hitPosition = hit.point;
                //_projector.SetActive(true);
                //_projector.enabled = true;
                //_projector.transform.position = hitPosition + Vector3.up * 0.1f;

                //hitPosition += Vector3.up * offsetExplosionY;
                //cubeHitPos.transform.position = hitPosition;
                return true;
            }
            else
            {
                //_projector.SetActive(false);
                //_projector.enabled = false;
            }
            return false;
        }

        bool CheckUnitSelection()
        {
            Ray ray = cam.ScreenPointToRay(_input.pos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000, unitSelectionLayerMask))
            {
                //var hitPosition = hit.point;

                if (_input.WasClicked())
                {
                    EventManager.TriggerEvent(N.Player.SelectUnit, hit.collider.gameObject);
                    return true;
                }
            }
            return false;
        }


        void OnBalanceUpdated(object p_data)
        {

        }
    }
}