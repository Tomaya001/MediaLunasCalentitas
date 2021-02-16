using com.baiba.core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.baiba.core.lang
{
    public class Language
    {
        //Instanciamiento Estatico
        private static Language _instance;
        public static Language instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Language();
                }
                return _instance;
            }
        }
        //Fin Instanciamiento Estatico

        private Dictionary<string, string> language;
        public void Init(string lang)
        {
            TextAsset ta = AssetLoader.GetAsset<TextAsset>(CONST.RESOURCES.LANG_FOLDER + lang);
            //TextAsset ta = Resources.Load<TextAsset>(CONST.RESOURCES.LANG_FOLDER + lang);
            LanguageList langList = JsonUtility.FromJson<LanguageList>(ta.text);

            language = new Dictionary<string, string>();

            foreach (LanguageRef langRef in langList.lang)
            {
                language.Add(langRef.key, langRef.value);
            }
            OnChangeLanguage(lang);

            PlayerPrefs.SetString("lang", lang);
            PlayerPrefs.Save();
        }

        public string GetString(string key)
        {
            if (language.ContainsKey(key))
            {
                return language[key];
            }

            return key;
        }

        public void OnChangeLanguage(string lang)
        {
            UITextTranslation[] tt = GameObject.FindObjectsOfType<UITextTranslation>();
            foreach (UITextTranslation t in tt)
            {
                t.SetText();
            }
        }
    }
}

