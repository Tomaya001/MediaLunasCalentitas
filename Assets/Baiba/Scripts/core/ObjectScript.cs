using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.baiba.core.leveleditor
{
    [Serializable]
    public class ObjectScript
    {
        public string nombre;
        public Vector3 poss;
        public string type;
        public string ruta;
    }

    [Serializable]
    public class ListObjectScript
    {
        public List<ObjectScript> nivel;

        public ListObjectScript()
        {
            nivel = new List<ObjectScript>();
        }
    }
}

