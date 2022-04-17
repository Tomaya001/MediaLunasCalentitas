using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoDestruirenStar : MonoBehaviour
{
    private static NoDestruirenStar instance = null;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
    }
}
