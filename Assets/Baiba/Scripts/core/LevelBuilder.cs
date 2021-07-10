using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace com.baiba.core.leveleditor
{
    public class LevelBuilder : MonoBehaviour
    {

        public GameObject Object;

        private List<GameObject> objects = new List<GameObject>();

        public void SaveLevel()
        {
            ListObjectScript list = new ListObjectScript();
            objects.Clear();
            objects.AddRange(FindObjectsOfType<GameObject>());

            foreach (var obj in objects)
            {
                if(obj.gameObject.GetComponent<ObjectSet>())
                {
                    ObjectScript os = new ObjectScript();
                    os.nombre = obj.name;
                    os.poss = obj.transform.position;
                    os.type = obj.GetType().ToString();
                    os.ruta = CONST.RESOURCES.PREFAB_FOLDER + obj.GetComponent<ObjectSet>().prefab.name;
                    list.nivel.Add(os);                   
                }
            }

            string json = JsonUtility.ToJson(list);

            StreamWriter writer = new StreamWriter(CONST.RESOURCES.FILES_FOLDER + "level.json");
            Debug.Log("NIVEL GUARDADO");
            writer.WriteLine(json);
            writer.Close();
        }

        public void SaveLevel(string nivel)
        {
            ListObjectScript list = new ListObjectScript();
            objects.Clear();
            objects.AddRange(FindObjectsOfType<GameObject>());

            Debug.Log(objects.Count);

            foreach (var obj in objects)
            {
                if (obj.gameObject.GetComponent<ObjectSet>())
                {
                    ObjectScript os = new ObjectScript();
                    os.nombre = obj.name;
                    os.poss = obj.transform.position;
                    os.type = obj.GetType().ToString();
                    os.ruta = CONST.RESOURCES.PREFAB_FOLDER + obj.GetComponent<ObjectSet>().prefab.name;
                    list.nivel.Add(os);
                }
            }

            string json = JsonUtility.ToJson(list);

            StreamWriter writer = new StreamWriter(CONST.RESOURCES.FILES_FOLDER + nivel + ".json");
            Debug.Log("NIVEL GUARDADO");
            writer.WriteLine(json);
            writer.Close();
        }

        public void LoadLevel()
        {
            ListObjectScript list;
            StreamReader reader = new StreamReader(CONST.RESOURCES.FILES_FOLDER + "level.json");
            string text = reader.ReadToEnd();
            reader.Close();

            list = JsonUtility.FromJson<ListObjectScript>(text);

            foreach (var ob in list.nivel)
            {
                Debug.Log(ob.ruta);
                Instantiate(AssetLoader.GetAsset<GameObject>(ob.ruta), ob.poss, Quaternion.identity);
            }
            Debug.Log("NIVEL CARGADO");
        }
    }
}

