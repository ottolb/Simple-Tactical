using System.Collections;
using System.Collections.Generic;
using Game.FSM;
using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/Find Strongest Ally")]
    public class FindStrongestAllyAction : Action
    {
        public override void Act(NPC_Character p_controller)
        {
            FindStrongestAlly(p_controller);
        }

        private void FindStrongestAlly(NPC_Character p_controller)
        {
            int life = 0;
            BaseCharacter strongestPlayerUnit = null;
            p_controller.SetTarget(null);
            foreach (var unit in p_controller.Allies)
            {
                if (unit.CurrentLife > life && unit != p_controller)
                {
                    life = unit.CurrentLife;
                    strongestPlayerUnit = unit;
                }
            }

            if (strongestPlayerUnit)
            {
                p_controller.SetTarget(strongestPlayerUnit);
            }
        }
    }
}