using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.baiba.core.leveleditor
{
    public class ObjectSet : MonoBehaviour
    {
        public GameObject prefab;
        public string nombre;

        private void Start()
        {
            prefab = AssetLoader.GetAsset<GameObject>(CONST.RESOURCES.PREFAB_FOLDER + "PlayerBasico");
            nombre = prefab.name;
        }
    }
}
