using System.Collections;
using System.Collections.Generic;
using Game.FSM;
using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/Flee")]
    public class FleeAction : Action
    {
        public override void Act(NPC_Character p_controller)
        {
            Flee(p_controller);
        }

        private void Flee(NPC_Character p_controller)
        {
            Vector3 direction = p_controller.transform.position - p_controller.GetTarget.transform.position;
            Vector3 point = p_controller.transform.position + (direction.normalized * p_controller.CharacterData.moveArea);
            Debug.LogFormat("#FSM# Unit {0} will run away from {1}", p_controller.name, p_controller.GetTarget.name);

            p_controller.Move(point);
        }
    }
}