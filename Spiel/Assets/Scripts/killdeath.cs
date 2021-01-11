﻿using System.Collections;
using UnityEngine;

public class killdeath : MonoBehaviour
{
    // AUKOMMENIETERTES "SOUND-ZEUG" und renderer/collider2D waren Versuche das hinzubekommen von Teil 7

    public AudioSource Hitaudio;
    public AudioSource Killaudio;
    public GameObject explo;
    public int damage = 1;                              //Variable für Schaden, der dem Schiff zugefügt werden soll
    public int leben = 3;                               //Gegner sollen Leben erhalten
    public int trefferPunkte = 1;
    public int killPunkte = 1;
    private GUIScript gui;
    private GameLogic gLogic;
    public enum sendTyp { asteroid, shiphorizontal, shipvertical, shipavoid};
    public sendTyp typ = sendTyp.asteroid;
    private bool vatterWeg = false;

    //public Renderer renderer;
    //public Collider2D collider2D;

    // Start is called before the first frame update
    void Awake()
    {
        gui = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GUIScript>();
        gLogic = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameLogic>();
        //renderer = renderer.GetComponent<Renderer>();
        //collider2D = collider2D.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gLogic.todesPhase && !vatterWeg)
        {
            vatterWeg = true;
            gameObject.SendMessage("Vatterlos", SendMessageOptions.DontRequireReceiver);
        }
    }
    void Sende()
    {
        gLogic.hitPosition = transform.position;
        switch (typ)
        {
            case sendTyp.asteroid:
                {
                    gLogic.asteroidHit++;
                    break;
                }
            case sendTyp.shiphorizontal:
                {
                    gLogic.ship1Hit++;
                    gameObject.SendMessage("Tot", SendMessageOptions.DontRequireReceiver);
                    break;
                }
            case sendTyp.shipvertical:
                {
                    gLogic.ship2Hit++;
                    gameObject.SendMessage("Tot", SendMessageOptions.DontRequireReceiver);
                    break;
                }
            case sendTyp.shipavoid:
                {
                    gLogic.ship3Hit++;
                    gameObject.SendMessage("Tot", SendMessageOptions.DontRequireReceiver);
                    break;
                }
        }
    }

    // wenn in Trigger eingedrungen wird -> auswertung
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("killzone"))
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("Player"))
        {
            //other.GetComponent<Renderer>().enabled = false;
            //other.GetComponent<Collider2D>().enabled = false;
            //renderer.enabled = false;
            //collider2D.enabled = false;
            // Zerstöre Obejkt erst nach 2f
            Destroy(gameObject, 2f);
            // wenn gameObjekt auf Schiff trifft -> Explosion auslösen
            Instantiate(explo, transform.position, Quaternion.identity);
            //Schiff wird benachrichtigt Schaden zu erhalten egal, ob es einen Empfänger hat oder nicht
            other.SendMessage("Treffer", damage, SendMessageOptions.DontRequireReceiver);
            gui.score += killPunkte;
            Killaudio.Play();
            Sende();
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
            //renderer.enabled = false;
            //collider2D.enabled = false;
            // Zerstöre Obejkt erst nach 2f
            Destroy(gameObject, 0.9f);
           
            
            //Erzeuge Explosion
            Instantiate(explo, transform.position, Quaternion.identity);
            Killaudio.Play();
            Sende();
        }
        else
        {
            if (!Hitaudio.isPlaying)
                Hitaudio.Pause();
        }
    }
}
