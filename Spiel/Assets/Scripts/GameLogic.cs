using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public int stage = 1;                           //level
    public float todeszeit = 5f;                    //Zeit respawn
    private bool totWarte = false;                  //Variable für Coroutine
    public bool macheSpieler = true;                //Variable für Spielererzeugung
    public bool spielStart = true;                  //Variable für Grundeinstellung
    public bool startPhase = true;                  //Raumschiff fliegt beim Start rein
    public bool todesPhase = false;                 //Raumschifft fliegt bei Tod raus
    public bool gameOver = false;                   //GameOver 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(todesPhase && !totWarte)
        {
            StartCoroutine(Warte());
            totWarte = true;
        }
        
    }
    IEnumerator Warte()
    {
        yield return new WaitForSeconds(todeszeit);
        todesPhase = false;
        startPhase = true;
        totWarte = false;
        if (!gameOver)
        {
            macheSpieler = true;
        }
    }
}
