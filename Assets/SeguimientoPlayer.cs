using com.baiba.core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguimientoPlayer : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(CONST.TAG.PLAYER);
        //this.transform.SetParent(player.transform);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = player.transform.position + new Vector3(0.6f, 7f, -7f);
    }
}
