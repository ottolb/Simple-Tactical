using System.Collections;
using System.Collections.Generic;
using Game.Data;
using Game.Event;
using Game.UI;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay
{
    public class BaseCharacter : MonoBehaviour, ICharacter
    {
        private int _availableActios;

        protected int currentLife;

        ///state properties
        public bool isWaiting;
        protected bool dead;

        public GameObject damageFX;

        protected NavMeshAgent _navMeshAgent;
        protected CharacterMesh _mesh;
        protected LifeBarWidget _lifeBarWidget;

        protected CharacterData _characterData;

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

        public virtual void Init(CharacterData p_characterData)
        {
            _characterData = p_characterData;
            CurrentLife = _characterData.life;

            if (_characterData.playerType != PlayerCharacterType.None)
                _mesh.SetupCharacter((int)_characterData.playerType, _characterData.attackAnimationType);
            else if (_characterData.enemyType != EnemyCharacterType.None)
                _mesh.SetupCharacter((int)_characterData.enemyType, _characterData.attackAnimationType);
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            if (dead)
                return;
            SetupAgentLocomotion();
            //transform.rotation = _mesh.GetRotation();
        }

        public virtual void Die()
        {
            _mesh.Die();
            dead = true;
            transform.Find("Select Collider").gameObject.SetActive(false);
            _lifeBarWidget.gameObject.SetActive(false);
            _navMeshAgent.enabled = false;
            EventManager.TriggerEvent(N.Unit.Died, this);
        }

        public virtual void StartTurn()
        {
            if (dead)
                return;
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
        }

        public virtual void TakeDamage(int p_amount)
        {
            CurrentLife -= p_amount;
            ShowDamageFX(string.Format("-{0}", p_amount));
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
            return Vector3.Distance(transform.position, p_target) > _characterData.moveArea;
        }

        protected int CurrentLife
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

        void ShowDamageFX(string p_damage)
        {
            Vector3 offset = Vector3.up * 2;

            GameObject go = Instantiate(damageFX);
            go.transform.position = transform.position + offset;

            ScoreFX aux = go.GetComponent<ScoreFX>();
            aux.Show(p_damage, 2.2f);
        }
    }
}