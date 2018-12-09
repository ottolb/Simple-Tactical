using UnityEngine;
using System.Collections;
using Game.Event;

namespace Game
{

    public class FollowTransform : MonoBehaviour
    {
        public Vector3 _positionOffset;

        private Transform _tr;

        public Transform _target;

        public Vector3 targetPos;

        public float smooth;

        void Awake()
        {
            _tr = transform;

            _positionOffset = transform.position - _target.position;
            targetPos = _target.position;
        }

        private void Start()
        {
            EventManager.StartListening(N.Player.Reset, BallPlaced);
            EventManager.StartListening(N.Level.Load, OnGameStart);
            EventManager.StartListening(N.Game.Start, OnGameStart);
            EventManager.StartListening(N.Game.Over, OnGameOver);
            EventManager.StartListening(N.Level.NextLevel, OnNextLevel);
            enabled = false;
        }

        void BallPlaced(object p_desc)
        {
            targetPos = _target.position;
            _tr.position = targetPos + _positionOffset;
            enabled = false;
            Update();
        }

        void OnGameStart(object p_desc)
        {
            enabled = true;
        }

        void OnLevelLoad(object p_desc)
        {
            enabled = true;
        }

        void OnGameOver(object p_desc)
        {
            enabled = false;
        }

        void OnNextLevel(object p_desc)
        {
            enabled = true;
        }

        void Update()
        {
            if (!_target)
                return;

            if (targetPos.y < _target.position.y)
                targetPos.y = _target.position.y;

            //Position
            _tr.position = Vector3.Lerp(_tr.position, targetPos + _positionOffset, Time.deltaTime * smooth);
        }
    }
}