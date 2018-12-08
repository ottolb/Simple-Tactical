namespace Game.Gameplay
{
    public interface ICharacter
    {

        void Init();

        void Move();

        void Die();

        void TakeDamage(float p_amount);

    }
}