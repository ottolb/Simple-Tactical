using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay
{
    public class BaseCharacter : MonoBehaviour, ICharacter
    {
        public int totalActions;
        public int availableActios;

        public float life;
        public float speed;
        public float moveArea;

        ///state properties
        public bool canMove;
        public bool canAttack;
        public bool isWaiting;

        protected NavMeshAgent _navMeshAgent;

        protected virtual void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            name = name.Replace("(Clone)", Random.Range(0, 1000).ToString());
        }

        // Start is called before the first frame update
        protected virtual void Start()
        {
            Init();
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
            availableActios = totalActions;
            isWaiting = false;
        }

        public virtual void Move(Vector3 p_point)
        {
            _navMeshAgent.SetDestination(p_point);
            canMove = false;
            availableActios--;
        }

        public virtual void Attack(float p_power)
        {
            availableActios--;
        }

        public virtual void TakeDamage(float p_amount)
        {

        }

        protected bool HasMoveEnergy(Vector3 p_target)
        {
            return Vector3.Distance(transform.position, p_target) > moveArea;
        }
    }
}