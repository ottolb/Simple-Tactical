using System.Collections;
using System.Collections.Generic;
using Game.GameInput;
using UnityEngine;
using Game.Event;
using DG.Tweening;

namespace Game.Gameplay
{

    public class BaseBlock : MonoBehaviour
    {

        public GameObject dieParticle, splashParticle;
        Rigidbody _rigidbody;
        bool dead;

        public float minSpeed;
        ParticleSystem.EmissionModule frictionP;

        Vector3 initScale;


        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            frictionP = transform.Find("Block Friction").GetComponent<ParticleSystem>().emission;
            frictionP.enabled = false;
            initScale = transform.localScale;
        }

        void Start()
        {

        }

        private void OnEnable()
        {
            EventManager.TriggerEvent(N.Block.Created, this);
            dead = false;
            _rigidbody.detectCollisions = true;
        }

        private void OnDisable()
        {
            EventManager.TriggerEvent(N.Block.Disabled, this);
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            transform.rotation = Quaternion.identity;
            transform.localScale = initScale;
        }

        private void Update()
        {
            frictionP.enabled = _rigidbody.velocity.magnitude > minSpeed;
        }

        void OnTriggerEnter(Collider p_collider)
        {
            string otherTag = p_collider.tag;

            if (otherTag.Equals("Powerup"))
                return;

            if (!dead)
            {
                if (otherTag.Equals("DeathZone"))
                {
                    EventManager.TriggerEvent(N.Block.Captured, this);
                }
                else
                {
                    EventManager.TriggerEvent(N.Block.Died, this);
                    //Create particle
                    GameObject go = CFX_SpawnSystem.GetNextObject(dieParticle);
                    go.transform.position = transform.position;
                    _rigidbody.velocity = Vector3.zero;
                    _rigidbody.detectCollisions = false;
                    ScaleFX();
                    DisableAfterTime(1);
                }

                dead = true;
            }
            else
            {
                //Create particle
                GameObject go = CFX_SpawnSystem.GetNextObject(splashParticle);
                go.transform.position = transform.position;
                DisableAfterTime(3);
            }
        }

        void DisableAfterTime(float p_duration)
        {
            this.WaitForSecondsAndDo(p_duration, delegate
            {
                gameObject.SetActive(false);
            });
        }

        void ScaleFX()
        {
            transform.DOScale(Vector3.zero, 0.2f).OnComplete(delegate
            {
                gameObject.SetActive(false);
            });
        }

        public void CreatedOnMatch(float p_delay)
        {
            transform.localScale = Vector3.zero;
            gameObject.layer = LayerMask.NameToLayer("Default");
            _rigidbody.useGravity = false;
            transform.DOScale(initScale, 0.3f).SetDelay(p_delay);


            this.WaitForSecondsAndDo(p_delay, delegate
            {
                gameObject.layer = LayerMask.NameToLayer("Block");
                _rigidbody.useGravity = true;
            });
        }
    }
}