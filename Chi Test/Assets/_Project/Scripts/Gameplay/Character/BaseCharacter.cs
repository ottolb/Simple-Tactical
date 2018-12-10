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
        protected float currentLife;
        public float speed;
        public float moveArea;

        public float attackForce;
        public float attackRange;

        ///state properties
        public bool canMove;
        public bool canAttack;
        public bool isWaiting;

        protected NavMeshAgent _navMeshAgent;
        protected CharacterMesh _mesh;


        protected virtual void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _mesh = GetComponentInChildren<CharacterMesh>();
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
            _mesh.StopOutline();
            currentLife = life;
        }

        public virtual void Move(Vector3 p_point)
        {
            _navMeshAgent.SetDestination(p_point);
            canMove = false;
            availableActios--;
        }

        /// <summary>
        /// Checks if unit is near to attack the target
        /// </summary>
        /// <returns><c>true</c>, if distance is less than Attack Range, <c>false</c> otherwise.</returns>
        /// <param name="p_target">P target.</param>
        public virtual bool CheckAttack(BaseCharacter p_target)
        {
            //Calc distance
            float distance = Vector3.Distance(p_target.transform.position, transform.position);

            return distance < p_target.attackRange;
        }

        public virtual void Attack(BaseCharacter p_target)
        {
            availableActios--;
            _mesh.Attack();
            p_target.TakeDamage(attackForce);
        }

        public virtual void TakeDamage(float p_amount)
        {
            currentLife -= p_amount;
            if (currentLife <= 0)
            {
                _mesh.Die();
            }
            else
            {
                _mesh.TakeHit();
            }
        }

        protected bool HasMoveEnergy(Vector3 p_target)
        {
            return Vector3.Distance(transform.position, p_target) > moveArea;
        }
    }
}