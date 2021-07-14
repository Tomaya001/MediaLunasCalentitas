using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.baiba.core
{
    public class CONST : MonoBehaviour
    {
        public static class TAG
        {
            public static string PLAYER = "Player";
            public static string OBJETO = "Objeto";
            public static string BANDEJA = "Bandeja";
            public static string HAND = "Hand";
            public static string JOYSTICK = "Joystick";
        }

        public static class LAYER
        {
            public static int PLAYER = 8;
        }

        public static class RESOURCES
        {
            public static string LANG_FOLDER = "lang/";
            public static string FILES_FOLDER = "Assets/Baiba/Resources/files/";
            public static string PREFAB_FOLDER = "prefab/";
        }

        public static class SCENES
        {
            public static string LOGO = "Logo";
            public static string MAINMENU = "MainMenu";
            public static string GAMEPLAY = "Pruebas";
        }
    }
}

