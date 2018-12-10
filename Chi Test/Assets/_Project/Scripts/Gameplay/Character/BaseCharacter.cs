﻿using System.Collections;
using System.Collections.Generic;
using Game.Event;
using Game.UI;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay
{
    public class BaseCharacter : MonoBehaviour, ICharacter
    {
        public int totalActions;
        private int _availableActios;

        public int life;
        protected int currentLife;
        public float speed;
        public float moveArea;

        public int attackForce;
        public float attackRange;

        ///state properties
        public bool canMove;
        public bool canAttack;
        public bool isWaiting;

        protected NavMeshAgent _navMeshAgent;
        protected CharacterMesh _mesh;
        protected LifeBarWidget _lifeBarWidget;

        protected virtual void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _mesh = GetComponentInChildren<CharacterMesh>();
            _lifeBarWidget = GetComponentInChildren<LifeBarWidget>();
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
            CurrentLife = life;
        }

        public virtual void StartTurn()
        {
            AvailableActions = totalActions;
            isWaiting = false;
            canMove = true;
            _mesh.StopOutline();
        }


        public virtual void Move(Vector3 p_point)
        {
            _navMeshAgent.SetDestination(p_point);
            canMove = false;
            AvailableActions--;
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
            AvailableActions--;
            _mesh.Attack();
            p_target.TakeDamage(attackForce);
        }

        public virtual void TakeDamage(int p_amount)
        {
            CurrentLife -= p_amount;
            if (CurrentLife <= 0)
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

        protected int CurrentLife
        {
            set
            {
                currentLife = value;
                _lifeBarWidget.UpdateLife(currentLife, life);
            }
            get
            {
                return currentLife;
            }
        }

        public int AvailableActions
        {
            private set
            {
                _availableActios = value;
                EventManager.TriggerEvent(N.Unit.ActionTaken, this);
            }
            get
            {
                return _availableActios;
            }
        }
    }
}