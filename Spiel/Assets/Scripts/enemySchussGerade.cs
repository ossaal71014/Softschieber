using System.Collections;
using UnityEngine;

public class enemySchussGerade : MonoBehaviour
{
    public float speed = 2f;  // Schussgeschwindigkeit
    public int schaden = 1;  // Schaden
    private Vector3 richtung;  // Richutungsvektor
    private GameObject player;

    /// <summary>
    /// Methode zur Ermittllung des Players und Festlegung der Schussrichtung
    /// </summary>
    void Start()
    {
        //Spüre Position des Spielers auf:

        player = GameObject.FindGameObjectWithTag("Player");       

        //Falls Spieler nicht gefunden wurde soll der Schuss sich gerade nach unten bewegen:

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

    /// <summary>
    /// Methode zur Ausführung des Flugschusses
    /// </summary>
    void Update()
    {
        transform.Translate(richtung * Time.deltaTime * speed);                                
    }

    /// <summary>
    /// Methode zur Kollisionsermittlung und übergeben von Schaden an Spieler
    /// </summary>
    void OnTriggerEnter2D(Collider2D other)
    {
        //Abfrage ob Spieler getroffen ist:

        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);                                                                //Zerstöre Projektil
            other.SendMessage("Treffer", schaden, SendMessageOptions.DontRequireReceiver);      //Übermittle Schaden an Spieler
        }

        //Wenn das Projektil außerhalb des Spielfelds ist, dann wird das Objekt (Projektil) zerstört:

        if (other.CompareTag("killzone"))
        {
            Destroy(gameObject);
        }
    }
}
