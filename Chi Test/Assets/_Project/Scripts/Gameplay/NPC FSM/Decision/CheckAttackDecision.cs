using System.Collections;
using System.Collections.Generic;
using Game.FSM;
using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/Check Attack")]
    public class CheckAttackDecision : Decision
    {

        public override bool Decide(NPC_Character p_controller)
        {
            bool shouldAttack = CheckAttack(p_controller);
            Debug.LogFormat("#FSM# Unit {0} decided to {1}", p_controller.name, shouldAttack ? "attack" : "not attack");
            return shouldAttack;
        }

        private bool CheckAttack(NPC_Character p_controller)
        {
            return p_controller.HasTarget && p_controller.AvailableActions > 0 && p_controller.CheckAttack(p_controller.GetTarget);
        }
    }
}