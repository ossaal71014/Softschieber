using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GUIScript : MonoBehaviour
{
    //Graphiken für das Banner 
    public GUISkin skin1;
    public GUISkin skin2;
    public GUISkin skin3;
    public GUISkin tab1Skin;
    public GUISkin tab1aSkin;
    public GUISkin tab2Skin;
    public GUISkin tab2aSkin;

    public Texture2D shieldTex;
    public Texture2D shipTex;
    public Texture2D backgroundTex;

    private float homi;             //Position horizontal mittig
    private float vemi;
    public int score = 0;           //Punkteanzahl
    public int shields = 0;         //Schildanzahl
    public int ships = 0;           //Leben

    private string praefix = "";    //Darstellung Score mit Nullen davorgestellt z.B. 00018

    private GameLogic gLogic;

    private bool goAnzeige;             // game over anzeige
    public bool wartemal;               // für game over anzeige
    private bool namenseingabe;         // fü namenseingabe wenn im highscore
    private bool highscore;             // für wechsel von highscoreanzeige zu titel
    private bool neustarthigh = true;   // timer für highscore neu starten
    public float highZeit = 6.5f;       // highscore anzeigezeit
    private bool hinweis;               // für hinweisanzeige
    private bool neustarthinweis = true;// timer für hinweis neu starten
    public float hinweisZeit = 1f;      // hinweiszeit

    // Start is called before the first frame update
    void Awake()
    {
        gLogic = gameObject.GetComponent<GameLogic>();

    }

    private void Start()
    {
        namenseingabe = false;
        wartemal = false;
        goAnzeige = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gLogic.spielStart)
        {
            highscore = false;
            neustarthigh = true;
            neustarthinweis = true;
            score = 0;
            ships = 3;
        }
        gLogic.score = score;
        homi = Screen.width / 2;
        vemi = Screen.height / 2;
        praefix = "00000";

        // Nullen werden davorgestellt wenn Wert 1-stellig, 2-stellig usw. ist 
        if (score > 9)
        {
            praefix = "0000";
        }
        if (score > 99)
        {
            praefix = "000";
        }
        if (score > 999)
        {
            praefix = "00";
        }
        if (score > 9999)
        {
            praefix = "0";
        }
        if (score > 99999)
        {
            praefix = "";
        }
    }
    void LateUpdate()
    {
        gLogic.spielStart = false;
    }
    //Einzeichnen der Grafik
    //(newRect(x,y,w,h), Texture2D) => zeichnet Textur "Texture2D" an Position x,y mit Breite w und Höhe h
    void OnGUI()
    {
        GUI.skin = skin1;
        GUI.DrawTexture(new Rect(homi - 200, 0, 400, 50), backgroundTex);
        GUI.Label(new Rect(homi - 190, 10, 32, 32), shipTex);
        GUI.Label(new Rect(homi - 150, 13, 50, 50), "" + ships);
        GUI.Label(new Rect(homi - 100, 8, 32, 32), shieldTex);
        GUI.Label(new Rect(homi - 60, 13, 50, 50), "" + shields);
        GUI.Label(new Rect(homi, 13, 200, 50), "score: " + praefix + score);

        if (gLogic.gameOver)
        {
            if (wartemal) 
            {
                StartCoroutine(Warte());
                goAnzeige = true;
                wartemal = false;
            }

            if (goAnzeige) // anzeige von gameover
            {
                GUI.skin = skin2;
                GUI.Label(new Rect(homi - 120, vemi - 50, 300, 100), "game over");
            }

            if (namenseingabe) // wenn unter den besten x, dann namen eingeben
            { 

            }

            if (!goAnzeige && !namenseingabe) 
            {
                if(neustarthinweis)             // starte den hinweis neu
                {
                    neustarthinweis = false;
                    StartCoroutine(HinweisTimer());
                }
                if(hinweis)
                {
                  //  GUI.skin = tab1aSkin;
                    GUI.Label(new Rect(homi - 120, vemi * 2 - 50, 300, 30), "press fire to start");
                }
                if(Input.GetButton("Fire1"))
                {
                    gLogic.gameOver = false;
                    gLogic.spielStart = true;
                    wartemal = true;
                }
            }
        }

        GUI.skin = skin2;
        //if (gLogic.gameOver)
        //{
        //    GUI.Label(new Rect(homi - 120, Screen.height/2 -50, 300, 100), "game over");
        //}
        if (gLogic.stageAnzeige && gLogic.anzeigeIstAn)
        {
            GUI.Label(new Rect(homi - 80, vemi - 50, 300, 100), "stage "+gLogic.stage);
        }
    }

    IEnumerator HinweisTimer()
    {
        yield return new WaitForSeconds(hinweisZeit);
        hinweis = !hinweis;
        neustarthinweis = true;
    }

    IEnumerator Warte()
    {
        yield return new WaitForSeconds(7f);
        goAnzeige = false;
    }
}
