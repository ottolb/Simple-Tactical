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


        protected override void Awake()
        {
            base.Awake();

        }

        public override void Init()
        {
            base.Init();
            canMove = true;
            totalDistance = -1;
        }

        public override void Move(Vector3 p_point)
        {
            base.Move(p_point);
        }

        protected override void Update()
        {
            base.Update();
            if (canMove)
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


            if (_navMeshAgent.remainingDistance < totalDistance - moveArea)
            {
                Debug.LogFormat("#NPC# NPC {0} stopped: ", name);
                _navMeshAgent.isStopped = true;
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
    }
}