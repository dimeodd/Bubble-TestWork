using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using LanguageSevices;

public class SettingsWindow : EditorWindow
{
    static String AssetPath = "Language";
    static String DirPath => Application.dataPath + "/Resources/" + AssetPath;

    static bool folderExist, fileExist;
    static DirectoryInfo dir;
    static LanguageSettings source;


    [MenuItem("Settings/Language")]
    private static void ShowWindow()
    {
        dir = new DirectoryInfo(DirPath);
        var window = GetWindow<SettingsWindow>();
        window.titleContent = new GUIContent("SettingsWindow");
        window.Show();
    }

    private void OnGUI()
    {
        var a = Resources.LoadAll<LanguageSettings>(AssetPath);
        source = a.Length > 0 ? a[0] : null;
        fileExist = source != null;
        folderExist = dir.Exists;

        GUILayout.Label(string.Format(
            "folder:{0} file:{1}",
            folderExist,
            fileExist
        ));

        if (!fileExist)
            ShowCreateSettings();
        else
            ShowSettings();
    }

    void ShowCreateSettings()
    {
        // Всё записывается в "Resources", чтобы "static class" мог найти файл настроек в рантайме
        // Это "удобнее" и НАДЁЖНЕЕ, чем через монобехи
        // Единственный минус, файл настроек в Resources
        GUILayout.Label(DirPath);
        if (GUILayout.Button("Create"))
        {
            var dir = new DirectoryInfo(DirPath);
            if (!dir.Exists) dir.Create();
            AssetDatabase.CreateAsset(new LanguageSettings(), "Assets/Resources/" + AssetPath + "/LanguageSettings.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            source = Resources.Load<LanguageSettings>(AssetPath);
        }
    }


    private SerializedProperty serializedProperty;
    void ShowSettings()
    {
        var serializedObject = new SerializedObject(source);
        serializedProperty = serializedObject.GetIterator();
        serializedProperty.NextVisible(true);
        DrawProperties(serializedProperty);

        serializedObject.ApplyModifiedProperties();
    }

    protected void DrawProperties(SerializedProperty p)
    {

        while (p.NextVisible(false))
        {
            EditorGUILayout.PropertyField(p, true);
        }
    }
}
