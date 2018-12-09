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
        NavMeshAgent _navMeshAgent;

        void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Die()
        {

        }

        public void Init()
        {

        }

        public void Move()
        {

        }

        public void TakeDamage(float p_amount)
        {

        }
    }
}