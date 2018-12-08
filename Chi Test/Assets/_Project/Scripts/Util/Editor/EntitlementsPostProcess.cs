using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using BuildandRun.iOS.Xcode.Custom;

namespace Game
{
    public class EntitlementsPostProcess : ScriptableObject
    {
        public DefaultAsset m_entitlementsFile;

        [PostProcessBuild]
        public static void OnPostProcess(BuildTarget buildTarget, string buildPath)
        {
            if (buildTarget != BuildTarget.iOS)
            {
                return;
            }


            string proj_path = PBXProject.GetPBXProjectPath(buildPath);
            PBXProject proj = new PBXProject();
            proj.ReadFromFile(proj_path);

            // target_name = "Unity-iPhone"
            string target_name = PBXProject.GetUnityTargetName();
            string targetGuid = proj.TargetGuidByName(target_name);

            //proj.SetBuildProperty(targetGuid, "ENABLE_BITCODE", "YES");

            //Remove from build and deployment debug symbols
            string configGuid = proj.BuildConfigByName(targetGuid, "Debug");
            proj.SetBuildPropertyForConfig(configGuid, "DEBUG_INFORMATION_FORMAT", "dwarf");
            //proj.SetBuildPropertyForConfig(configGuid, "COPY_PHASE_STRIP", "NO");
            Debug.LogFormat("[BuildUtils] Configuration GUID: {0}", configGuid);

            //Remove from build and deployment debug symbols
            configGuid = proj.BuildConfigByName(targetGuid, "Release");
            proj.SetBuildPropertyForConfig(configGuid, "DEBUG_INFORMATION_FORMAT", "dwarf");
            //proj.SetBuildPropertyForConfig(configGuid, "COPY_PHASE_STRIP", "NO");

            //Remove from build and deployment debug symbols
            configGuid = proj.BuildConfigByName(targetGuid, "ReleaseForRunning");
            proj.SetBuildPropertyForConfig(configGuid, "DEBUG_INFORMATION_FORMAT", "dwarf");
            //proj.SetBuildPropertyForConfig(configGuid, "COPY_PHASE_STRIP", "NO");

        }


    }
}