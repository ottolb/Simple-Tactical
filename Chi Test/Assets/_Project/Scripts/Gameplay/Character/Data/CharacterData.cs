using UnityEngine;

namespace Game.Gameplay
{
    public class CharacterData : ScriptableObject
    {
        public int totalActions;
        private int _availableActios;

        public int life;
        protected int currentLife;
        public float speed;
        public float moveArea;

        public int attackForce;
        public float attackRange;

    }
}