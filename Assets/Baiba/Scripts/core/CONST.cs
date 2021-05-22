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
        }

        public static class LAYER
        {
            public static int PLAYER = 8;
        }

        public static class RESOURCES
        {
            public static string LANG_FOLDER = "lang/";
            public static string FILES_FOLDER = "files/";
        }

        public static class SCENES
        {
            public static int LOGO = 1;
            public static int MAINMENU = 2;
            public static int GAMEPLAY = 3;
        }
    }
}

