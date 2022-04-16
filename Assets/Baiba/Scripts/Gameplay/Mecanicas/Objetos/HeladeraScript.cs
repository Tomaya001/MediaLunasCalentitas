using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeladeraScript : GenericObject
{
    public GameObject panel;
    public GameObject joystick;

    public GameObject prefabBrownie;
    public GameObject prefabCheesecake;

    public List<GameObject> poolBrownie;
    public List<GameObject> poolCheesecake;

    private Transform point;
    private bool padre;

    private void Awake()
    {
        InstanciarObjetos(prefabBrownie);
        InstanciarObjetos(prefabCheesecake);
        GeneralPool(poolBrownie, prefabBrownie.gameObject.GetComponent<GenericObject>().id);
        GeneralPool(poolCheesecake, prefabCheesecake.gameObject.GetComponent<GenericObject>().id);
    }

    private void InstanciarObjetos(GameObject prefab)
    {
        prefab.gameObject.tag = prefab.gameObject.GetComponent<GenericObject>().id;
        for (int i = 0; i < 3; i++)
        {
            Instantiate(prefab);
        }
    }

    private void GeneralPool(List<GameObject> pool, string prefab)
    {
        pool.AddRange(GameObject.FindGameObjectsWithTag(prefab));

        foreach (GameObject g in pool)
        {
            g.SetActive(false);
        }
    }

    public void ClickElemento(string ingrediente)
    {
        switch (ingrediente)
        {
            case "Brownie":
                foreach (GameObject g in poolBrownie)
                {
                    if(!g.activeSelf)
                    {
                        g.GetComponent<Rigidbody>().detectCollisions = false;
                        g.GetComponent<Collider>().enabled = false;
                        g.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                        g.transform.position = point.position;
                        if (padre)
                            g.transform.SetParent(point.parent);
                        else
                            g.transform.SetParent(point);
                        g.SetActive(true);
                        break;
                    }                    
                }
                break;
            case "Cheesecake":
                foreach (GameObject g in poolCheesecake)
                {
                    if (!g.activeSelf)
                    {
                        g.GetComponent<Rigidbody>().detectCollisions = false;
                        g.GetComponent<Collider>().enabled = false;
                        g.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                        g.transform.position = point.position;
                        if (padre)
                            g.transform.SetParent(point.parent);
                        else
                            g.transform.SetParent(point);
                        g.SetActive(true);
                        break;
                    }
                }
                break;
        }
        joystick.SetActive(true);
        Time.timeScale = 1f;
        panel.SetActive(false);
    }

    public void AbrirInventario(Transform _point, bool _padre)
    {
        point = _point;
        padre = _padre;
        joystick.SetActive(false);
        panel.SetActive(true);
        Time.timeScale = 0f;
    }
}
