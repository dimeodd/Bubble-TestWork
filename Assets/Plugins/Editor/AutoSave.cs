﻿using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEditor;

namespace EditorTools
{
    [InitializeOnLoad]
    public class AutoSave
    {
        static AutoSave()
        {
            EditorApplication.playModeStateChanged += SaveOnPlay;
        }

        private static void SaveOnPlay(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingEditMode)
            {
                Debug.Log("Auto-saving...");
                EditorSceneManager.SaveOpenScenes();
                AssetDatabase.SaveAssets();
            }
        }
    }
}
