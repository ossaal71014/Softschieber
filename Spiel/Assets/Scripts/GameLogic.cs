using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public AudioClip startMusik;
    public AudioClip totMusik;
    public ParticleSystemRenderer[] allstars;
    private float[] oldScale = new float[3];
    private float[] newScale = new float[3];
    public int stage = 1;
    public float todeszeit = 5f;
    private bool todWarte = false;
    public bool macheSpieler = true;
    public bool spielStart = true;
    public bool startPhase = true;
    public bool todesPhase = false;
    public bool gameOver = false;
    public float anzeigeZeit = 2f;
    public bool stageAnzeige = true;
    public bool anzeigeIstAn = false;

    public int score = 0;
    public int calcScore = 0;
    public int asteroidHit = 0;
    public int ship1Hit = 0;
    public int ship2Hit = 0;
    public int ship3Hit = 0;

    public Vector3 hitPosition;

    public int asterTotal = 0;
    public int Ship1Total = 0;
    public int Ship2Total = 0;
    public int Ship3Total = 0;

    public int extraShip = 1000;
    public int asterForShield = 10;

    public int ship1ForShield = 10;
    public int ship2ForShield = 10;
    public int ship3ForShield = 10;

    public bool makeShield;
    public bool makeShip;

    public bool stageWechsel;
    public int istStage = 0;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < allstars.Length; i++)
        {
            oldScale[i] = allstars[i].lengthScale;
        }
        newScale[0] = -25;
        newScale[1] = -15;
        newScale[2] = -10;
        makeShip = false;
        makeShield = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (spielStart)
        {
            macheSpieler = true;
            startPhase = true;
            stageAnzeige = true;

            score = 0;
            calcScore = 0;
            ship1Hit = 0;
            Ship1Total = 0;
            ship2Hit = 0;
            Ship2Total = 0;
            ship3Hit = 0;
            Ship3Total = 0;
            asteroidHit = 0;
            asterTotal = 0;
            stageWechsel = false;
            istStage = stage - 1;
        }
        if(score >= calcScore + extraShip)
        {
            makeShip = true;
            calcScore = score;
        }
        if(asteroidHit >= asterForShield)
        {
            makeShield = true;
            asterTotal += asteroidHit;
            asteroidHit = 0;
        }
        if(ship1Hit >= ship1ForShield)
        {
            Ship1Total += ship1Hit;
            ship1Hit = 0;
            makeShield = true;
        }
        if (ship2Hit >= ship2ForShield)
        {
            Ship2Total += ship2Hit;
            ship2Hit = 0;
            makeShield = true;
        }
        if (ship3Hit >= ship3ForShield)
        {
            Ship3Total += ship3Hit;
            ship3Hit = 0;
            makeShield = true;
        }

        //Stagewechsel
        if (stageWechsel)
        {
            stageWechsel = false;
            anzeigeIstAn = false;   //Stageanzeige neu starten
            stageAnzeige = true;
        }
        if (!todesPhase && !startPhase && stageAnzeige && !anzeigeIstAn)
        {
            GetComponent<AudioSource>().clip = startMusik;
            GetComponent<AudioSource>().Play();
            anzeigeIstAn = true;
            StartCoroutine(WarteAnzeige());
            istStage = stage;   //wenn Stageanzeige erscsheind sind wir im neuen Stage
            StartCoroutine("StageZeit");
        }

        //Steuerung der Todesphase
        if (todesPhase && !todWarte)
        {
            StopCoroutine("StageZeit");
            StartCoroutine(Warte());
            todWarte = true;
            GetComponent<AudioSource>().clip = totMusik;
            GetComponent<AudioSource>().Play();
        }
        if (todesPhase && allstars[0].lengthScale > newScale[0])
        {
            allstars[0].lengthScale -= Time.deltaTime * 10;
        }
        if (todesPhase && allstars[1].lengthScale > newScale[1])
        {
            allstars[1].lengthScale -= Time.deltaTime * 10;
        }
        if (todesPhase && allstars[2].lengthScale > newScale[2])
        {
            allstars[2].lengthScale -= Time.deltaTime * 10;
        }
        if (!todesPhase && allstars[0].lengthScale < oldScale[0])
        {
            allstars[0].lengthScale += Time.deltaTime * 7;
            if (allstars[0].lengthScale > oldScale[0])
            {
                allstars[0].lengthScale = oldScale[0];
            }
        }
        if (!todesPhase && allstars[1].lengthScale < oldScale[1])
        {
            allstars[1].lengthScale += Time.deltaTime * 7;
            if (allstars[1].lengthScale > oldScale[1])
            {
                allstars[1].lengthScale = oldScale[1];
            }
        }
        if (!todesPhase && allstars[2].lengthScale < oldScale[2])
        {
            allstars[2].lengthScale += Time.deltaTime * 7;
            if (allstars[2].lengthScale > oldScale[2])
            {
                allstars[2].lengthScale = oldScale[2];
            }
        }
    }
    IEnumerator StageZeit()
    {
        yield return new WaitForSeconds(75f); //Zeit die eine Stage andauert
        stageWechsel = true;
        stage++; 
    }
    IEnumerator Warte()
    {
        yield return new WaitForSeconds(todeszeit);
        todesPhase = false;
        startPhase = true;
        stageAnzeige = true;
        todWarte = false;
        if (!gameOver)
        {
            macheSpieler = true;
        }
    }

    IEnumerator WarteAnzeige()
    {
        yield return new WaitForSeconds(anzeigeZeit);
        stageAnzeige = false;
        anzeigeIstAn = false;
    }
}

