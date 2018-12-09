using System.Collections;
using System.Collections.Generic;
using Game.Event;
using UnityEngine;

namespace Game.Gameplay
{
    public class PlayerCharacter : BaseCharacter
    {
        private GameObject selectParticle;
        private GameObject moveAreaParticle;

        protected override void Awake()
        {
            base.Awake();
            selectParticle = transform.Find("Mesh/Selection").gameObject;
            moveAreaParticle = transform.Find("Mesh/Max Move Area").gameObject;

            EventManager.StartListening(N.Player.HoverUnit, OnHoverUnit);
            EventManager.StartListening(N.Player.SelectUnit, OnUnitSelected);

            EventManager.StartListening(N.Player.MoveUnit, OnMoveUnit);
            EventManager.StartListening(N.Player.CheckUnitMovement, OnCheckUnitMovement);
        }

        public override void Init()
        {
            base.Init();
            selectParticle.SetActive(false);
            moveAreaParticle.SetActive(false);
        }

        void OnUnitSelected(object p_data)
        {
            if (p_data as GameObject == gameObject)
            {
                selectParticle.SetActive(true);
            }
            else
                selectParticle.SetActive(false);

            if (canMove)
            {
                moveAreaParticle.SetActive(true);
            }
        }

        void OnHoverUnit(object p_data)
        {
            if (p_data as GameObject == gameObject)
            {
                Debug.Log("Color ");
            }
            else
            {
                //Debug.Log("Color normal");
            }
        }

        void OnMoveUnit(object p_data)
        {
            Vector3 position = (Vector3)p_data;
            _navMeshAgent.SetDestination(position);
        }

        void OnCheckUnitMovement(object p_data)
        {

        }
    }
}