using System.Collections;
using System.Collections.Generic;
using Game.FSM;
using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/Flee Decision")]
    public class FleeDecision : Decision
    {
        public float fleeDistance;

        public override bool Decide(NPC_Character p_controller)
        {
            bool shouldChase = CheckFlee(p_controller);
            Debug.LogFormat("#FSM# Unit {0} decided to {1}", p_controller.name, shouldChase ? "flee" : "stay");
            return shouldChase;
        }

        private bool CheckFlee(NPC_Character p_controller)
        {
            if (!p_controller.HasTarget)
                return false;
            float distance = Vector3.Distance(p_controller.transform.position, p_controller.GetTarget.transform.position);
            return distance < fleeDistance && p_controller.HasTarget && p_controller.AvailableActions > 0;
        }
    }
}