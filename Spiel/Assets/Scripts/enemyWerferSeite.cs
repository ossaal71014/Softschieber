using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyWerferSeite : MonoBehaviour
{
    public GameObject enemy;
    private bool jetzt;
    private GameObject Vatter;
    private GameLogic gLogic;
    public float ersterWurf = 10f;
    public float wurfrate = 0.25f;
    public float endzeit = 99f;
    private bool beginn;
    private bool enden; //coroutine fürs enden gestartet?
    private bool beendet; // aktion beendet?

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
        if(!gLogic.startPhase && !gLogic.todesPhase)
        {
            if (!enden) //sind wir am Beenden
            {
                StartCoroutine(Beende(endzeit));
                enden = true; 
            }
            if (beginn && ! beendet)
            {
                beginn = false;
                StartCoroutine(Warte(ersterWurf));
            }
            if (jetzt && !beendet)
            {
                Instantiate(enemy, transform.position, Quaternion.identity);
                jetzt = false;
                StartCoroutine(Warte(1 / wurfrate));
            }
        }
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
        if(gLogic.gameOver ||(gLogic.stage !=gLogic.istStage && !gLogic.startPhase))
        {
            Destroy(gameObject);
        }
    }
    IEnumerator Beende(float et)    //wann ist es vorbei?
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
