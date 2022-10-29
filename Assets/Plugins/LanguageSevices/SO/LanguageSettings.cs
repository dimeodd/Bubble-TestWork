using UnityEngine;


namespace LanguageSevices
{
    [CreateAssetMenu(fileName = "LanguageSettings", menuName = "LanguageData/Settings", order = 0)]
    public class LanguageSettings : ScriptableObject
    {
        public LangData[] Languages;

        [System.Serializable]
        public class LangData
        {
            public string LANG;
            public TextAsset TextAsset;
        }
    }
}