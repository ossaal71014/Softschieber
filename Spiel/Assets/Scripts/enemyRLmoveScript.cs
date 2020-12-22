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
    private bool jetzt;
    private bool tot;
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
        transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);       //Bewege Gegner

        //Ist Gegner nicht zerstört soll er nach bestimmter zeit schießen
        if (jetzt && !tot)
        {
            jetzt = false;
            //Gegner soll nur innerhalb des sichtbaren bereichs schießen
            if (transform.position.x > -3f && transform.position.x <3f)
            {

            }
        }
    }
    IEnumerator WarteSchuss(float z)
    {
        yield return new WaitForSeconds(z);
        jetzt = true;
    }
}
