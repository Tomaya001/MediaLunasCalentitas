﻿using com.baiba.core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CafeteraScripts : MonoBehaviour
{
    public Image temporizador;
    public float tiempoEspera;
    public Sprite check;
    public Sprite circulo;

    public Transform punto;
    public bool tazaLista;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivarCafetera(Transform taza)
    {
        StartCoroutine(PrepararCafe(tiempoEspera, taza));
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