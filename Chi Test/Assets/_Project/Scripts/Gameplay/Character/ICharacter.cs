using UnityEngine;

namespace Game.Gameplay
{
    public interface ICharacter
    {

        void Init();

        void StartTurn();

        void Move(Vector3 p_point);

        void Die();

        void Attack(BaseCharacter p_target);

        void TakeDamage(int p_amount);

    }
}