using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Erstellerscript : MonoBehaviour
{
    private GameLogic gLogic;
    public GameObject Schiffsprefab;

    // Start is called before the first frame update
    void Awake()
    {
        gLogic = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameLogic>();

    }

    // Update is called once per frame
    void Update()
    {
        if(!gLogic.gameOver && gLogic.macheSpieler)
        {
            Instantiate(Schiffsprefab);
            gLogic.macheSpieler = false;
        }
    }
}
