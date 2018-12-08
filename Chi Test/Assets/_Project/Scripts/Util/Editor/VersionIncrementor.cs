// Version Incrementor Script for Unity by Francesco Forno (Fornetto Games)
// Inspired by http://forum.unity3d.com/threads/automatic-version-increment-script.144917/

using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Game
{

    //[InitializeOnLoad]
    public class VersionIncrementor
    {
        static VersionIncrementor()
        {
#if !UNITY_CLOUD_BUILD
            //TextAsset keyPass = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/_Project/Editor/Android_key/keystore_pass.txt");
            //if (keyPass == null)
            //{
            //    Debug.LogError("Can't find password");
            //    return;
            //}

            //string[] passes;
            //passes = keyPass.text.Split(',');

            //PlayerSettings.Android.keystorePass = passes[0];
            //PlayerSettings.Android.keyaliasName = "tommy";
            //PlayerSettings.Android.keyaliasPass = passes[1];


            if (!CheckFacebookId())
            {
                EditorUtility.DisplayDialog("Warning", "Incorrect facebook id", "Ok");
            }
#endif
        }


        [PostProcessBuildAttribute(1)]
        public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
        {
            Debug.Log("Build version: " + PlayerSettings.bundleVersion + " (" + PlayerSettings.Android.bundleVersionCode + ")");
            IncreaseBuild();
        }

        static void IncrementVersion(int majorIncr, int minorIncr, int buildIncr)
        {
            string[] lines = PlayerSettings.bundleVersion.Split('.');

            int MajorVersion = int.Parse(lines[0]) + majorIncr;
            int MinorVersion = int.Parse(lines[1]) + minorIncr;
            int Build = 0;

#if UNITY_ANDROID
        Build = PlayerSettings.Android.bundleVersionCode + buildIncr;
#elif UNITY_IOS
        Build = int.Parse(PlayerSettings.iOS.buildNumber) + buildIncr;
#endif


            PlayerSettings.bundleVersion = MajorVersion.ToString("0") + "." +
                                            MinorVersion.ToString("00") + "." +
                                            Build.ToString("0");

            PlayerSettings.Android.bundleVersionCode = Build;
            PlayerSettings.iOS.buildNumber = Build.ToString();

        }

        [MenuItem("Editor/Build/Increase Minor Version")]
        private static void IncreaseMinor()
        {
            IncrementVersion(0, 1, 0);
        }

        [MenuItem("Editor/Build/Increase Major Version")]
        private static void IncreaseMajor()
        {
            IncrementVersion(1, 0, 0);
        }

        [MenuItem("Editor/Build/Increase Build Version")]
        private static void IncreaseBuild()
        {
            IncrementVersion(0, 0, 1);
        }


        static bool CheckFacebookId()
        {
            //ApplicationManager applicationManager = Object.FindObjectOfType<ApplicationManager>();
            //if (applicationManager == null)
            //    return true;

            //Srv.ServerType serverType = applicationManager.serverType;
            //if (serverType == Srv.ServerType.Production
            //    && !IsCorrectFacebookId(0))
            //{

            //    return false;
            //}
            //else if (serverType == Srv.ServerType.Test
            //    && !IsCorrectFacebookId(1))
            //{
            //    return false;
            //}
            //else if (serverType == Srv.ServerType.Development
            //    && !IsCorrectFacebookId(2))
            //{
            //    return false;
            //}

            return true;
        }
        /*static bool IsCorrectFacebookId(int p_appIndex)
        {
            Debug.LogFormat("App Id {0} | Settings {1}", Facebook.Unity.Settings.FacebookSettings.AppId,
                            Facebook.Unity.Settings.FacebookSettings.AppIds[p_appIndex]);
            return Facebook.Unity.Settings.FacebookSettings.AppId == Facebook.Unity.Settings.FacebookSettings.AppIds[p_appIndex];
        }*/
    }
}