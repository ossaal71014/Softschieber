using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyWerferSeite : MonoBehaviour
{
    public GameObject enemy;
    private bool jetzt;
    private GameObject Vatter;
    private GameLogic gLogic;
    public float ersterWurf = 10f;
    public float wurfrate = 0.25f;
    private bool beginn;

    void Awake()
    {
        Vatter = GameObject.FindGameObjectWithTag("MainCamera");
        gLogic = Vatter.GetComponent<GameLogic>();

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!gLogic.startPhase && !gLogic.todesPhase)
        {
            if (beginn)
            {
                beginn = false;
                StartCoroutine(Warte(ersterWurf));
            }
            if (jetzt)
            {
                Instantiate(enemy, transform.position, Quaternion.identity);
                jetzt = false;
                StartCoroutine(Warte(1 / wurfrate));
            }
        }
        if (gLogic.startPhase)
        {
            beginn = true;
            jetzt = false;
        }
    }
    IEnumerator Warte(float t)
    {
        yield return new WaitForSeconds(t);
        jetzt = true;
    }
}
