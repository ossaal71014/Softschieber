using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projektil : MonoBehaviour
{
    public float speed = 2f;                            //Variable für Projektilgeschwindigkeit
    public int schaden = 1;                             //Schaden den das Projektil verursacht

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Projektil soll sich mit Geschwindigkeit "speed" aufwärts bewegen
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }
}
