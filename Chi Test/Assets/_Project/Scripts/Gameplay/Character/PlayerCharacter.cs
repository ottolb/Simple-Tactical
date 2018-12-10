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

            EventManager.StartListening(N.Unit.HoverUnit, OnHoverUnit);
            EventManager.StartListening(N.Unit.SelectUnit, OnUnitSelected);

            EventManager.StartListening(N.Unit.MoveUnit, OnMoveUnit);
            EventManager.StartListening(N.Unit.CheckUnitMovement, OnCheckUnitMovement);
        }

        public override void Init()
        {
            base.Init();
            Setup(Random.Range(0, 1));
        }

        public override void StartTurn()
        {
            base.StartTurn();
            selectParticle.SetActive(false);
            moveAreaParticle.SetActive(false);
            moveIndicator.SetActive(false);

            isSelected = false;
        }

        public void Setup(int p_charType)
        {
            _mesh.SetupCharacter(p_charType);
        }

        void OnUnitSelected(object p_data)
        {
            if (p_data as Transform == transform)
            {
                Debug.LogFormat("#Character# Character {0} is selected", name);

                selectParticle.SetActive(true);
                isSelected = true;

                EventUIManager.TriggerEvent(NUI.HUD.SetActionButton, canMove || canAttack);

                Dictionary<string, int> dict = new Dictionary<string, int>();
                dict["total"] = totalActions;
                dict["current"] = AvailableActions;
                EventUIManager.TriggerEvent(NUI.HUD.SetAvailableActions, dict);
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
                _mesh.Hover();
            }
            else
            {
                _mesh.StopOutline();
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



    }
}