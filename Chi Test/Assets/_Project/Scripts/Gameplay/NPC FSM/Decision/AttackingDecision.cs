using System.Collections;
using System.Collections.Generic;
using Game.FSM;
using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/Attacking Decision")]
    public class AttackingDecision : Decision
    {
        public float attackDuration;

        public override bool Decide(NPC_Character p_controller)
        {
            bool isWaiting = WaitingAttack(p_controller);
            return isWaiting;
        }

        private bool WaitingAttack(NPC_Character p_controller)
        {
            if (p_controller.CheckIfCountDownElapsed(attackDuration))
                return false;

            return true;
        }
    }
}