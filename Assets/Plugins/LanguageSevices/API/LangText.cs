using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LanguageSevices;

[RequireComponent(typeof(Text))]
[AddComponentMenu("Language/LangText")]
public class LangText : MonoBehaviour
{
    Text textLabel;
    LString textTag;
    IDisposable disposable;

    public void Start()
    {
        textLabel = GetComponent<Text>();
        textTag = new LString(textLabel.text);
        disposable = LanguageService.Subscribe(SetText);
    }
    private void OnDestroy()
    {
        disposable?.Dispose();
    }

    void SetText()
    {
        //TODO парсить текст на ключи, затем заменять их значения
        // Прим: {Hello_message}: {ItemName}

        //TODO парсить текст на переменные из игры, типа {PlayerName} и подобные

        textLabel.text = textTag;
    }
}
