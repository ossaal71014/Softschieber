using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Erstellerscript : MonoBehaviour
{
    private GameLogic gLogic;
    public GameObject Schiffsprefab;
    public GameObject ShieldAddPrefab;
    public GameObject ShipAddPrefab;

    private Vector3 machPosi;       //Position der Items

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
        if (gLogic.makeShield)
        {
            gLogic.makeShield = false;
            machPosi = gLogic.hitPosition;
            Instantiate(ShieldAddPrefab, machPosi, Quaternion.identity);
        }
        if (gLogic.makeShip)
        {
            gLogic.makeShip = false;
            machPosi = gLogic.hitPosition;
            Instantiate(ShipAddPrefab, machPosi, Quaternion.identity);
        }
    }
}
