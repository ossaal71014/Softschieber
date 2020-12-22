using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyRLmoveScript : MonoBehaviour
{
    public bool vonLinks;
    public float speed = 2;
    public float schussrate = 0.8f;
    public float ersterSchuss = 1.5f;
    public GameObject Schuss;
    public bool jetzt;
    public bool tot;
    // Start is called before the first frame update
    void Start()
    {
        //Wenn enemy von rechts kommt, dann fliege auch nach links
        if (!vonLinks)
        {
            speed *= -1;
        }
        StartCoroutine(WarteSchuss(ersterSchuss));              //Fange erst nach ablauf der Zeit, angegeben von "ersterSchuss", an zu schießen
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
