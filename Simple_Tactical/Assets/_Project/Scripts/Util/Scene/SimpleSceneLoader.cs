//
// Author:
//       Otto Lopes <otto@buildandrungames.com>
//
// Copyright (c) 2018 Build and Run Games 
//

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


namespace Game
{
    public class SimpleSceneLoader : MonoBehaviour
    {
        public float sceneShowTime;

        public string sceneName;

        private float timer = 0;

        void Update()
        {
            timer += Time.deltaTime;

            if (timer > sceneShowTime)
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}