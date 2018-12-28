using System.Collections;
using System.Collections.Generic;
using Game.FSM;
using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/Chase")]
    public class ChaseAction : Action
    {
        public override void Act(NPC_Character p_controller)
        {
            Chase(p_controller);
        }

        private void Chase(NPC_Character p_controller)
        {
            Debug.LogFormat("#FSM# Unit {0} will chase {1}", p_controller.name, p_controller.GetTarget.name);
            //todo: Analyse Map to get better approach position
            p_controller.Move(p_controller.GetTarget.transform.position);
        }
    }
}