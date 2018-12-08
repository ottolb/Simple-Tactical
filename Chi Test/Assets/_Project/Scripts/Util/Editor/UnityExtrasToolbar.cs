//
// 	UnityExtrasToolbar.cs
//
// Author:
//       Otto Lopes <otto@buildandrungames.com>
//
// Copyright (c) 2015 Secret Weapon 
//

using UnityEngine;
using UnityEditor;

namespace Game
{

    class UnityExtrasToolbar : EditorWindow
    {

        [MenuItem("Window/Unity Extras")]
        static void Init()
        {
            UnityExtrasToolbar window = (UnityExtrasToolbar)EditorWindow.GetWindow(typeof(UnityExtrasToolbar), false, "Extras");
            window.Show();
        }

        void OnGUI()
        {

            GUIStyle someGUIStyle = GUI.skin.GetStyle("minibutton");
            // update style to fit minimum editor window width
            someGUIStyle.padding = new RectOffset(1, 1, 0, 0);
            someGUIStyle.overflow = new RectOffset(0, 0, 2, 4);
            // someGUIStyle.fixedWidth = 0;
            someGUIStyle.fixedHeight = 20f;
            someGUIStyle.imagePosition = ImagePosition.ImageAbove;

            // project settings
            GUIContent someGuiContent = new GUIContent();

            someGuiContent.tooltip = someGuiContent.text = "Project Settings";
            GUILayout.Label(someGuiContent, EditorStyles.boldLabel);
            GUILayout.Space(4f);

            GUILayout.BeginHorizontal();

            someGuiContent.text = "";

            someGuiContent.tooltip = "Input";
            someGuiContent.image = EditorGUIUtility.Load("icons/d_movetool.png") as Texture2D;
            if (GUILayout.Button(someGuiContent, someGUIStyle, GUILayout.MinWidth(40), GUILayout.MaxWidth(80)))
            {
                EditorApplication.ExecuteMenuItem("Edit/Project Settings/Input");
            }

            someGuiContent.tooltip = "Tags";
            someGuiContent.image = EditorGUIUtility.Load("icons/d_unityeditor.hierarchywindow.png") as Texture2D;
            if (GUILayout.Button(someGuiContent, someGUIStyle, GUILayout.MinWidth(40), GUILayout.MaxWidth(80)))
            {
                EditorApplication.ExecuteMenuItem("Edit/Project Settings/Tags and Layers");
            }

#if ENABLE_NAVIGATION
            someGuiContent.tooltip = "Navigation";
            someGuiContent.image = AssetPreview.GetMiniTypeThumbnail(typeof(UnityEngine.AI.NavMeshAgent)) as Texture2D;
            if (GUILayout.Button(someGuiContent, someGUIStyle, GUILayout.MinWidth(40), GUILayout.MaxWidth(80)))
            {
                EditorApplication.ExecuteMenuItem("Window/Navigation");
            }
#endif
            someGuiContent.tooltip = "Lighting";
            someGuiContent.image = AssetPreview.GetMiniTypeThumbnail(typeof(Light)) as Texture2D;
            if (GUILayout.Button(someGuiContent, someGUIStyle, GUILayout.MinWidth(40), GUILayout.MaxWidth(80)))
            {
                EditorApplication.ExecuteMenuItem("Window/Lighting");
            }

            someGuiContent.tooltip = "Audio";
            someGuiContent.image = AssetPreview.GetMiniTypeThumbnail(typeof(AudioClip)) as Texture2D;

            if (GUILayout.Button(someGuiContent, someGUIStyle, GUILayout.MinWidth(40), GUILayout.MaxWidth(80)))
            {
                EditorApplication.ExecuteMenuItem("Edit/Project Settings/Audio");
            }

            someGuiContent.tooltip = "Time";

            someGuiContent.image = AssetPreview.GetMiniTypeThumbnail(typeof(Animation)) as Texture2D;

            if (GUILayout.Button(someGuiContent, someGUIStyle, GUILayout.MinWidth(40), GUILayout.MaxWidth(80)))
            {
                EditorApplication.ExecuteMenuItem("Edit/Project Settings/Time");
            }

            someGuiContent.tooltip = "Player";
            someGuiContent.image = EditorGUIUtility.Load("icons/d_unityeditor.gameview.png") as Texture2D;
            if (GUILayout.Button(someGuiContent, someGUIStyle, GUILayout.MinWidth(40), GUILayout.MaxWidth(80)))
            {
                EditorApplication.ExecuteMenuItem("Edit/Project Settings/Player");
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(15);
            GUILayout.BeginHorizontal();

            someGuiContent.tooltip = "Physics";
            someGuiContent.image = AssetPreview.GetMiniTypeThumbnail(typeof(PhysicMaterial)) as Texture2D;

            if (GUILayout.Button(someGuiContent, someGUIStyle, GUILayout.MinWidth(40), GUILayout.MaxWidth(80)))
            {
                EditorApplication.ExecuteMenuItem("Edit/Project Settings/Physics");
            }

#if ENABLE_PHYSICS2D
            someGuiContent.tooltip = "Physics 2D";
            someGuiContent.image = AssetPreview.GetMiniTypeThumbnail(typeof(BoxCollider2D)) as Texture2D;

            if (GUILayout.Button(someGuiContent, someGUIStyle, GUILayout.MinWidth(40), GUILayout.MaxWidth(80)))
            {
                EditorApplication.ExecuteMenuItem("Edit/Project Settings/Physics 2D");
            }
#endif
            someGuiContent.tooltip = "Quality";
            someGuiContent.image = EditorGUIUtility.Load("icons/d_viewtoolorbit.png") as Texture2D;
            if (GUILayout.Button(someGuiContent, someGUIStyle, GUILayout.MinWidth(40), GUILayout.MaxWidth(80)))
            {
                EditorApplication.ExecuteMenuItem("Edit/Project Settings/Quality");
            }

            //someGuiContent.tooltip = "Network";
            //someGuiContent.image = AssetPreview.GetMiniTypeThumbnail(typeof(UnityEngine.Networking.NetworkLobbyManager)) as Texture2D;
            //if (GUILayout.Button(someGuiContent, someGUIStyle, GUILayout.MinWidth(40), GUILayout.MaxWidth(80)))
            //{
            //    EditorApplication.ExecuteMenuItem("Edit/Project Settings/Network");
            //}

            someGuiContent.tooltip = "Editor";
            someGuiContent.text = "Editor";
            someGuiContent.image = null;

            if (GUILayout.Button(someGuiContent, someGUIStyle, GUILayout.MinWidth(40), GUILayout.MaxWidth(80)))
            {
                EditorApplication.ExecuteMenuItem("Edit/Project Settings/Editor");
            }

            someGuiContent.tooltip = "Script Execution Order";
            someGuiContent.text = "Order";
            someGuiContent.image = null;

            if (GUILayout.Button(someGuiContent, someGUIStyle, GUILayout.MinWidth(40), GUILayout.MaxWidth(80)))
            {
                EditorApplication.ExecuteMenuItem("Edit/Project Settings/Script Execution Order");
            }

            EditorGUILayout.EndHorizontal();

        }
    }
}