using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killdeath : MonoBehaviour
{
    public GameObject explo;
    public int damage = 1;                              //Variable für Schaden, der dem Schiff zugefügt werden soll
    public int leben = 3;                               //Gegner sollen Leben erhalten
    public int trefferPunkte = 1;                       //Punkte für einen Treffer
    public int killPunkte = 1;                          //Punkte wenn Genger zerstört wird
    private GUIScript gui;

    
    // Start is called before the first frame update
    void Awake()
    {
        gui = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GUIScript>();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // wenn in Trigger eingedrungen wird -> auswertung
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("killzone"))
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            // wenn gameObjekt auf Schiff trifft -> Explosion auslösen
            Instantiate(explo, transform.position, Quaternion.identity);
            //Schiff wird benachrichtigt Schaden zu erhalten egal, ob es einen Empfänger hat oder nicht
            other.SendMessage("Treffer", damage, SendMessageOptions.DontRequireReceiver);
            gui.score += killPunkte;
        }
    }

    /// <summary>
    /// Gegner erhalten Schaden und werden zerstört
    /// </summary>
    /// <param name="schaden"></param>
    void Treffer(int schaden)
    {
        leben -= schaden;
        gui.score += trefferPunkte;
        //Genügt Schaden um Gegner zu zerstören?
        if (leben <= 0)
        {
            gui.score += killPunkte;
            Destroy(gameObject);
            //Erzeuge Explosion
            Instantiate(explo, transform.position, Quaternion.identity);
        }
    }
}
