using System.Collections;
using UnityEngine;

public class EnemyWerferOben5 : MonoBehaviour
{
    public GameObject enemy;
    private GameObject Vatter;
    private GameLogic gLogic;
    private bool jetzt;
    private bool gestartet = false;  // ist die Coroutine gestartet
    private bool beendet = false;  // ist die Coroutine beendet
    private bool starten = false;   // ist die coroutine fürs starten schon am laufen?
    private bool enden = false;      // ist die coroutine fürs enden schon am laufen?
    public float startzeit = 0f;    // nach wieviel sek. soll gestartet werden?
    public float endzeit = 99f;     // nach wieviel sek. soll beendet werden?
    public float wurfZeit = 2.5f;   // in welcher rate sollen die asteroiden rauskommen?
    private Vector3 pos1;  // Vektoren für die Position der jeweiligen Zyklen
    private Vector3 pos2;
    private Vector3 pos3;
    private Vector3 pos4;
    private Vector3 pos5;

    public int[] zyklus1 = new int[5];  // je Auswurf binär
    public int[] zyklus2 = new int[5];
    public int[] zyklus3 = new int[5];
    public int[] zyklus4 = new int[5];
    public int[] zyklus5 = new int[5];

    public int sollzyklus = 1;  // Zyklus für die Erscheinung der Gegner
    private int zaehler = 0;  // Zähler für die die Zyklus Aufzählung
    private int wo;

    /// <summary>
    /// Gegner als Child der Kamera
    /// </summary>
    void Awake()
    {
        Vatter = GameObject.FindGameObjectWithTag("MainCamera");
        gLogic = Vatter.GetComponent<GameLogic>();
    }


    /// <summary>
    /// Methode um die Punkte aufzuzählen
    /// </summary>
    void Start()
    {
        transform.parent = Vatter.transform;
        transform.localPosition = new Vector3(0f, 6f, 11f);

        zaehler = 0;
    }
    /// <summary>
    /// Methode um die die Gegner zu generieren und nach den bestimmten Schemata aufzurufen
    /// </summary>
    void Update()
    {
        if(!gLogic.startPhase && !gLogic.todesPhase)
        {
            if (!starten)
            {
                StartCoroutine(Starte (startzeit));
                starten = true;
            }
            if (!enden)
            {
                StartCoroutine(Beende(endzeit));
                enden = true;
            }

            if(!jetzt && !beendet && gestartet)
            {
                jetzt = true;
                if (sollzyklus == 1) wo = zyklus1[zaehler];
                if (sollzyklus == 2) wo = zyklus2[zaehler];
                if (sollzyklus == 3) wo = zyklus3[zaehler];
                StartCoroutine(Raus(wurfZeit));
                zaehler++;
                if (zaehler > 4) zaehler = 0;
            }
        }
        else
        {
            //wir sind gerade am sterben oder am entstehen also alles auf anfang zurück:

            gestartet = false;
            beendet = false;
            starten = false;
            enden = false;
            jetzt = false;
            StopAllCoroutines();
            zaehler = 0;    
        }
    }
    /// <summary>
    /// Methode um das Schiff zu zerstören 
    /// </summary>
    void LateUpdate()
    {
        if (gLogic.gameOver || (gLogic.stage != gLogic.istStage && !gLogic.startPhase))
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Methode um zu starten
    /// </summary>
    IEnumerator Starte(float st)    
    {
        yield return new WaitForSeconds(st);
        gestartet = true; 
    }

    /// <summary>
    /// Methode um zu beenden
    /// </summary>
    IEnumerator Beende(float et)    
    {
        yield return new WaitForSeconds(et);
        beendet = true; 
    }

    /// <summary>
    /// Methode um die Schiffe in den angegeben Zyklen zu Starten
    /// </summary>
    IEnumerator Raus (float z)
    {
        pos1 = transform.position;
        pos2 = pos1;
        pos3 = pos1;
        pos4 = pos1;
        pos5 = pos1;

        pos1.x = -2f;
        pos2.x = -1f;
        pos4.x = 1f;
        pos5.x = 2f;

        BitArray example = new BitArray(new int[] { wo });

        if (example[0])
        {
            Instantiate(enemy, pos5, Quaternion.identity);
        }
        if (example[1])
        {
            Instantiate(enemy, pos4, Quaternion.identity);
        }
        if (example[2])
        {
            Instantiate(enemy, pos3, Quaternion.identity);
        }
        if (example[3])
        {
            Instantiate(enemy, pos2, Quaternion.identity);
        }
        if (example[4])
        {
            Instantiate(enemy, pos1, Quaternion.identity);
        }
        yield return new WaitForSeconds(z);

        jetzt = false; 
    }
}
