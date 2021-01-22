using System.Collections;
using UnityEngine;

public class HighwayWerferSeite : MonoBehaviour
{
    public GameObject zivilist;
    private bool jetzt;
    private GameObject Vatter;
    private GameLogic gLogic;
    public float ersterWurf = 10f;
    public float wurfrate = 0.25f;
    public float endzeit = 99f;
    private bool beginn;
    private bool enden;  //coroutine fürs enden gestartet?
    private bool beendet;  // aktion beendet?

    // Als übergeordnetes Objekt wird "MainCamera" festgelegt, damit Werfer sich mitbewegt:

    void Awake()
    {
        Vatter = GameObject.FindGameObjectWithTag("MainCamera");
        gLogic = Vatter.GetComponent<GameLogic>();

    }
    // Start is called before the first frame update
    void Start()
    {
        jetzt = false;
        beginn = true;
        enden = false;
        beendet = false;
    }

    // Update is called once per frame
    void Update()
    {
        // erst Objekt erzeugen wenn Start-, Todesphase vorbei und Spieler nicht Gameover:

        if (!gLogic.startPhase && !gLogic.todesPhase && !gLogic.gameOver)
        {
            
            if (!enden) 
            {
                StartCoroutine(Beende(endzeit));
                enden = true;
            }

            // nur ersten Wurf machen, wenn aktuell aktiv:

            if (beginn && !beendet)
            {
                beginn = false;
                StartCoroutine(Warte(ersterWurf));
            }

            // tatsächliches Erzeugen des Schiffs an aktullem Ort / Corutine Warte erzeugt Schiff mit Rate:

            if (jetzt && !beendet)
            {
                Instantiate(zivilist, transform.position, Quaternion.identity);
                jetzt = false;
                StartCoroutine(Warte(1 / wurfrate));
            }
        }
        // wenn Spieler-Raumschiff gerade erzeugt wurde, alle Corutines beenden / Variablen ensprechend setzten:

        if (gLogic.startPhase)
        {
            StopAllCoroutines();
            beginn = true;
            jetzt = false;
            beendet = false;
            enden = false;
        }
    }
    void LateUpdate()
    {
        if (gLogic.gameOver || (gLogic.stage != gLogic.istStage && !gLogic.startPhase))
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Beendet das Erstellen von Zivilisten nach "et" Sekunden
    /// </summary>
    /// <param name="et"></param>
    /// <returns></returns>
    IEnumerator Beende(float et)    
    {
        yield return new WaitForSeconds(et);
        beendet = true;
    }

    
    IEnumerator Warte(float t)
    {
        yield return new WaitForSeconds(t);
        jetzt = true;
    }
}
