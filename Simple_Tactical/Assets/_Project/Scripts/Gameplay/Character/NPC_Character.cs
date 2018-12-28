using System.Collections;
using System.Collections.Generic;
using Game.Event;
using UnityEngine;
using DG.Tweening;
using Game.FSM;

namespace Game.Gameplay
{
    public class NPC_Character : BaseCharacter
    {
        [HideInInspector]
        public float totalDistance;
        /// <summary>
        /// The target to attack or to approach
        /// </summary>
        private BaseCharacter _target;

        public State startState;
        private State currentState;
        public State remainState;
        public Transform eyes;
        [HideInInspector] public float stateTimeElapsed;


        protected override void Awake()
        {
            base.Awake();
            EventManager.StartListening(N.Unit.SelectUnit, OnUnitSelected);
        }

        public override void Init(CharacterData p_characterData)
        {
            base.Init(p_characterData);
            isMoving = false;
            startState = p_characterData.mentality;
        }

        public override void StartTurn()
        {
            base.StartTurn();
            currentState = startState;
            totalDistance = -1;
            isMoving = false;
        }

        public override void Move(Vector3 p_point)
        {
            //if (CheckAttack(_target))
            //{
            //    Debug.LogFormat("#NPC# NPC {0} will attack now!", name);
            //    Attack(_target);
            //    AvailableActions = 0;
            //}
            //else
            {
                totalDistance = -1;
                _navMeshAgent.isStopped = false;
                isMoving = true;
                base.Move(p_point);
            }
        }

        protected override void Update()
        {
            base.Update();

            if (isDead || !myTurn)
                return;

            //if (isMoving)
            //HandleMovement();

            UpdateFSM();
        }

        void HandleMovement()
        {
            if (_navMeshAgent.isStopped)
            {
                Debug.LogFormat("#NPC# NPC {0} is Stopped", name);
                return;
            }

            if (_navMeshAgent.remainingDistance < 0.1f)
            {
                Debug.LogFormat("#NPC# NPC {0} remainingDistance: {1}", name, _navMeshAgent.remainingDistance);
                return;
            }
            else if (totalDistance == -1)
            {
                totalDistance = _navMeshAgent.remainingDistance;
                Debug.LogFormat("#NPC# NPC {0} total distance set: {1}", name, totalDistance);
            }

            Debug.LogFormat("#NPC# NPC {0} remainingDistance: {1}", name, _navMeshAgent.remainingDistance);
            //if (_navMeshAgent.remainingDistance < totalDistance - moveArea)
            if (AgentDone() || (_navMeshAgent.remainingDistance < totalDistance - _characterData.moveArea))
            {
                Debug.LogFormat("#NPC# NPC {0} stopped: ", name);
                _navMeshAgent.isStopped = true;
                Attack(_target);
                isMoving = false;
            }
        }

        void OnUnitSelected(object p_data)
        {
            if (p_data as Transform == transform)
            {
                StartFSM();
            }
        }

        void OnCheckUnitMovement(object p_data)
        {
            Vector3 position = (Vector3)p_data;
            bool aux = HasMoveEnergy(position);
        }

        public void SetTarget(BaseCharacter p_character)
        {
            _target = p_character;
        }

        public override void Attack(BaseCharacter p_target)
        {
            base.Attack(p_target);
        }

        public void StartFSM()
        {
            currentState.UpdateState(this);

            myTurn = true;
        }

        void UpdateFSM()
        {
            currentState.UpdateState(this);
        }

        void OnDrawGizmos()
        {
            if (currentState != null && _characterData)
            {
                Gizmos.color = currentState.sceneGizmoColor;
                Gizmos.DrawWireSphere(transform.position, _characterData.attackRange);
            }
        }

        public void TransitionToState(State nextState)
        {
            if (nextState != remainState)
            {
                //bool goToNextState = currentState.cost == 0;

                Debug.LogFormat("#FSM# Unit {0} switch from {1} to {2}",
                                name, currentState.name, nextState.name);
                currentState = nextState;
                OnExitState();
            }
        }

        public bool CheckIfCountDownElapsed(float duration)
        {
            stateTimeElapsed += Time.deltaTime;
            return (stateTimeElapsed >= duration);
        }

        private void OnExitState()
        {
            stateTimeElapsed = 0;
        }

        public void Stop()
        {
            myTurn = false;
            currentState = remainState;
        }

        public List<BaseCharacter> PlayerUnits { get; set; }

        public List<BaseCharacter> Allies { get; set; }

        public bool HasTarget
        {
            get { return _target != null; }
        }

        public BaseCharacter GetTarget
        {
            get { return _target; }
        }
    }
}