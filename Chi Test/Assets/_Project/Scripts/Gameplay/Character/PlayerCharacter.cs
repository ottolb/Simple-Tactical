using System.Collections;
using System.Collections.Generic;
using Game.Event;
using UnityEngine;

namespace Game.Gameplay
{
    public class PlayerCharacter : BaseCharacter
    {

        protected override void Awake()
        {
            base.Awake();

            EventManager.StartListening(N.Player.SelectUnit, OnUnitSelected);
        }

        void OnUnitSelected(object p_data)
        {

        }
    }
}