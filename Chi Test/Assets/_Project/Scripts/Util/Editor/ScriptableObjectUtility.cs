//
// Author:
//       Otto Lopes <otto@buildandrungames.com>
//
// Copyright (c) 2015 Secret Weapon 
//

using System.IO;
using UnityEditor;
using UnityEngine;

namespace Game
{

    public class ScriptableObjectUtility : Editor
    {

        [MenuItem("Editor/Create Level Setup")]
        public static void CreateAsset()
        {
            CreateAsset<Gameplay.CharacterData>();
        }

        /// <summary>
        //	This makes it easy to create, name and place unique new ScriptableObject asset files.
        /// </summary>
        public static Object CreateAsset<T>() where T : ScriptableObject
        {
            T asset = ScriptableObject.CreateInstance<T>();

            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (path == "")
            {
                path = "Assets";
            }
            else if (Path.GetExtension(path) != "")
            {
                path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
            }

            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New " + typeof(T).ToString() + ".asset");

            AssetDatabase.CreateAsset(asset, assetPathAndName);

            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
            return Selection.activeObject;
        }
    }
}