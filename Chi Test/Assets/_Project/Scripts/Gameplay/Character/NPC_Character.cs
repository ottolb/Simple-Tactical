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

        public override void Init()
        {
            base.Init();
            canMove = true;
        }

        public override void StartTurn()
        {
            base.StartTurn();
            totalDistance = -1;
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
                base.Move(p_point);
            }
        }

        protected override void Update()
        {
            base.Update();

            if (canMove || !canAttack || dead)
                return;

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
            if (AgentDone() || (_navMeshAgent.remainingDistance < totalDistance - moveArea))
            {
                Debug.LogFormat("#NPC# NPC {0} stopped: ", name);
                _navMeshAgent.isStopped = true;
                Attack(_target);
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
                //this.WaitForSecondsAndDo(0.4f, EndAttack);
            }
            else
            {
                Debug.LogFormat("#Character# Character {0} is far to attack {1}", name, p_target.name);
                AvailableActions--;
            }
        }

        void EndAttack()
        {
            //EventManager.TriggerEvent(N.Unit.Attacked, this);
        }
    }
}