using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projektil : MonoBehaviour
{
    public float speed = 2f;                            //Variable für Projektilgeschwindigkeit
    public int schaden = 1;                             //Schaden den das Projektil verursachen soll
    public int leben = 3;                               //Gegner sollen Leben erhalten
    public GameObject explo;                            //Projektil soll Explosion verursachen bei Kollison mit Gegner
    

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

    /// <summary>
    /// Projektil verursacht Explosion bei Gegnerkollision, verursacht Schaden am Gegner und Projektil zerstört sich selbst bei Kollision
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
    {
        //Trifft Projektil auf Gegner?
        if (other.CompareTag("Enemy"))
        {
            //Erzeuge Explosion
            Instantiate(explo, transform.position, Quaternion.identity);
            //Zerstöre dich selbst
            Destroy(gameObject);
            //Verursache Schaden am Gegner 
            other.SendMessage("Treffer", schaden, SendMessageOptions.DontRequireReceiver);
        }
    }

 

}
