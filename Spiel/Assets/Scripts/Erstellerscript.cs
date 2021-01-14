using System.Collections;
using UnityEngine;

public class Erstellerscript : MonoBehaviour
{
    private GameObject Vatter;
    private GameLogic gLogic;
    public GameObject Schiffsprefab;
    public GameObject ShieldAddPrefab;
    public GameObject ShipAddPrefab;

    public GameObject asteroidWerfer;
    public GameObject EnemyWerferR;
    public GameObject EnemyWerferL;
    public GameObject EnemyWerferAusweich;
    public GameObject EnemyWerferLooping;

    public GameObject HighwayWerferR;
    public GameObject HighwayWerferL;

    public int istStage = 0;

    private Vector3 machPosi;       //Position der Items

    // Start is called before the first frame update
    void Awake()
    {
        Vatter = GameObject.FindGameObjectWithTag("MainCamera");
        gLogic = Vatter.GetComponent<GameLogic>();

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
            machPosi = transform.position;
            Instantiate(ShipAddPrefab, machPosi, Quaternion.identity);
        }
        if (gLogic.gameOver)
        {
            istStage = 0; //reset damit es anfängt
            gLogic.stage = 1;       //////////////
        }


        
        if(gLogic.stage != istStage && !gLogic.gameOver && !gLogic.startPhase)
        {
            switch (gLogic.stage)
            {
                //Level 1
                case 1:
                    //instanzieren und werte übergeben
                    {
                        Asteroiden(0f, 30f, 2.5f); //startzeit, endzeit, wurfrate
                        Asteroiden(40f, 55f, 1.5f);
                        SchiffLinks(10f, 25f, 0.25f);
                        SchiffRechts(25f, 40f, 0.4f);
                        SchiffLinks(50f, 67f, 0.5f);
                        SchiffRechts(52f, 69f, 0.65f);
                        ZivilistenLinks(0f, 60f, 0.05f);
                        ZivilistenRechts(25f, 60f, 0.05f);
                        break;
                    }
                    //Level 2
                case 2:
                    //instanzieren und werte übergeben
                    {
                        Asteroiden(0f, 20f, 5f); //startzeit, endzeit, wurfrate
                        Asteroiden(30f, 60f, 0.5f);
                        SchiffLinks(10f, 30f, 0.25f);
                        SchiffRechts(30f, 40f, 0.3f);
                        SchiffLinks(50f, 65f, 0.5f);
                        SchiffRechts(52f, 67f, 0.65f);
                        ZivilistenLinks(0f, 60f, 0.25f);
                        ZivilistenRechts(25f, 60f, 0.4f);
                        break;
                    }
                    //Level 3
                case 3:
                    {
                        SchiffAusweich(0f, 20f, 0.5f, 1); //startzeit, endzeit, rate, zyklus
                        SchiffAusweich(27f, 47f, 1.5f, 2);
                        SchiffAusweich(50f, 70f, 2f, 3);
                        ZivilistenLinks(0f, 60f, 0.05f);
                        ZivilistenRechts(25f, 60f, 0.05f);
                        break;
                    }
                    // Level 4
                case 4:
                    {
                        SchiffLooping(0f, 20f, 3f, 1); //startzeit, endzeit, rate, zyklus
                        SchiffLooping(27f, 47f, 1.5f, 2);
                        SchiffLooping(50f, 70f, 2f, 3);
                        ZivilistenLinks(0f, 60f, 0.05f);
                        ZivilistenRechts(25f, 60f, 0.05f);
                        break;
                    }

                default:
                    {
                        Asteroiden(0f, 70f, 2.5f); //startzeit, endzeit, wurfrate
                        SchiffLinks(10f, 60f, 0.2f);
                        SchiffRechts(30f, 60f, 0.2f);                       
                        SchiffAusweich(0f, 60f, 2f, 1); //startzeit, endzeit, rate, zyklus
                        SchiffLooping(0f, 60f, 2f, 1); //startzeit, endzeit, rate, zyklus  
                        ZivilistenLinks(0f, 70f, 0.05f);
                        ZivilistenRechts(25f, 60f, 0.05f);
                        break;
                    }
            }
            istStage = gLogic.stage;
        }
    }

    void Asteroiden(float start, float ende, float r)
    {
        GameObject aster = Instantiate(asteroidWerfer, transform.position, Quaternion.identity) as GameObject;
        Asterwerfer ascript = aster.GetComponent<Asterwerfer>();
        ascript.endzeit = ende;
        ascript.startzeit = start;
        ascript.wurfZeit = r;
    }

    void ZivilistenLinks(float start, float ende, float r)
    {
        Vector3 pos = new Vector3(Vatter.transform.position.x - 4f, Vatter.transform.position.y + 2.5f, Vatter.transform.position.z + 11f);
        GameObject zivL = Instantiate(HighwayWerferL, pos, Quaternion.identity) as GameObject;
        HighwayWerferSeite zlscript = zivL.GetComponent<HighwayWerferSeite>();
        zlscript.endzeit = ende;
        zlscript.ersterWurf = start;
        zlscript.wurfrate = r;
    }

    void ZivilistenRechts(float start, float ende, float r)
    {
        Vector3 pos = new Vector3(Vatter.transform.position.x + 4f, Vatter.transform.position.y + 2.5f, Vatter.transform.position.z + 11f);
        GameObject zivL = Instantiate(HighwayWerferR, pos, Quaternion.identity) as GameObject;
        HighwayWerferSeite zrscript = zivL.GetComponent<HighwayWerferSeite>();
        zrscript.endzeit = ende;
        zrscript.ersterWurf = start;
        zrscript.wurfrate = r;
    }

    void SchiffLinks(float start, float ende, float r)
    {
        Vector3 posi = new Vector3(Vatter.transform.position.x -4f, Vatter.transform.position.y+2.5f, Vatter.transform.position.z+11f);
        GameObject shipL = Instantiate(EnemyWerferL, posi, Quaternion.identity) as GameObject;
        enemyWerferSeite elscript = shipL.GetComponent<enemyWerferSeite>();
        elscript.endzeit = ende;
        elscript.ersterWurf = start;
        elscript.wurfrate = r;
    }
    void SchiffRechts(float start, float ende, float r)
    {
        Vector3 posi = new Vector3(Vatter.transform.position.x+4f, Vatter.transform.position.y+2.5f, Vatter.transform.position.z+11f);
        GameObject shipR = Instantiate(EnemyWerferR, posi, Quaternion.identity) as GameObject;
        enemyWerferSeite erscript = shipR.GetComponent<enemyWerferSeite>();
        erscript.endzeit = ende;
        erscript.ersterWurf = start;
        erscript.wurfrate = r;
    }
    void SchiffAusweich(float start, float ende, float r, int zyklus)
    {
        GameObject shipA = Instantiate(EnemyWerferAusweich, transform.position, Quaternion.identity) as GameObject;
        EnemyWerferOben5 eascript = shipA.GetComponent<EnemyWerferOben5>();
        eascript.endzeit = ende;
        eascript.startzeit = start;
        eascript.wurfZeit = r;
        eascript.sollzyklus = zyklus;
    }
    void SchiffLooping(float start, float ende, float r, int zyklus)
    {
        GameObject shipLoop = Instantiate(EnemyWerferLooping, transform.position, Quaternion.identity) as GameObject;
        EnemyWerferOben5 eloopscript = shipLoop.GetComponent<EnemyWerferOben5>();
        eloopscript.endzeit = ende;
        eloopscript.startzeit = start;
        eloopscript.wurfZeit = r;
        eloopscript.sollzyklus = zyklus;
    }
}
