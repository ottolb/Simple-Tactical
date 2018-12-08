using UnityEngine;
using System.Collections;
using Game.Event;

namespace Game
{

    [ExecuteInEditMode()]
    public class LevelBounds : MonoBehaviour
    {
        public Camera cam;

        public GameObject wallLeft, wallRight, wallTop, wallBottom;

        public Vector3 left, right, top, bottom;

        Transform bkgMesh;

        void Awake()
        {
            bkgMesh = transform.Find("Background");
        }

        private void Start()
        {
            //EventManager.StartListening(N.Ball.Reset, BallPlaced);
            //EventManager.StartListening(N.Level.Load, OnGameStart);
            //EventManager.StartListening(N.Game.Start, OnGameStart);
            //EventManager.StartListening(N.Game.Over, OnGameOver);
            //EventManager.StartListening(N.Level.NextLevel, OnNextLevel);

            Setup();
        }

        private void OnEnable()
        {
            Setup();
        }

        // Use this for initialization
        public void Setup()
        {
            cam = this.gameObject.GetComponent<Camera>();


            float screenAspect = (float)Screen.width / (float)Screen.height;

            float depth = bkgMesh.transform.localPosition.z;
            left = cam.ScreenToWorldPoint(new Vector3(0, Screen.height / 2, depth));
            right = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / 2, depth));
            top = cam.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height, depth));
            bottom = cam.ScreenToWorldPoint(new Vector3(Screen.width / 2, 0, depth));

            wallLeft.transform.position = left;
            wallRight.transform.position = right;
            wallTop.transform.position = top;
            wallBottom.transform.position = bottom;

        }

    }
}