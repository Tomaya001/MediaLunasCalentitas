using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;

public class BarcodeCam: MonoBehaviour
{

    // Almacenamiento de texturas de código QR
    private Texture2D encoded;

    // El código QR que se muestra en la interfaz de usuario
    // [Información sobre herramientas("Poner una imagen de imagen en bruto")]
    public RawImage codeimage;

    // Identifica la cámara utilizada por el código QR
    private WebCamTexture camTexture;

    // hilo
    private Thread qrThread;

    //color 
    private Color32[] c;
    // Ancho Altura
    private int W, H;

    // El rectángulo de la pantalla
    private Rect screenRect;

    // Si salir
    private bool isQuit;

    //El resultado final
    //[Información sobre herramientas("Detectar URL aquí, no es necesario completar")]
    public string LastResult;

    // Puede codificar
    private bool shouldEncodeNow;

    // ¿Se puede usar
    private bool Isuse = false;

    void OnGUI()
    {
        GUI.DrawTexture(screenRect, camTexture, ScaleMode.ScaleToFit);
    }

    #region Este método se utiliza para identificar
    /// <summary>
    /// Este método se usa para identificar
    /// </summary>
    void OnEnable()
    {
        if (camTexture != null)
        {
            //     camTexture.Play();
            W = camTexture.width;
            H = camTexture.height;
        }
    }

    void OnDisable()
    {
        if (camTexture != null)
        {
            camTexture.Pause();
        }
    }

    void OnDestroy()
    {
        if (qrThread != null)
        {
            qrThread.Abort();
        }

        if (camTexture != null)
        {
            camTexture.Stop();
        }
    }
    #endregion

    /// <summary>
    /// Salida del programa
    /// </summary>
    void OnApplicationQuit()
    {
        isQuit = true;
    }

    /// <summary>
    /// actualizar
    /// </summary>
    void Update()
    {
        // encode the last found
        // usar o no
        if (Isuse)
        {
            // Obtener la última url
            var textForEncoding = LastResult;

            // Se puede decodificar y el texto no está vacío
            if (shouldEncodeNow && textForEncoding != null)
            {
                // Obtenga el código QR después de la producción
                var color32 = Encode(textForEncoding, encoded.width, encoded.height);
                // Configuración de píxeles
                encoded.SetPixels32(color32);
                //Solicitud
                encoded.Apply();
                // Asignación de imagen de código QR
                codeimage.GetComponent<RawImage>().texture = encoded;
                // No es necesario analizar de nuevo
                shouldEncodeNow = false;
            }
        }
    }

    void DecodeQR()
    {
        // Crea un brillo de fuente de lector personalizado
        var barcodeReader = new BarcodeReader { AutoRotate = false, TryHarder = false };

        while (true)
        {
            if (isQuit)
                break;
            try
            {
                // Decodifica el marco actual
                var result = barcodeReader.Decode(c, W, H);
                // Juzga si actualmente está vacío
                if (result != null)
                {
                    // Obtener el texto del resultado
                    LastResult = result.Text;
                    // Capaz de mostrar
                    shouldEncodeNow = true;
                    // URL
                    //print(result.Text);
                }

                // Duerme un poco y configura la señal para obtener el siguiente cuadro
                Thread.Sleep(200);
                c = null;
            }
            catch
            {
            }
        }
    }

    /// <summary>
    /// Hacer un código QR
    /// </summary>
    /// <param name="textForEncoding"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <returns></returns>
    private static Color32[] Encode(string textForEncoding, int width, int height)
    {
        var writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width
            }
        };
        return writer.Write(textForEncoding);
    }

    /// <summary>
    /// Cambiar la última url
    /// </summary>
    /// <param name="str"></param>
    public void ChangeLastResult(string str)
    {
        // Se puede usar como verdadero
        Isuse = true;
        // Establecer la textura
        encoded = new Texture2D(256, 256);
        // Obtener la última url
        LastResult = str;
        // Puede decodificar ahora
        shouldEncodeNow = true;

        //screenRect = new Rect(0, 0, Screen.width, Screen.height);

        //camTexture = new WebCamTexture();
        //camTexture.requestedHeight = Screen.height; // 480;
        //camTexture.requestedWidth = Screen.width; //640;
        //OnEnable();

        // Abrir hilo
        qrThread = new Thread(DecodeQR);
        qrThread.Start();
    }
}

