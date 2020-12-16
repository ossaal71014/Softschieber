using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public int stage = 1;
    public float todeszeit = 5f;
    private bool todWarte = false;
    public bool macheSpieler = true;
    public bool spielStart = true;
    public bool startPhase = true;
    public bool todesPhase = false;
    public bool gameOver = false; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Steuerung der Todesphase
        if(todesPhase && !todWarte)
        {
            StartCoroutine(Warte());
            todWarte = true; 
        }
    }
    IEnumerator Warte() 
    {
        yield return new WaitForSeconds(todeszeit);
        todesPhase = false;
        startPhase = true;
        todWarte = false;
        if (!gameOver)
        {
            macheSpieler = true; 
        }
    }
}

