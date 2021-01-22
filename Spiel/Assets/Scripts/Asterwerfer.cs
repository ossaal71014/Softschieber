using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asterwerfer : MonoBehaviour
{
    public GameObject aster1;
    public GameObject aster2;
    private GameObject wer;
    private bool jetzt;
    private float zeit;
    private int typ;
    public float links;
    public float rechts;
    public float speed = 10f;
    private bool nachlinks;

    private GameObject Vatter;
    private GameLogic gLogic;

    private bool gestartet = false;
    private bool beendet = false;
    private bool starten = false;
    private bool enden = false; 
    public float startzeit = 0f;
    public float endzeit = 99f;
    public float wurfZeit = 2.5f; 



    // Start is called before the first frame update
    /// <summary>
    /// Methode legt "Vatter" von Typ "MainCamera" aus Szene fest
    /// und erlaubt über "gLogic" den Zugriff auf Inhalte von GameLogic.cs
    /// </summary>
    void Awake()
    {
        Vatter = GameObject.FindGameObjectWithTag("MainCamera");
        gLogic = Vatter.GetComponent<GameLogic>();
    }

    void Start()
    {
        transform.parent = Vatter.transform;
        transform.localPosition = new Vector3(0f, 6.5f, 11f);
    }

    // Update is called once per frame
    void Update()
    {
        if(!gLogic.startPhase && !gLogic.todesPhase && !gLogic.gameOver)
        {
            if (!starten)
            {
                StartCoroutine(Starte (startzeit));
                starten = true;
            }
            if(!enden)
            {
                StartCoroutine(Beende (endzeit));
                enden = true; 
            }
            if (!jetzt && !beendet &&!gestartet)
            {
                jetzt = true;
                // Zahl zw. 1 und 2
                typ = Random.Range(1, 3);
                if (typ == 1)
                {
                    wer = aster1;
                }
                else
                {
                    wer = aster2;
                }
                zeit = Random.Range(2.5f, wurfZeit);
                StartCoroutine(Raus(wer, zeit));
            }

            if (nachlinks)
            {
                if (transform.localPosition.x > links)
                {
                    transform.Translate(-Vector3.right * Time.deltaTime * speed);
                }
                else
                {
                    nachlinks = false;
                }
            }
            else
            {
                if (transform.localPosition.x < rechts)
                {
                    transform.Translate(Vector3.right * Time.deltaTime * speed);
                }
                else
                {
                    nachlinks = true;
                }
            }
        }
        else
        {
            //gerade am Sterben oder am Enstehen, also alles auf Anfang zurück
            gestartet = false;
            beendet = false;
            starten = false;
            enden = false;
            jetzt = false;
            StopAllCoroutines();
        }
        
    }

    /// <summary>
    /// Wenn Stage geschafft oder der Spieler gameover ist, wird Asteroidenwerfer zerstört
    /// </summary>
    void LateUpdate()
    {
        if(gLogic.gameOver || (gLogic.stage != gLogic.istStage && !gLogic.startPhase)) 
        {
            Destroy(gameObject);  // wir zerstören uns
        }
    }
    /// <summary>
    /// Nach "st" Sekunden beginnt der Asteroidenwerfer mit dem Erzeugen von Asteroiden
    /// </summary>
    /// <param name="st"></param>
    IEnumerator Starte(float st)
    {
        yield return new WaitForSeconds(st);
        gestartet = true; 
    }

    /// <summary>
    /// Nach "et" Sekunden beendet der Asteroidenwerfer das Erzeugen von Asteroiden
    /// </summary>
    /// <param name="et"></param>
    IEnumerator Beende(float et)   
    {
        yield return new WaitForSeconds(et);
        beendet = true;
    }

    /// <summary>
    /// Nach jeweils "z" Sekunden wird Asteroid erzeugt
    /// </summary>
    /// <param name="go"></param>
    /// <param name="z"></param>
    IEnumerator Raus(GameObject go, float z)
        {
            yield return (new WaitForSeconds(z));
            Instantiate(go, transform.position, Quaternion.identity);
            jetzt = false;
        }
    
}
