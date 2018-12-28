using System.Collections;
using System.Collections.Generic;
using Game.FSM;
using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/End Turn Decision")]
    public class EndTurnDecision : Decision
    {

        public override bool Decide(NPC_Character p_controller)
        {
            bool hasActions = CheckEndTurn(p_controller);
            Debug.LogFormat("#FSM# Unit {0} decided to {1}", p_controller.name, hasActions ? "end turn" : "continue turn");
            return hasActions;
        }

        private bool CheckEndTurn(NPC_Character p_controller)
        {
            return p_controller.AvailableActions <= 0;
        }
    }
}