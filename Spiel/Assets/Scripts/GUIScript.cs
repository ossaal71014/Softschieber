using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIScript : MonoBehaviour
{
    //Graphiken für das Banner 
    public GUISkin skin1;
    public GUISkin skin2;
    public Texture2D shield;
    public Texture2D ship;
    public Texture2D background;

    private float homi;             //Position horizontal mittig
    public int score = 0;           //Punkteanzahl
    public int shields = 0;         //Schildanzahl
    public int ships = 0;           //Leben

    private string praefix = "";    //Darstellung Score mit Nullen davorgestellt z.B. 00018

    private GameLogic gLogic;       //Verweis zum script GameLogic

    // Start is called before the first frame update
    void Awake()
    {
        gLogic = gameObject.GetComponent<GameLogic>();  
    }

    // Update is called once per frame
    void Update()
    {
        if (gLogic.spielStart)
        {
            score = 0;
            ships = 3;
            gLogic.spielStart = false;

        }
        homi = Screen.width / 2;
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
    //Einzeichnen der Grafik
    //(newRect(x,y,w,h), Texture2D) => zeichnet Textur "Texture2D" an Position x,y mit Breite w und Höhe h
    void OnGUI()
    {
        GUI.skin = skin1;
        GUI.DrawTexture(new Rect(homi - 200, 0, 400, 50), background);
        GUI.Label(new Rect(homi - 190, 10, 32, 32), ship);
        GUI.Label(new Rect(homi - 150, 13, 50, 50), "" + ships);
        GUI.Label(new Rect(homi - 100, 8, 32, 32), shield);
        GUI.Label(new Rect(homi - 60, 13, 50, 50), "" + shields);
        GUI.Label(new Rect(homi, 13, 200, 50), "score: " + praefix + score);

        GUI.skin = skin2;
        if (gLogic.gameOver)
        {
            GUI.Label(new Rect(homi - 150, Screen.height / 2 - 50, 300, 100), "game over");
        }

    }
}
