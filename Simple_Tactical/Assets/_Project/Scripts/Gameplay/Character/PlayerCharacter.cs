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
        LineRenderer lineRenderer;

        public Color movementAllowedColor, movementBlockedColor;
        public Gradient gradAllowedColor, gradBlockedColor;

        bool isSelected;
        bool hasMoveEnergy;
        bool drawPath;

        protected override void Awake()
        {
            base.Awake();
            selectParticle = transform.Find("Mesh/Selection").gameObject;
            moveAreaParticle = transform.Find("Mesh/Max Move Area").gameObject;
            lineRenderer = transform.Find("Mesh/Path Line Rendeder").GetComponent<LineRenderer>();
            lineRenderer.useWorldSpace = true;

            moveIndicatorMtl = moveIndicator.GetComponent<Renderer>().material;

            EventManager.StartListening(N.Unit.SelectUnit, OnUnitSelected);

            EventManager.StartListening(N.Unit.MoveUnit, OnMoveUnit);
            EventManager.StartListening(N.Unit.CheckUnitMovement, OnCheckUnitMovement);
        }

        public override void StartTurn()
        {
            base.StartTurn();
            selectParticle.SetActive(false);
            lineRenderer.enabled = false;
            moveAreaParticle.SetActive(false);
            moveIndicator.SetActive(false);
            _navMeshAgent.isStopped = true;
            isSelected = false;
        }

        protected override void Update()
        {
            base.Update();
            if (isMoving)
                HandleMovement();
        }

        void OnUnitSelected(object p_data)
        {
            if (p_data as Transform == transform)
            {
                Debug.LogFormat("#Character# Character {0} is selected", name);

                selectParticle.SetActive(true);
                isSelected = true;

                UpdateActions();
                EventUIManager.TriggerEvent(NUI.HUD.SetActionButton, AvailableActions > 0);
            }
            else
            {
                isSelected = false;
                selectParticle.SetActive(false);
            }

            bool _canMove = AvailableActions > 0;
            moveAreaParticle.SetActive(_canMove && isSelected);
            moveIndicator.SetActive(_canMove && isSelected);
            lineRenderer.enabled = _canMove && isSelected;

        }

        void OnMoveUnit(object p_data)
        {
            if (isSelected && AvailableActions > 0)
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
            _navMeshAgent.isStopped = false;
            base.Move(p_point);

            isMoving = true;
            UpdateActions();
            Debug.LogFormat("#Character# Character {0} moved ", name);
        }

        void HandleMovement()
        {
            if (AgentDone())
            {
                Debug.LogFormat("#Character# Character {0} done movement: ", name);
                isMoving = false;
                NotifyAction();
                _navMeshAgent.isStopped = true;
            }
        }

        void OnCheckUnitMovement(object p_data)
        {
            if (!isSelected || AvailableActions <= 0)
                return;

            Vector3 position = (Vector3)p_data;
            moveIndicator.transform.position = position;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, position);
            bool aux = HasMoveEnergy(position);
            if (aux != hasMoveEnergy)
            {
                moveIndicatorMtl.DOColor(hasMoveEnergy ? movementAllowedColor : movementBlockedColor, 0.2f);
                lineRenderer.colorGradient = hasMoveEnergy ? gradAllowedColor : gradBlockedColor;
            }
            hasMoveEnergy = !aux;
            lineRenderer.enabled = drawPath;
            moveIndicator.SetActive(drawPath);
        }

        protected override void OnHoverUnit(object p_data)
        {
            base.OnHoverUnit(p_data);
            drawPath = p_data == null;
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
                ["total"] = _characterData.totalActions,
                ["current"] = AvailableActions
            };
            EventUIManager.TriggerEvent(NUI.HUD.SetAvailableActions, dict);


            bool _canMove = AvailableActions > 0;
            moveAreaParticle.SetActive(_canMove);
            moveIndicator.SetActive(_canMove);
            lineRenderer.enabled = _canMove;
        }
    }
}