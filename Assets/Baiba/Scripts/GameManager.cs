using com.baiba.core.lang;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        Language lang = Language.instance;
        lang.Init("es");
    }
}
