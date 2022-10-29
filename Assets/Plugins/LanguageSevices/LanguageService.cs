using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.IO;
using UnityEngine;
using System.Runtime.CompilerServices;

namespace LanguageSevices
{
    public static partial class LanguageService
    {
#if UNITY_EDITOR
        public static readonly string langDataPath = Application.dataPath + "/_testFolder/Languages";
#else
        public static readonly string langDataPath = Application.dataPath + "/Languages";
#endif

        static LanguageSettings languageSettings;
        static bool isStarted;
        static Parcer parcerEN;
        static Parcer parcerLANG;

        [UnityEngine.RuntimeInitializeOnLoadMethod]
        static void Init()
        {
            Log("Initialization");
            languageSettings = Resources.LoadAll<LanguageSettings>("Language")[0];

            //проверка существования данных в папке

            //проверка языка пользователя
            var hasKey = PlayerPrefs.HasKey("Lang");
            if (!hasKey)
            {
                var langPrefix = Application.systemLanguage == SystemLanguage.Russian ?
                    "RU" :
                    "EN";
                PlayerPrefs.SetString("Lang", langPrefix);
                PlayerPrefs.Save();
            }

            var Lang = PlayerPrefs.GetString("Lang");

            //создание запасного парсера
            var textEN = GetLang("EN").TextAsset.text;
            parcerEN = new Parcer(textEN);

            //создание основного парсера
            SetLanguige(Lang);

            isStarted = true;
            Log("Initialization Complete!");
        }

        /// <summary>
        /// Выдаёт перевод по ключу
        /// </summary>
        /// <param name="tag">ключ</param>
        /// <returns>перевод</returns>
        public static string GetTextByID(string tag)
        {
            if (!isStarted)
                throw new Exception("LanguageSetvice not Inited");

            string text = null;
            try
            {
                if (!parcerLANG.TrySearchByID(tag, out text))
                {
                    text = parcerEN.SearchByID(tag);
                }
            }
            catch (ParcerException ex)
            {
                Log($"Tag {tag} not found");
                text = "[PH]_" + tag;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return text;
        }

        /// <summary>
        /// меняет язык приложения
        /// </summary>
        /// <param name="LANG">прим: EN</param>
        public static void SetLanguige(string LANG)
        {
            var textLang = GetLang(LANG).TextAsset.text;
            parcerLANG = new Parcer(textLang);

            var oldLANG = PlayerPrefs.GetString("Lang");
            if (oldLANG != LANG)
            {
                PlayerPrefs.SetString("Lang", LANG);
                PlayerPrefs.Save();
            }

            UpdateLStrings();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void Log(string str)
        {
            UnityEngine.Debug.Log("LanguageService: " + str);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void LogError(string str)
        {
            Log("Runtime Error => " + str);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static LanguageSettings.LangData GetLang(string LANG)
        {
            foreach (var langData in languageSettings.Languages)
            {
                if (langData.LANG == LANG) return langData;
            }

            throw new Exception(string.Format(
                "language:{0} not exist",
                LANG
            ));
        }
    }
}