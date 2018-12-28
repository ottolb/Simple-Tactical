using System.Collections;
using System.Collections.Generic;
using Game.Gameplay;
using UnityEngine;

namespace Game.FSM
{
    public abstract class Action : ScriptableObject
    {
        public abstract void Act(NPC_Character p_controller);
    }
}