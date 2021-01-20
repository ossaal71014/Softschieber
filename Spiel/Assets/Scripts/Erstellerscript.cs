using System.Collections;
using UnityEngine;

public class Erstellerscript : GameLogic
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
            gLogic.stage = 1;       
        }

        float time_pro_stage_1 = time_pro_stage * 0.4f; 
        float time_pro_stage_2 = time_pro_stage * 0.5f; 
        float time_pro_stage_3 = time_pro_stage * 0.7f; 
        float time_pro_stage_4 = time_pro_stage * 0.1f; 
        float time_pro_stage_5 = time_pro_stage * 0.66f;
        float time_pro_stage_6 = time_pro_stage * 0.9f; 
        float time_pro_stage_7 = time_pro_stage * 0.25f;
        float time_pro_stage_8 = time_pro_stage * 0.8f; 

        if (gLogic.stage != istStage && !gLogic.gameOver && !gLogic.startPhase)
        {
            switch (gLogic.stage)
            {
                //Level 1
                case 1:
                    //instanzieren und werte übergeben
                    {
                        Asteroiden(0f, time_pro_stage_1, 2.5f); //startzeit, endzeit, wurfrate
                        Asteroiden(time_pro_stage_1, time_pro_stage_3, 1.5f);
                        SchiffLinks(time_pro_stage_4, time_pro_stage_1, 0.25f);
                        SchiffRechts(time_pro_stage_1, time_pro_stage_2, 0.4f);
                        SchiffLinks(time_pro_stage_5, time_pro_stage_6, 0.5f);
                        SchiffRechts(time_pro_stage_5, time_pro_stage_6, 0.65f);
                        ZivilistenLinks(0f, time_pro_stage_3, 0.05f);
                        ZivilistenRechts(time_pro_stage_1, time_pro_stage_3, 0.05f);                        
                        break;
                    }
                    //Level 2
                case 2:
                    //instanzieren und werte übergeben
                    {
                        Asteroiden(0f, time_pro_stage_7, 5f); //startzeit, endzeit, wurfrate
                        Asteroiden(time_pro_stage_1, time_pro_stage_8, 0.5f);
                        SchiffLinks(time_pro_stage_4, time_pro_stage_1, 0.25f);
                        SchiffRechts(time_pro_stage_1, time_pro_stage_2, 0.3f);
                        SchiffLinks(time_pro_stage_5, time_pro_stage_6, 0.5f);
                        SchiffRechts(time_pro_stage_5, time_pro_stage_6, 0.65f);
                        ZivilistenLinks(0f, time_pro_stage_8, 0.25f);
                        ZivilistenRechts(25f, 60f, 0.4f);
                        break;
                    }
                    //Level 3
                case 3:
                    {
                        SchiffAusweich(0f, time_pro_stage_7, 0.5f, 1); //startzeit, endzeit, rate, zyklus
                        SchiffAusweich(time_pro_stage_1, time_pro_stage_5, 1.5f, 2);
                        SchiffAusweich(time_pro_stage_5, time_pro_stage_6, 2f, 3);
                        ZivilistenLinks(0f, time_pro_stage_8, 0.05f);
                        ZivilistenRechts(time_pro_stage_1, time_pro_stage_8, 0.05f);
                        break;
                    }
                    // Level 4
                case 4:
                    {
                        SchiffLooping(0f, time_pro_stage_7, 3f, 1); //startzeit, endzeit, rate, zyklus
                        SchiffLooping(time_pro_stage_1, time_pro_stage_5, 1.5f, 2);
                        SchiffLooping(time_pro_stage_5, time_pro_stage_6, 2f, 3);
                        ZivilistenLinks(0f, time_pro_stage_8, 0.05f);
                        ZivilistenRechts(time_pro_stage_1, time_pro_stage_8, 0.05f);
                        break;
                    }

                default:
                    {
                        Asteroiden(0f, time_pro_stage_6, 2.5f); //startzeit, endzeit, wurfrate
                        SchiffLinks(time_pro_stage_4, time_pro_stage_8, 0.2f);
                        SchiffRechts(time_pro_stage_1, time_pro_stage_8, 0.2f);
                        SchiffAusweich(0f, time_pro_stage_8, 2f, 1); //startzeit, endzeit, rate, zyklus
                        SchiffLooping(0f, time_pro_stage_8, 2f, 1); //startzeit, endzeit, rate, zyklus  
                        ZivilistenLinks(0f, time_pro_stage_6, 0.05f);
                        ZivilistenRechts(time_pro_stage_1, time_pro_stage_8, 0.05f);
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
