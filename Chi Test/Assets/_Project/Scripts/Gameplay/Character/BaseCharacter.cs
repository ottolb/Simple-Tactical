using System.Collections;
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
        protected bool dead;

        protected NavMeshAgent _navMeshAgent;
        protected CharacterMesh _mesh;
        protected LifeBarWidget _lifeBarWidget;

        protected virtual void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _mesh = GetComponentInChildren<CharacterMesh>();
            _lifeBarWidget = GetComponentInChildren<LifeBarWidget>();
            name = name.Replace("(Clone)", Random.Range(0, 1000).ToString());

            _navMeshAgent.updateRotation = false;
            _mesh.SetNavMeshAgent(_navMeshAgent);
        }

        // Start is called before the first frame update
        protected virtual void Start()
        {
            EventManager.StartListening(N.Unit.HoverUnit, OnHoverUnit);
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            SetupAgentLocomotion();
            //transform.rotation = _mesh.GetRotation();
        }

        public virtual void Die()
        {
            _mesh.Die();
            dead = true;
            transform.Find("Select Collider").gameObject.SetActive(false);
            _lifeBarWidget.gameObject.SetActive(false);

            EventManager.TriggerEvent(N.Unit.Died, this);
        }

        public virtual void Init()
        {
            CurrentLife = life;
            Setup(Random.Range(0, 2));
        }

        protected void Setup(int p_charType)
        {
            _mesh.SetupCharacter(p_charType);
        }

        public virtual void StartTurn()
        {
            if (dead)
                return;
            AvailableActions = totalActions;
            isWaiting = false;
            canAttack = canMove = true;
            _mesh.StopOutline();
        }

        protected virtual void OnHoverUnit(object p_data)
        {
            if (p_data as Transform == transform)
            {
                _mesh.Hover();
            }
            else
            {
                _mesh.StopOutline();
            }
        }

        private void OnAnimatorMove()
        {
            if (_mesh.locomotion.IsState("Attacks.Sword Attack") ||
               _mesh.locomotion.IsState("Attacks.Magic Attack"))
            {
                return;
            }

            _navMeshAgent.velocity = _mesh.Animator.deltaPosition / Time.deltaTime;
            transform.rotation = _mesh.Animator.rootRotation;
        }

        protected void SetupAgentLocomotion()
        {
            if (AgentDone())
            {
                _mesh.locomotion.Do(0, 0);
            }
            else
            {
                float s = _navMeshAgent.desiredVelocity.magnitude;

                Vector3 velocity = Quaternion.Inverse(transform.rotation) * _navMeshAgent.desiredVelocity;
                float angle = Mathf.Atan2(velocity.x, velocity.z) * 180.0f / 3.14159f;
                _mesh.locomotion.Do(s, angle);
            }
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
            transform.LookAt(p_target.transform);
            canAttack = false;
            AvailableActions--;
            _mesh.Attack();
            p_target.TakeDamage(attackForce);
        }

        public virtual void TakeDamage(int p_amount)
        {
            CurrentLife -= p_amount;
            if (CurrentLife <= 0)
            {
                this.WaitForSecondsAndDo(0.4f, Die);
            }
            else
            {
                this.WaitForSecondsAndDo(0.4f, _mesh.TakeHit);
                //_mesh.TakeHit();
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
            set
            {
                _availableActios = value;
                EventManager.TriggerEvent(N.Unit.ActionTaken, this);
            }
            get
            {
                return _availableActios;
            }
        }

        protected bool AgentDone()
        {
            return !_navMeshAgent.pathPending && AgentStopping();
        }

        protected bool AgentStopping()
        {
            return _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance;
        }
    }
}