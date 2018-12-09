using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay
{
    public class BaseCharacter : MonoBehaviour, ICharacter
    {
        public float life;
        public float speed;
        public bool canMove;
        public bool canAttack;
        protected NavMeshAgent _navMeshAgent;

        protected virtual void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        // Start is called before the first frame update
        protected virtual void Start()
        {

        }

        // Update is called once per frame
        protected virtual void Update()
        {

        }

        public virtual void Die()
        {

        }

        public virtual void Init()
        {

        }

        public virtual void Move()
        {

        }

        public virtual void TakeDamage(float p_amount)
        {

        }
    }
}