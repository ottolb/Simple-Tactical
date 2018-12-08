using System.Collections;
using System.Collections.Generic;
using Game.Event;
using Game.GameInput;
using UnityEngine;
using DG.Tweening;

namespace Game.Gameplay
{

    public class Player : MonoBehaviour
    {

        BaseInput _input;

        Camera cam;

        public LayerMask floorLayerMask, blockLayerMask;

        Vector3 hitPosition;

        public Projector _projector;
        public AnimationCurve projSize;


        public float radiusMin, radiusMax;
        public float power;
        public float upwardsModifier;
        public float offsetExplosionY;

        bool isPlaying, wasPressed;

        public GameObject cubeHitPos;

        Tween projTween;


        private void Awake()
        {
        }

        void Start()
        {
            _input = BaseInput.SelectInput(gameObject);
            cam = Camera.main;
            EventManager.StartListening(N.GameBalance.Updated, OnBalanceUpdated);

            EventManager.StartListening(N.Game.Start, OnGameStarted);
            EventManager.StartListening(N.Game.Over, OnGameStarted);
            EventManager.StartListening(N.Level.Passed, OnLevelPassed);

            _projector.enabled = false;
        }

        public float speedFade;
        void Update()
        {
            if (!isPlaying)
            {
                return;
            }

            if (_input.IsPressing() && CheckRay())
            {
                if (projTween != null)
                {
                    projTween.Kill();
                    projTween = null;
                }
                wasPressed = true;
            }

            float f = _input.ClampedForce();
            //Debug.Log("Force " + f);
            if (f > 0 && projTween == null)
            {
                _projector.orthographicSize = projSize.Evaluate(f);
            }


            if (f < 0.1f && _projector.orthographicSize > projSize.Evaluate(0))
            {
                wasPressed = false;
                if (projTween == null)
                {
                    projTween = DOTween.To(() => _projector.orthographicSize, x => _projector.orthographicSize = x,
                                          0.1f, speedFade).OnComplete(OnProjectorFade);
                }
            }

            if (f > 0)
            {
                ApplyForce(f);
            }
        }

        void OnProjectorFade()
        {
            //Debug.Log("OnProjectorFade");
            projTween = null;
        }

        void ApplyForce(float p_force)
        {
            float c = (p_force * (radiusMax - radiusMin)) + radiusMin;
            //Debug.Log("c " + c);
            Collider[] colliders = Physics.OverlapSphere(hitPosition, c, blockLayerMask);
            //Debug.Log("colliders " + colliders.Length);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(power, hitPosition, c, upwardsModifier);
                    //rb.AddForce
                    Debug.DrawLine(hitPosition, rb.transform.position, Color.blue);
                }
            }
        }

        bool CheckRay()
        {
            Ray ray = cam.ScreenPointToRay(_input.pos);
            //Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000, floorLayerMask))
            {
                hitPosition = hit.point;
                //_projector.SetActive(true);
                _projector.enabled = true;
                _projector.transform.position = hitPosition + Vector3.up * 0.1f;

                hitPosition += Vector3.up * offsetExplosionY;
                cubeHitPos.transform.position = hitPosition;
                return true;
            }
            else
            {
                //_projector.SetActive(false);
                _projector.enabled = false;
            }
            return false;
        }

        void OnBalanceUpdated(object p_data)
        {
            radiusMin = GameBalance.Instance.PlayerMinRadius;
            radiusMax = GameBalance.Instance.PlayerMaxRadius;
            power = GameBalance.Instance.PlayerPower;
            upwardsModifier = GameBalance.Instance.PlayerUpwardsModifier;
        }

        void OnGameStarted(object p_data)
        {
            isPlaying = true;
        }

        void OnGameOver(object p_data)
        {
            isPlaying = false;
            _projector.enabled = false;
        }

        void OnLevelPassed(object p_data)
        {
            isPlaying = false;
            _projector.enabled = false;
        }
    }
}