using System.Collections;
using System.Collections.Generic;
using Game.Event;
using UnityEngine;
using DG.Tweening;


namespace Game.Gameplay
{
    public class NPC_Character : BaseCharacter
    {
        private float totalDistance;
        private BaseCharacter _target;
        private bool isMoving;



        public override void Init(CharacterData p_characterData)
        {
            base.Init(p_characterData);
            isMoving = false;
        }

        public override void StartTurn()
        {
            base.StartTurn();
            totalDistance = -1;
            isMoving = false;
        }

        public override void Move(Vector3 p_point)
        {
            if (CheckAttack(_target))
            {
                Debug.LogFormat("#NPC# NPC {0} will attack now!", name);
                Attack(_target);
                AvailableActions = 0;
            }
            else
            {
                _navMeshAgent.isStopped = false;
                isMoving = true;
                base.Move(p_point);
            }
        }

        protected override void Update()
        {
            base.Update();

            if (dead)
                return;

            if (isMoving)
                HandleMovement();
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


            }
            else
            {

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
            if (CheckAttack(p_target))
            {
                base.Attack(p_target);
            }
            else
            {
                Debug.LogFormat("#Character# Character {0} is far to attack {1}", name, p_target.name);
                AvailableActions--;
            }
        }
    }
}