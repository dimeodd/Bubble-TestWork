using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;


namespace LanguageSevices
{
    public class Parcer
    {
        char[] data;
        Dictionary<string, uint> allID;

        public Parcer(string text)
        {
            //подгодовить данные к обработке
            data = text.ToCharArray();

            // Найти все ключи
            allID = new Dictionary<string, uint>();
            StringBuilder sb = new StringBuilder();

            for (uint i = 0, iMax = (uint)data.Length; i < iMax; i++)
            {
                if (data[i] != '$') continue;
                sb.Clear();

                i++; // перемещение после $
                do
                {
                    sb.Append(data[i]);
                    i++;
                }
                while (i < iMax && data[i] != ';');
                i++;// перемещение после ;

                allID[sb.ToString()] = i;
            }
        }

        public bool TrySearchByID(string ID, out string result)
        {
            result = null;
            if (allID == null)
                throw new Exception("Text will not set. Use LoadText()");

            if (!allID.TryGetValue(ID, out var index)) return false;

            result = GetText(data, index);

            return true;
        }
        public string SearchByID(string ID)
        {
            if (allID == null)
                throw new Exception("Text will not set. Use LoadText()");

            if (!allID.TryGetValue(ID, out var index))
                throw new ParcerException();

            return GetText(data, index);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static string GetText(char[] data, uint index)
        {
            StringBuilder sb = new StringBuilder();

            // Запись значения
            var iMax = data.Length;

            while (index < iMax && data[index] != ';')
            {
                sb.Append(data[index]);
                index++;
            }
            if (sb.Length < 1)
                throw new ParcerException();

            return sb.ToString();
        }

        public string[] GetAllKeys()
        {
            List<string> keyList = new List<string>(allID.Keys);
            return keyList.ToArray();
        }

    }


///
    public class ParcerException : Exception
    {
        public ParcerException() : base() { }
    }
}
