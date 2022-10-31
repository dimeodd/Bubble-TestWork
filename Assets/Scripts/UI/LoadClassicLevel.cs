using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class LoadClassicLevel : MonoBehaviour
{
    public LevelData level;
    void Start()
    {
        if (level == null) throw new NullReferenceException();
        var btn = GetComponent<Button>();
        btn.onClick.AddListener(LoadLevel);
    }

    void LoadLevel()
    {
        World.Level = level;
        SceneManager.LoadScene("World");
    }
}
