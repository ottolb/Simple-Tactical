﻿using UnityEngine;

namespace Game.Gameplay
{
    public interface ICharacter
    {

        void Init(CharacterData p_characterData);

        void StartTurn();

        void Move(Vector3 p_point);

        void Die();

        void Attack(BaseCharacter p_target);

        void TakeDamage(int p_amount);

    }
}