using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LanguageSevices
{
    [Serializable]
    public sealed class LString
    {
        [Tooltip("ID for languige translation")]
        [SerializeField] string _textID;
        [NonSerialized] string text;
        [NonSerialized] IDisposable _disposable;

        public LString(string textTag)
        {
            _textID = textTag;
            _disposable = LanguageService.Subscribe(RefreshText);
        }
        ~LString() => _disposable.Dispose();

        public void RefreshText()
        {
            try
            {
                text = LanguageService.GetTextByID(_textID);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
                text = "[PH]" + _textID;
            }
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(text))
                RefreshText();

            return text;
        }

        public static implicit operator LString(string str) => new LString(str);
        public static implicit operator string(LString str) => str.ToString();
    }
}