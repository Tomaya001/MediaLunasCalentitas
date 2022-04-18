﻿using com.baiba.GameManager;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;
using System;
using System.Text;
using System.IO;


public class QRGenerator : MonoBehaviour
{
    [SerializeField] BarcodeCam barcode;
    [SerializeField] RawImage qr;
    [SerializeField] Text mensaje;


    [SerializeField]
    string url = "El Usuario " + ConnectionManager.user + " consiguio un score de" + GameManager.Puntos + " puntos\n" +
        "Se ha ganado un descuento de 25% en un Americano\n";

    void Start()
    {
        if (PlayerPrefs.GetString("EscenaAnterior") == "YouLose")
        {
            GenerarQR();
            AlmacenarQR();
        }
        else
        {
            if (PlayerPrefs.HasKey("Descuento"))
            {
                AbrirQR();
            }
            else
            {
                qr.gameObject.SetActive(false);
                mensaje.gameObject.SetActive(true);
            }

        }
    }

    public void GenerarQR()
    {
        barcode.ChangeLastResult(url);
    }

    public void AlmacenarQR()
    {
        PlayerPrefs.SetString("Descuento", url);
    }

    public void AbrirQR()
    {
        url = PlayerPrefs.GetString("Descuento");
        barcode.ChangeLastResult(url);
    }

}
    
    


