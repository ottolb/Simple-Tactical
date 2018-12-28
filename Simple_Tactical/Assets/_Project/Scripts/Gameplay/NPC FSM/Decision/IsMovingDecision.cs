using System.Collections;
using System.Collections.Generic;
using Game.FSM;
using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/Is Moving Decision")]
    public class IsMovingDecision : Decision
    {

        public override bool Decide(NPC_Character p_controller)
        {
            bool isMoving = HasCompletedPath(p_controller);
            Debug.LogFormat("#FSM# Unit {0} is {1}", p_controller.name, isMoving ? "moving" : "not moving");
            return isMoving;
        }

        private bool HasCompletedPath(NPC_Character p_controller)
        {
            if (p_controller.totalDistance < 0)
            {
                p_controller.totalDistance = p_controller.NavMeshAgent.remainingDistance;
                //Debug.LogFormat("#NPC# NPC {0} total distance set: {1}", name, p_controller.totalDistance);
                return false;
            }

            //Debug.LogFormat("#NPC# NPC {0} remainingDistance: {1}", name, p_controller.NavMeshAgent.remainingDistance);
            if (p_controller.AgentDone()
                || (p_controller.NavMeshAgent.remainingDistance < p_controller.totalDistance - p_controller.CharacterData.moveArea))
            {
                Debug.LogFormat("#NPC# NPC {0} stopped: ", name);
                p_controller.NavMeshAgent.isStopped = true;
                p_controller.NotifyAction();
                return true;
            }

            return false;

        }
    }
}