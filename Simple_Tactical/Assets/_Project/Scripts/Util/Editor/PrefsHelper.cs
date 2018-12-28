//
// Author:
//       Otto Lopes <otto@buildandrungames.com>
//
// Copyright (c) 2018 Secret Weapon 
//

using UnityEngine;
using UnityEditor;

namespace Game
{

    public class PrefsHelper : Editor
    {
        [MenuItem("Editor/Player Prefs/Clear All")]
        public static void ClearAllPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            EditorUtility.DisplayDialog("Success", "All Player Preferences deleted", "Ok");
        }

    }
}