using System.Collections;
using System.Collections.Generic;
using Game.FSM;
using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/End Turn Action")]
    public class EndTurnAction : Action
    {
        public override void Act(NPC_Character p_controller)
        {
            EndTurn(p_controller);
        }

        private void EndTurn(NPC_Character p_controller)
        {
            p_controller.AvailableActions = 0;
            p_controller.NotifyAction();
        }
    }
}