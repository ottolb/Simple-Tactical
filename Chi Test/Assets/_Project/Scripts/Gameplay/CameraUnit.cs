using UnityEngine;
using System.Collections;
using Game.Event;
using DG.Tweening;


namespace Game
{

    public class CameraUnit : MonoBehaviour
    {
        public Vector3 _positionOffset;

        private Transform _tr;

        private Transform _target;

        public Vector3 targetPos;

        public float smooth, smoothLookAt;

        bool shouldLookAt;

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
            shouldLookAt = false;
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


            targetPos = _target.transform.position;
            //Position
            _tr.position = Vector3.Lerp(_tr.position, targetPos + _positionOffset, Time.deltaTime * smooth);

            Quaternion targetRotation = Quaternion.LookRotation(targetPos - transform.position);

            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * smoothLookAt);
        }
    }
}