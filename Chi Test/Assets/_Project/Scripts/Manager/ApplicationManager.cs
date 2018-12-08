//
// Author:
//       Otto Lopes <otto@buildandrungames.com>
//
// Copyright (c) 2015 Build and Run Games
//

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Game.Event;

namespace Game
{
    /// <summary>
    /// High level application control
    /// </summary>
    public class ApplicationManager : MonoBehaviour
    {
        static public ApplicationManager Instance;

        // First time that application runs
        public bool IsFirstTime;

        private bool _initialized;

        public NetworkReachability networkReachability;

        [Range(15, 80), Header("Set the target frame rate, pass -1 to use platform default frame rate")]
        public int FPS;

        [Range(0, 500), Header("Device screen sleep timeout")]
        public int sleepTimeout;

        [Header("Run app in background")]
        public bool runInBackground;

        /// <summary>
        /// Perform platform check and setup specific settings
        /// </summary>
        void Awake()
        {
            Instance = this;
            /*	if(Instance)
				{
					DestroyImmediate(gameObject);
				}
				else {
					Instance = this;
					DontDestroyOnLoad(gameObject);
				}*/


            Application.targetFrameRate = FPS;
            Screen.sleepTimeout = sleepTimeout;
            Application.runInBackground = runInBackground;

        }

        internal void ForceDeviceOrientation(ScreenOrientation p_rotation)
        {
            Screen.orientation = p_rotation;
        }

        /// <summary>
        /// Initialize other components
        /// </summary>
        void Start()
        {
            StartCoroutine(HandleAppStart());

            ForceDeviceOrientation(ScreenOrientation.Portrait);
        }


        IEnumerator HandleAppStart()
        {
            if (Debug.isDebugBuild)
                ShowDebugInfo();

            EventManager.TriggerEvent(N.App.Opened);

            yield return new WaitForEndOfFrame();

            EventManager.TriggerEvent(N.App.Loaded);
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }



        void ShowDebugInfo()
        {
            string versionStr;
#if UNITY_IOS
			versionStr = BundleVersionBindings.BundleVersion;
#else
            versionStr = Application.version;
#endif
            versionStr = "v " + versionStr;

            EventUIManager.TriggerEvent(NUI.Debug.Show, versionStr);
        }



        public bool IsNetworkAvailable
        {
            get
            {
                if (Application.isEditor)
                    return networkReachability != NetworkReachability.NotReachable;
                else
                    return Application.internetReachability != NetworkReachability.NotReachable;
            }
        }
    }
}