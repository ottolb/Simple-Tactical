using System.Collections;
using System.Collections.Generic;
using Game.FSM;
using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/Attack")]
    public class AttackAction : Action
    {
        public override void Act(NPC_Character p_controller)
        {
            Attack(p_controller);
        }

        private void Attack(NPC_Character p_controller)
        {
            Debug.LogFormat("#FSM# Unit {0} will try to attack {1}", p_controller.name, p_controller.GetTarget.name);
            p_controller.Attack(p_controller.GetTarget);
        }
    }
}