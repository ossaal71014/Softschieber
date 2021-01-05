using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySchussGerade : MonoBehaviour
{
    public float speed = 2f;
    public int schaden = 1;
    private Vector3 richtung;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");        //Spüre Position des Spielers auf

        //Falls Spieler nicht gefunden wurde soll der Schuss sich gerade nach unten bewegen
        if (player == null)
        {
            richtung = -Vector3.up;
            speed = 5f;
        }
        else
        {
            richtung = -Vector3.up;             //Richtung des Schusses nach unten
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(richtung * Time.deltaTime * speed);                                 //Schuss fliegt
    }

    /// <summary>
    /// Methode zur Kollisionsermittlung und übergeben von Schaden an Spieler
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
    {
        //Ist getroffenens Objekt Spieler?
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);                                                                //Zerstöre Projektil
            other.SendMessage("Treffer", schaden, SendMessageOptions.DontRequireReceiver);      //Übermittle Schaden an Spieler
        }
        //Bewegt sich Projektil ausßerhalb der Spielzone?
        if (other.CompareTag("killzone"))
        {
            Destroy(gameObject);
        }
    }
}
