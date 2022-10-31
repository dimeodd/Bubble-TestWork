using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadRandomLevel : MonoBehaviour
{
    public Button PlayButton;
    public Button NewSeedButton;
    public InputField SeedField;

    System.Random _rnd;

    void Start()
    {
        var seed = (int)System.DateTime.UtcNow.Ticks;
        _rnd = new System.Random(seed);
        SeedField.text = seed.ToString();

        PlayButton.onClick.AddListener(Play);
        NewSeedButton.onClick.AddListener(NewSeed);
    }

    void Play()
    {
        if (!int.TryParse(SeedField.text, out var seed)) return;

        var rnd = new System.Random(seed);
        var level = new LevelData();

        level.IsRandomMap = true;
        level.LevelSeed = rnd.Next();

        level.IsRandomColors = true;
        level.ColorSeed = rnd.Next();

        World.Level = level;
        SceneManager.LoadScene("World");
    }

    void NewSeed()
    {
        var seed = _rnd.Next();
        SeedField.text = seed.ToString();
    }
}
