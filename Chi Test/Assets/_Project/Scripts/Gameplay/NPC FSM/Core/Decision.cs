using System.Collections;
using System.Collections.Generic;
using Game.Gameplay;
using UnityEngine;

namespace Game.FSM
{
    public abstract class Decision : ScriptableObject
    {
        public abstract bool Decide(NPC_Character p_controller);
    }
}