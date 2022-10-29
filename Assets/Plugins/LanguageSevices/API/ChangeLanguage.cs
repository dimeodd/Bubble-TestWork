using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LanguageSevices;


[AddComponentMenu("Language/ChangeLanguage")]
public class ChangeLanguage : MonoBehaviour
{
    public void EN()
    {
        LanguageService.SetLanguige("EN");
    }

    public void RU()
    {
        LanguageService.SetLanguige("RU");
    }
}
