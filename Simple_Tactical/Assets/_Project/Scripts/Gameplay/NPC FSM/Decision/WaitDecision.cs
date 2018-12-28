using System.Collections;
using System.Collections.Generic;
using Game.FSM;
using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/Wait Decision")]
    public class WaitDecision : Decision
    {
        public float duration;

        public override bool Decide(NPC_Character p_controller)
        {
            bool isWaiting = Waiting(p_controller);
            return isWaiting;
        }

        private bool Waiting(NPC_Character p_controller)
        {
            if (p_controller.CheckIfCountDownElapsed(duration))
                return false;

            return true;
        }
    }
}