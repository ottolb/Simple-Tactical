using System.Collections;
using System.Collections.Generic;
using Game.Event;
using UnityEngine;
using DG.Tweening;


namespace Game.Gameplay
{
    public class PlayerCharacter : BaseCharacter
    {
        private GameObject selectParticle;
        private GameObject moveAreaParticle;

        public GameObject moveIndicator;
        private Material moveIndicatorMtl;

        public Color movementAllowedColor, movementBlockedColor;

        bool isSelected;
        bool hasMoveEnergy;

        protected override void Awake()
        {
            base.Awake();
            selectParticle = transform.Find("Mesh/Selection").gameObject;
            moveAreaParticle = transform.Find("Mesh/Max Move Area").gameObject;

            moveIndicatorMtl = moveIndicator.GetComponent<Renderer>().material;

            EventManager.StartListening(N.Player.HoverUnit, OnHoverUnit);
            EventManager.StartListening(N.Player.SelectUnit, OnUnitSelected);

            EventManager.StartListening(N.Player.MoveUnit, OnMoveUnit);
            EventManager.StartListening(N.Player.CheckUnitMovement, OnCheckUnitMovement);

            EventUIManager.StartListening(NUI.HUD.WaitAction, OnWaitAction);
        }

        public override void Init()
        {
            base.Init();
            selectParticle.SetActive(false);
            moveAreaParticle.SetActive(false);
            moveIndicator.SetActive(false);

            isSelected = false;
            canMove = true;

        }

        void OnUnitSelected(object p_data)
        {
            if (p_data as Transform == transform)
            {
                Debug.LogFormat("#Character# Character {0} is selected", name);

                selectParticle.SetActive(true);
                isSelected = true;
                EventUIManager.TriggerEvent(NUI.HUD.SetActionButton, canMove || canAttack);
            }
            else
            {
                isSelected = false;
                selectParticle.SetActive(false);
            }

            moveAreaParticle.SetActive(canMove && isSelected);
            moveIndicator.SetActive(canMove && isSelected);

        }

        void OnHoverUnit(object p_data)
        {
            if (p_data as Transform == transform)
            {
                //Debug.Log("Color ");
            }
            else
            {
                //Debug.Log("Color normal");
            }
        }

        void OnMoveUnit(object p_data)
        {
            if (isSelected && canMove)
            {
                if (hasMoveEnergy)
                {
                    Vector3 position = (Vector3)p_data;
                    _navMeshAgent.SetDestination(position);
                    canMove = false;
                    moveAreaParticle.SetActive(false);
                    moveIndicator.SetActive(false);

                    Debug.LogFormat("#Character# Character {0} moved ", name);
                }
                else
                {
                    Debug.LogFormat("#Character# Character {0} is too far away from destination point ", name);
                }
            }
        }

        void OnCheckUnitMovement(object p_data)
        {
            if (!isSelected || !canMove)
                return;

            Vector3 position = (Vector3)p_data;
            moveIndicator.transform.position = position;
            bool aux = HasMoveEnergy(position);
            if (aux != hasMoveEnergy)
                moveIndicatorMtl.DOColor(hasMoveEnergy ? movementAllowedColor : movementBlockedColor, 0.2f);
            hasMoveEnergy = !aux;
        }

        void OnWaitAction(object p_data)
        {
            if (!isSelected)
                return;

            canAttack = false;
            Debug.LogFormat("#Character# Character {0} won't attack ", name);

            //todo: Team controller should handle that
            EventUIManager.TriggerEvent(NUI.HUD.SetActionButton, canMove || canAttack);
        }

    }
}