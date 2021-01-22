using System.Collections;
using UnityEngine;

public class killdeath : MonoBehaviour
{
    public AudioSource Hitaudio;
    public AudioSource Killaudio;
    public GameObject explo;
    public int damage = 1;  //Variable für Schaden, der dem Schiff zugefügt werden soll
    public int leben = 3;  //Gegner sollen Leben erhalten
    public int trefferPunkte = 1;
    public int killPunkte = 1;
    private GUIScript gui;
    private GameLogic gLogic;
    public enum SendTyp { asteroid, shiphorizontal, shipvertical, shipavoid};
    public SendTyp typ = SendTyp.asteroid;
    private bool vatterWeg = false;


    // Start is called before the first frame update
    void Awake()
    {
        gui = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GUIScript>();
        gLogic = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameLogic>();
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

        // : Unterscheide zwischen den Gegnertypen. :

        switch (typ)
        {
            case SendTyp.asteroid:
                {
                    gLogic.asteroidHit++;
                    break;
                }
            case SendTyp.shiphorizontal:
                {
                    gLogic.ship1Hit++;
                    gameObject.SendMessage("Tot", SendMessageOptions.DontRequireReceiver);
                    break;
                }
            case SendTyp.shipvertical:
                {
                    gLogic.ship2Hit++;
                    gameObject.SendMessage("Tot", SendMessageOptions.DontRequireReceiver);
                    break;
                }
            case SendTyp.shipavoid:
                {
                    gLogic.ship3Hit++;
                    gameObject.SendMessage("Tot", SendMessageOptions.DontRequireReceiver);
                    break;
                }
        }
    }

    // : Wenn in den Trigger eingedrungen wird erfolgt eine Auswertung. :

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("killzone"))
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("Player"))
        {          
            Destroy(gameObject, 2f);  //Zerstöre das Objekt, das mit dem Spieler kollidiert ist.
            
            Instantiate(explo, transform.position, Quaternion.identity);  //Wenn der Gegner auf Schiff trifft Explosion auslösen

            other.SendMessage("Treffer", damage, SendMessageOptions.DontRequireReceiver);  //Das Schiff wird benachrichtigt den Schaden zu erhalten.

            gui.score += killPunkte;  //Addiere die Killpunkte (Punkte für zerstörte Gegner) auf den Score des Spielers.

            Killaudio.Play();  //Spiele einen Ton ab bei der Zerstörung des Spielers.

            Sende();
        }
    }

    /// <summary>
    /// Die Gegner erhalten Schaden und werden zerstört
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
