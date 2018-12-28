using System.Collections;
using System.Collections.Generic;
using Game.FSM;
using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/Chase")]
    public class ChaseDecision : Decision
    {

        public override bool Decide(NPC_Character p_controller)
        {
            bool shouldChase = Chase(p_controller);
            Debug.LogFormat("#FSM# Unit {0} decided to {1}", p_controller.name, shouldChase ? "chase" : "not chase");
            return shouldChase;
        }

        private bool Chase(NPC_Character p_controller)
        {
            //todo: check if other unit is weak
            //todo: check if other unit has big attack point
            return p_controller.HasTarget && p_controller.AvailableActions > 0;
        }
    }
}