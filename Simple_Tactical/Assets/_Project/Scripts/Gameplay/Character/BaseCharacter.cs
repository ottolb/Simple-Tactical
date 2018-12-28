using System.Collections;
using System.Collections.Generic;
using Game.Data;
using Game.Event;
using Game.UI;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay
{
    /// <summary>
    /// Base character behavior and fields
    /// </summary>
    public class BaseCharacter : MonoBehaviour, ICharacter
    {
        private int _availableActios;

        protected int currentLife;

        ///state properties
        public bool isWaiting;
        protected bool isDead;
        /// <summary>
        /// Is agent moving?
        /// </summary>
        protected bool isMoving;
        protected bool myTurn;


        protected NavMeshAgent _navMeshAgent;

        protected CharacterMesh _mesh;
        protected LifeBarWidget _lifeBarWidget;
        protected CharacterData _characterData;

        protected virtual void Awake()
        {
            GetReferences();
        }

        void GetReferences()
        {
            if (_navMeshAgent != null) return;
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _mesh = GetComponentInChildren<CharacterMesh>();
            _lifeBarWidget = GetComponentInChildren<LifeBarWidget>();
            name = name.Replace("(Clone)", Random.Range(0, 1000).ToString());

            _navMeshAgent.updateRotation = false;
            _mesh.SetNavMeshAgent(_navMeshAgent);
        }

        protected virtual void Start()
        {
            EventManager.StartListening(N.Unit.HoverUnit, OnHoverUnit);
        }

        /// <summary>
        /// Init character graphics and fields from a data model
        /// </summary>
        /// <param name="p_characterData">P character data.</param>
        public virtual void Init(CharacterData p_characterData)
        {
            GetReferences();
            //setup data
            _characterData = p_characterData;
            CurrentLife = _characterData.life;

            //Request graphical setup based on Type
            if (_characterData.playerType != PlayerCharacterType.None)
                _mesh.SetupCharacter((int)_characterData.playerType, _characterData.attackAnimationType);
            else if (_characterData.enemyType != EnemyCharacterType.None)
                _mesh.SetupCharacter((int)_characterData.enemyType, _characterData.attackAnimationType);
        }

        protected virtual void Update()
        {
            if (isDead)
                return;
            SetupAgentLocomotion();
        }

        public virtual void Die()
        {
            _mesh.Die();
            isDead = true;
            //Deactive components and GOs
            transform.Find("Select Collider").gameObject.SetActive(false);
            _lifeBarWidget.gameObject.SetActive(false);
            _navMeshAgent.enabled = false;
            //Send event to let team controller handle his death
            EventManager.TriggerEvent(N.Unit.Died, this);
        }

        /// <summary>
        /// Called on each turn by TeamController
        /// </summary>
        public virtual void StartTurn()
        {
            if (isDead)
                return;
            //reset actions
            AvailableActions = _characterData.totalActions;
            isWaiting = false;
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

        /// <summary>
        /// AnimatorController call
        /// </summary>
        private void OnAnimatorMove()
        {
            //avoid moving player when attacking
            if (_mesh.locomotion.IsAttacking())
            {
                return;
            }

            //moves character based on animation root delta
            _navMeshAgent.velocity = _mesh.Animator.deltaPosition / Time.deltaTime;
            //rotate character based on animation
            transform.rotation = _mesh.Animator.rootRotation;
        }

        /// <summary>
        /// Handle mecanim animation control
        /// </summary>
        protected void SetupAgentLocomotion()
        {
            if (AgentDone())
            {
                //Idle animation
                _mesh.locomotion.Do(0, 0);
            }
            else
            {
                //retrieve speed from NavMeshAgent component
                float agentSpeed = _navMeshAgent.desiredVelocity.magnitude;
                //Calculate angle
                Vector3 velocity = Quaternion.Inverse(transform.rotation) * _navMeshAgent.desiredVelocity;
                float angle = Mathf.Atan2(velocity.x, velocity.z) * 180.0f / 3.14159f;
                //Set speed and angle to locomotion component
                _mesh.locomotion.Do(agentSpeed, angle);
            }
        }

        /// <summary>
        /// Set NavMeshAgent destination and decrease action
        /// </summary>
        /// <param name="p_point">P point.</param>
        public virtual void Move(Vector3 p_point)
        {
            _navMeshAgent.SetDestination(p_point);
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

            return distance < p_target._characterData.attackRange;
        }

        public virtual void Attack(BaseCharacter p_target)
        {
            transform.LookAt(p_target.transform);
            AvailableActions--;
            _mesh.Attack();
            p_target.TakeDamage(_characterData.attackForce);
            this.WaitForSecondsAndDo(0.5f, NotifyAction);
        }

        /// <summary>
        /// Receive an attack and handle death
        /// </summary>
        /// <param name="p_amount">P amount.</param>
        public virtual void TakeDamage(int p_amount)
        {
            CurrentLife -= p_amount;
            this.WaitForSecondsAndDo(0.4f, delegate
            {
                _mesh.ShowDamageFX(string.Format("-{0}", p_amount));
            });
            if (CurrentLife <= 0)
            {
                this.WaitForSecondsAndDo(0.4f, Die);
            }
            else
            {
                //todo: Could use an animation event
                this.WaitForSecondsAndDo(0.4f, _mesh.TakeHit);
            }
        }

        public void NotifyAction()
        {
            EventManager.TriggerEvent(N.Unit.ActionTaken, this);
            if (_availableActios == 0)
            {
                myTurn = false;
                EventManager.TriggerEvent(N.Unit.EndTurn, this);
            }
        }

        /// <summary>
        /// Is desired destination inside character's move area
        /// </summary>
        /// <returns><c>true</c>, if move energy was hased, <c>false</c> otherwise.</returns>
        /// <param name="p_target">P target.</param>
        protected bool HasMoveEnergy(Vector3 p_target)
        {
            return Vector3.Distance(transform.position, p_target) > _characterData.moveArea;
        }

        /// <summary>
        /// Dynamic character life, for total life use <see cref="CharacterData.life"/>
        /// </summary>
        /// <value>The current life.</value>
        public int CurrentLife
        {
            set
            {
                currentLife = value;
                _lifeBarWidget.UpdateLife(currentLife, _characterData.life);
            }
            get
            {
                return currentLife;
            }
        }

        /// <summary>
        /// How many action character has
        /// </summary>
        /// <value>The available actions.</value>
        public int AvailableActions
        {
            set
            {
                _availableActios = value;
            }
            get
            {
                return _availableActios;
            }
        }

        public bool AgentDone()
        {
            return !_navMeshAgent.pathPending && AgentStopping();
        }

        public bool AgentStopping()
        {
            return _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance;
        }

        public NavMeshAgent NavMeshAgent
        {
            get
            {
                return _navMeshAgent;
            }
        }

        public CharacterData CharacterData
        {
            get
            {
                return _characterData;
            }
        }
    }
}