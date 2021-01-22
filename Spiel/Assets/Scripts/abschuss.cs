using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abschuss : MonoBehaviour
{
    public Transform punkt2;
    public AudioClip typ1Sound;
    public float typ1Volume = 0.5f;

    public GameObject typ1;  //schuss1 (Projektil)
    public float schussrate = 0.4f;  // Grundwartezeit der Schüsse
    public float nachladezeit = 0.1f;  // Pause zwischen den Schüssen
    public int schussladung = 20;                        
    public int schussladungMax = 2;
    public bool jetzt = true;  // Variable zur Abfrage wann man schiessen darf
    private bool gestartet = false;
    private bool amLaden = false;
    private GameLogic gLogic;


    // Start is called before the first frame update
    void Awake()
    {
        gLogic = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        // Abfrage: Bei Tastendruck der zugeordneten Aktion "Fire1" (Maustaste Default) soll Projektil abgefeuert werden:

        if (Input.GetButton("Fire1") && jetzt && schussladung > 0 && !gLogic.anzeigeIstAn && !gLogic.startPhase)
        {
            //Schuss wird abgefeuert:
            
            Instantiate(typ1, transform.position, Quaternion.identity);
            Instantiate(typ1, punkt2.position, Quaternion.identity);
            jetzt = false;
            schussladung--;
            AudioSource.PlayClipAtPoint(typ1Sound, transform.position, typ1Volume);
        }

        //Abfrage für den Start der Wartezeit-Coroutine:

        if(!jetzt && !gestartet)
        {
            StartCoroutine(Wartezeit());
            gestartet = true; 
        }
        //Abfrage Status der Schussladung für den Start der Nachladen-Coroutine:

        if(schussladung < schussladungMax && !amLaden)
        {
            StartCoroutine(Nachladen());
            amLaden = true; 
        }
    }
    /// <summary>
    /// Lässt Schüsse im Abstand von "schussrate" zu
    /// </summary>
    IEnumerator Wartezeit()
    {
        yield return (new WaitForSeconds(schussrate));
        jetzt = true;
        gestartet = false; 
    }
    /// <summary>
    /// Wenn aktuelle Schüsse verbraucht sind, unterbricht Coroutine Feuer für "nachladezeit"
    /// </summary>
    IEnumerator Nachladen() 
    {
        yield return (new WaitForSeconds(nachladezeit));
        schussladung++; 
        amLaden = false;
    }

}
