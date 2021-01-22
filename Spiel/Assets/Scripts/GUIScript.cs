using System.Collections;
using UnityEngine;

public class GUIScript : MonoBehaviour
{
    // Graphiken für das Banner: 

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

    private float homi;  // Position horizontal mittig
    private float vemi;  // Position vertikal mittig
    public int score = 0;  // Punkteanzahl
    public int shields = 0;  // Schildanzahl
    public int ships = 0;  // Leben

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

    private string[] hiName = new string[11];   // array für 10 namen des highscores
    private int[] hiPunkte = new int[11];       // array für 10 punkte des highscores
    public string meinName = "";

    /// <summary>
    /// Bei Spielbeginn anzeigen
    /// </summary>
    void Awake()
    {
        gLogic = gameObject.GetComponent<GameLogic>();

    }

    /// <summary>
    /// Methode Highscore Tabelle anzuzeigen
    /// </summary>
    private void Start()
    {
        namenseingabe = false;
        wartemal = false;
        goAnzeige = false;

        // Highscore-Liste:

        if (!PlayerPrefs.HasKey("1Name"))                
        {
            PlayerPrefs.SetString("1Name", "fritz");
            PlayerPrefs.SetInt("1Score", 1000);
            PlayerPrefs.SetString("2Name", "tom");
            PlayerPrefs.SetInt("2Score", 900);
            PlayerPrefs.SetString("3Name", "carsten");
            PlayerPrefs.SetInt("3Score", 800);
            PlayerPrefs.SetString("4Name", "uli");
            PlayerPrefs.SetInt("4Score", 700);
            PlayerPrefs.SetString("5Name", "andreas");
            PlayerPrefs.SetInt("5Score", 600);
            PlayerPrefs.SetString("6Name", "serge");
            PlayerPrefs.SetInt("6Score", 500);
            PlayerPrefs.SetString("7Name", "nigel");
            PlayerPrefs.SetInt("7Score", 400);
            PlayerPrefs.SetString("8Name", "joseph");
            PlayerPrefs.SetInt("8Score", 300);
            PlayerPrefs.SetString("9Name", "bill");
            PlayerPrefs.SetInt("9Score", 200);
            PlayerPrefs.SetString("10Name", "maxe");
            PlayerPrefs.SetInt("10Score", 10);
            PlayerPrefs.Save();
        }
        for(int i=1; i<11; i++)
        {
            hiName[i] = PlayerPrefs.GetString(i+"Name");
            hiPunkte[i] = PlayerPrefs.GetInt(i+"Score");
        }
    }

    /// <summary>
    /// Methode um die Punkte aufzuzählen
    /// </summary>
    void Update()
    {
        if (gLogic.spielStart)
        {
            highscore = false;
            neustarthigh = true;
            neustarthinweis = true;
            score = 0;
            ships = 3;
            hiPunkte[0] = 20;
        }
        gLogic.score = score;
        homi = Screen.width / 2;
        vemi = Screen.height / 2;
        praefix = "00000";

        // Nullen werden davorgestellt wenn Wert 1-stellig, 2-stellig usw. ist:

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

    /// <summary>
    /// Einzeichnen der Grafik
    /// (newRect(x,y,w,h), Texture2D) => zeichnet Textur "Texture2D" an Position x,y mit Breite w und Höhe h
    /// </summary>
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

            if (namenseingabe)                                                                          // wenn unter den besten x, dann namen eingeben
            {
                GUI.skin = skin2;
                GUI.Label(new Rect(homi - 200, vemi - 190, 400, 220), "you have reached\na top 10\nhighscore !\n\nplease\nenter name");
                GUI.SetNextControlName("Eingabe");
                meinName = GUI.TextField(new Rect(homi - 100, vemi + 50, 200, 50), meinName, 10);       // max. 10 zeichen pro name
                meinName = meinName.ToLower();                                                          // nur kleinbuchstaben

                GUI.SetNextControlName("Button");
                if(GUI.Button(new Rect (homi -50, vemi + 120, 100, 50), "ok") || (Event.current.type == EventType.KeyUp && Event.current.keyCode == KeyCode.Return))
                {
                    namenseingabe = false;
                    int zaehler = 10;
                    while (zaehler > 0 && score > hiPunkte[zaehler])                                    // vgl. von letzten Highscoreplatz ausgehend mit aktuellen punkten
                    {
                        zaehler--;
                    }
                    for (int s = 10; s < zaehler+1; s--)
                    {
                        hiPunkte[s] = hiPunkte[s - 1];
                        hiName[s] = hiName[s - 1];
                    }
                    hiPunkte[zaehler + 1] = score;
                    hiName[zaehler + 1] = meinName;
                    hiPunkte[0] = zaehler + 1;
                    for (int n = 1; n < 11; n++)
                    {
                        PlayerPrefs.SetString(n+"Name", hiName[n]);
                        PlayerPrefs.SetInt(n+"Score", hiPunkte[n]);
                    }
                    PlayerPrefs.Save();
                }
                GUI.FocusControl("Eingabe");

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
                    GUI.Label(new Rect(homi - 105, vemi * 2 - 50, 300, 30), "'strg' -> starten");
                }
                if(Input.GetButton("Fire1"))
                {
                    gLogic.gameOver = false;
                    gLogic.spielStart = true;
                    wartemal = true;
                }
                if(neustarthigh)            // starte highscore counter neu
                {
                    neustarthigh = false;
                    StartCoroutine(HighscoreTimer());
                }
                if(highscore)
                {
                    GUI.BeginGroup(new Rect (homi - 185, vemi - 250, 370, 450));
                    GUI.skin = skin2;
                    GUI.Label(new Rect(30, 80, 300, 50), "highscores");
                    bool zeileGerade = false;           // um Zeilen abwechselnd hell dunkel zu machen      
                    int yy = 130;                       // Startwert erster Tabelle
                    for(int i=1; i<11; i++)
                    {
                        if (!zeileGerade)
                        {
                            GUI.skin = tab1Skin;
                            if (hiPunkte[0] == i)
                            {
                                GUI.skin = tab2Skin;
                            }
                        }
                        else
                        {
                            GUI.skin = tab1aSkin;
                            if (hiPunkte[i] == i)
                            {
                                GUI.skin = tab2aSkin;
                            }
                        }
                            GUI.Label(new Rect (0, yy, 50, 30), ""+i);
                            GUI.Label(new Rect (50, yy, 170, 30), hiName[i]);
                            GUI.Label(new Rect(220, yy, 150, 30), ""+hiPunkte[i]);
                            yy += 30;
                            zeileGerade = !zeileGerade;
                        }
                        GUI.EndGroup();
                }
                else
                {
                    GUI.skin = skin2;
                    GUI.Label(new Rect(homi - 140, vemi - 50, 300, 100), "se_shooter");
                }
            }
        }

        GUI.skin = skin2;
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
        if(score > hiPunkte[10])
        {
            namenseingabe = true;
            meinName = "";
        }
    }

    IEnumerator HighscoreTimer()
    {
        yield return new WaitForSeconds(highZeit);
        highscore = !highscore;
        neustarthigh = true;
    }
}
