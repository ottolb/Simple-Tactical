using Game.Data;
using UnityEngine;

namespace Game.Gameplay
{
    /// <summary>
    /// Character data model
    /// </summary>
    public class CharacterData : ScriptableObject
    {
        public PlayerCharacterType playerType;
        public EnemyCharacterType enemyType;

        [Tooltip("How many actions this character will receive each turn"), Range(1, 4)]
        public int totalActions;

        [Tooltip("Character life"), Range(1, 300)]
        public int life;

        [Tooltip("How far the character can go on each action"), Range(1, 8)]
        public float moveArea;

        [Tooltip("Attack"), Range(1, 100)]
        public int attackForce;
        [Tooltip("Attack distance"), Range(0.5f, 8)]
        public float attackRange;

        [Tooltip("Type of animation"), Range(1, 5)]
        public int attackAnimationType;

        public FSM.State mentality;
    }
}