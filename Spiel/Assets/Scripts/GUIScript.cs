using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIScript : MonoBehaviour
{
    //Graphiken für das Banner 
    public GUISkin skin1;       
    public Texture2D shield;
    public Texture2D ship;
    public Texture2D background;

    private float homi;             //Position horizontal mittig
    public int score = 0;           //Punkteanzahl
    public int shields = 0;         //Schildanzahl
    public int ships = 0;           //Leben

    private string praefix = "";    //Darstellung Score mit Nullen davorgestellt z.B. 00018

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
}
