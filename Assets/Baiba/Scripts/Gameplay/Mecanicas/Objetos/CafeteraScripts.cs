using com.baiba.core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CafeteraScripts : GenericObject
{
    public Image temporizador;
    public float tiempoEspera;
    public Transform punto;
    public bool tazaLista;

    
    [SerializeField] Sprite check;
    [SerializeField] Sprite circulo;
    [SerializeField] GameObject joystick;
    [SerializeField] GameObject panel;
    [SerializeField] List<string> ingredientes;
    [SerializeField] List<Material> materialesCafe;
    [SerializeField] AudioSource audio;




    private Transform point;
    private Transform taza;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AbrirInventario(Transform _point,Transform _taza)
    {
        taza = _taza;
        point = _point;
        joystick.SetActive(false);
        panel.SetActive(true);
        audio.Play();
        Time.timeScale = 0f;
    }

    public void ClickElemento(string ingrediente)
    {
        if(taza != null)
        {
            if (!ingredientes.Contains(taza.GetComponent<GenericObject>().id))
            {
                switch (ingrediente)
                {
                    case "Expresso":
                        taza.GetComponent<GenericObject>().id = "Expresso";
                        taza.GetChild(0).gameObject.GetComponent<Renderer>().material =
                            BuscarMaterial("Expresso", materialesCafe);
                        break;
                    case "Latte":
                        taza.GetComponent<GenericObject>().id = "Latte";
                        taza.GetChild(0).gameObject.GetComponent<Renderer>().material =
                            BuscarMaterial("Latte", materialesCafe);
                        break;
                    case "Americano":
                        taza.GetComponent<GenericObject>().id = "Americano";
                        taza.GetChild(0).gameObject.GetComponent<Renderer>().material =
                            BuscarMaterial("Americano", materialesCafe);
                        break;
                    case "Moca":
                        taza.GetComponent<GenericObject>().id = "Moca";
                        taza.GetChild(0).gameObject.GetComponent<Renderer>().material =
                            BuscarMaterial("Moca", materialesCafe);
                        break;
                }
                StartCoroutine(PrepararCafe(tiempoEspera, taza));
                joystick.SetActive(true);
                Time.timeScale = 1f;
                panel.SetActive(false);
            }            
        }        
    }

    private Material BuscarMaterial(string cafe, List<Material> materiales)
    {
        for (int i = 0; i < materiales.Count; i++)
        {
            if(materiales[i].name == cafe)
            {
                return materiales[i];
            }
        }
        return null;
    }

    public bool SacarTaza(Transform player)
    {
        bool ok;
        if(this.transform.childCount != 0)
        {
            if (tazaLista)
            {
                transform.GetChild(0).GetChild(0).transform.position = player.position;
                transform.GetChild(0).GetChild(0).transform.SetParent(player);
                ok = true;
                temporizador.sprite = circulo;
                temporizador.gameObject.SetActive(false);
                audio.Stop();
                tazaLista = false;
            }
            else
            {
                Debug.Log("La taza no esta lista");
                ok = false;
            }            
        }
        else
        {
            ok = false;
        }
        return ok;
    }

    public bool SacarTaza(Transform player, Transform poss)
    {
        bool ok;
        if (this.transform.childCount != 0)
        {
            Debug.Log(player.gameObject.name);
            if (tazaLista)
            {
                transform.GetChild(0).GetChild(0).transform.position = poss.position;
                transform.GetChild(0).GetChild(0).transform.SetParent(player);
                ok = true;
                temporizador.sprite = circulo;
                temporizador.gameObject.SetActive(false);
                audio.Stop();
                tazaLista = false;
            }
            else
            {
                Debug.Log("La taza no esta lista");
                ok = false;
            }
        }
        else
        {
            ok = false;
        }
        return ok;
    }

    private IEnumerator PrepararCafe(float tiempoEspera, Transform taza)
    {
        StartCoroutine(Temporizador(0f,tiempoEspera,temporizador,taza));
        taza.SetParent(punto);
        taza.localPosition = Vector3.zero;
        yield return new WaitForSeconds(tiempoEspera);
        tazaLista = true;        
    }

    private IEnumerator Temporizador(float tiempo, float tiempoEspera, Image temporizador, Transform taza)
    {
        temporizador.gameObject.SetActive(true);
        if (tiempo < tiempoEspera)
        {
            tiempo += Time.deltaTime;
            temporizador.fillAmount = (tiempo / tiempoEspera);
            if (tiempo > (tiempoEspera - 2.5f))
                taza.GetComponent<Animator>().SetBool("Preparando", true);
        }
        yield return new WaitForEndOfFrame();
        if (tiempo < tiempoEspera)
        {
            StartCoroutine(Temporizador(tiempo, tiempoEspera, temporizador, taza));
        }
        else
        {
            temporizador.sprite = check;
        }
    }
}
