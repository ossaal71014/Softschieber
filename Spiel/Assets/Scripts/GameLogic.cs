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
    }

    // Update is called once per frame
    void Update()
    {
        if (!todesPhase && !startPhase && stageAnzeige && !anzeigeIstAn)
        {
            GetComponent<AudioSource>().clip = startMusik;
            GetComponent<AudioSource>().Play();
            anzeigeIstAn = true;
            StartCoroutine(WarteAnzeige());
        }

        //Steuerung der Todesphase
        if (todesPhase && !todWarte)
        {
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

