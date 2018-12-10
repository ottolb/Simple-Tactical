using UnityEngine;

namespace Game.Gameplay
{
    public interface ICharacter
    {

        void Init();

        void Move(Vector3 p_point);

        void Die();

        void Attack(float p_power);

        void TakeDamage(float p_amount);

    }
}