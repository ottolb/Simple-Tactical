using System.Collections;
using System.Collections.Generic;
using Game.FSM;
using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/FindNearestTargetAction")]
    public class FindNearestTargetAction : Action
    {
        public override void Act(NPC_Character p_controller)
        {
            FindNearestTarget(p_controller);
        }

        private void FindNearestTarget(NPC_Character p_controller)
        {
            float distance = Mathf.Infinity;
            float auxD;
            BaseCharacter nearestPlayerUnit = null;
            foreach (var unit in p_controller.PlayerUnits)
            {
                auxD = Vector3.Distance(unit.transform.position, p_controller.transform.position);
                if (auxD < distance)
                {
                    distance = auxD;
                    nearestPlayerUnit = unit;
                }
            }

            if (nearestPlayerUnit)
            {
                p_controller.SetTarget(nearestPlayerUnit);
            }
        }
    }
}