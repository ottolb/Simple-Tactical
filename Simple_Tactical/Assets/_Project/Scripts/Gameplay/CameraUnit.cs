using UnityEngine;
using System.Collections;
using Game.Event;
using DG.Tweening;


namespace Game
{

    public class CameraUnit : MonoBehaviour
    {
        public Vector3 _positionOffset;

        public Vector3 minPositionOffset;

        public Vector3 maxPositionOffset;

        private Transform _tr;

        private Transform _target;

        public Vector3 targetPos;

        public float smooth, smoothLookAt, controlSpeed;


        void Awake()
        {
            _tr = transform;
        }

        private void Start()
        {
            EventManager.StartListening(N.Game.Start, OnGameStart);
            EventManager.StartListening(N.Game.Over, OnGameOver);
            EventManager.StartListening(N.Unit.SelectUnit, OnUnitSelected);
            enabled = false;
        }

        void OnUnitSelected(object p_unit)
        {
            _target = (UnityEngine.Transform)p_unit;
            //_tr.DOLookAt(_target.position, 0.7f).OnComplete(delegate
            //{
            //    shouldLookAt = true;
            //});
        }

        void OnGameStart(object p_desc)
        {
            enabled = true;
        }

        void OnGameOver(object p_desc)
        {
            enabled = false;
        }

        void Update()
        {
            if (!_target)
                return;

            HandleMovement();

            targetPos = _target.transform.position;
            //Position
            _tr.position = Vector3.Lerp(_tr.position, targetPos + _positionOffset, Time.deltaTime * smooth);

            Quaternion targetRotation = Quaternion.LookRotation(targetPos - transform.position);

            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smoothLookAt);
        }

        void HandleMovement()
        {
            _positionOffset.x += Input.GetAxis("Horizontal") * Time.deltaTime * controlSpeed;
            _positionOffset.z += Input.GetAxis("Vertical") * Time.deltaTime * controlSpeed;
            _positionOffset.y += Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * controlSpeed;


            _positionOffset.x = Mathf.Clamp(_positionOffset.x, minPositionOffset.x, maxPositionOffset.x);
            _positionOffset.y = Mathf.Clamp(_positionOffset.y, minPositionOffset.y, maxPositionOffset.y);
            _positionOffset.z = Mathf.Clamp(_positionOffset.z, minPositionOffset.z, maxPositionOffset.z);
        }
    }
}