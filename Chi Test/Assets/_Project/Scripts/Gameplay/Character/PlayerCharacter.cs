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

            EventManager.StartListening(N.Unit.SelectUnit, OnUnitSelected);

            EventManager.StartListening(N.Unit.MoveUnit, OnMoveUnit);
            EventManager.StartListening(N.Unit.CheckUnitMovement, OnCheckUnitMovement);
        }

        public override void Init()
        {
            base.Init();
        }

        public override void StartTurn()
        {
            base.StartTurn();
            selectParticle.SetActive(false);
            moveAreaParticle.SetActive(false);
            moveIndicator.SetActive(false);

            isSelected = false;
        }

        void OnUnitSelected(object p_data)
        {
            if (p_data as Transform == transform)
            {
                Debug.LogFormat("#Character# Character {0} is selected", name);

                selectParticle.SetActive(true);
                isSelected = true;

                UpdateActions();
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

        void OnMoveUnit(object p_data)
        {
            if (isSelected && canMove)
            {
                if (hasMoveEnergy)
                {
                    Vector3 position = (Vector3)p_data;
                    Move(position);
                }
                else
                {
                    Debug.LogFormat("#Character# Character {0} is too far away from destination point ", name);
                }
            }
        }

        public override void Move(Vector3 p_point)
        {
            base.Move(p_point);
            moveAreaParticle.SetActive(false);
            moveIndicator.SetActive(false);

            UpdateActions();
            Debug.LogFormat("#Character# Character {0} moved ", name);
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

        public override void Attack(BaseCharacter p_target)
        {
            base.Attack(p_target);
            UpdateActions();
        }

        private void UpdateActions()
        {
            Dictionary<string, int> dict = new Dictionary<string, int>
            {
                ["total"] = totalActions,
                ["current"] = AvailableActions
            };
            EventUIManager.TriggerEvent(NUI.HUD.SetAvailableActions, dict);
        }
    }
}