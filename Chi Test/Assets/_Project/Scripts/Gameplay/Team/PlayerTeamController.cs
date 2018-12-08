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

        public LayerMask floorLayerMask, blockLayerMask;

        public GameObject cubeHitPos;



        protected override void Start()
        {
            base.Start();
            _input = BaseInput.SelectInput(gameObject);
            cam = Camera.main;
            EventManager.StartListening(N.GameBalance.Updated, OnBalanceUpdated);
        }


        void Update()
        {
            if (!isPlaying)
            {
                return;
            }

            if (isMyTurn && _input.WasClicked())
            {
                isMyTurn = false;
                EventManager.TriggerEvent(N.Game.TurnFinished, this);
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


        void OnBalanceUpdated(object p_data)
        {

        }
    }
}