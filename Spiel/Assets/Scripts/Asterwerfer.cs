using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asterwerfer : MonoBehaviour
{
    public GameObject aster1;
    public GameObject aster2;
    private GameObject wer;
    private bool jetzt;
    private float zeit;
    private int typ;
    public float links;
    public float rechts;
    public float speed = 10f;
    private bool nachlinks;

    private GameObject Vatter;
    private GameLogic gLogic;


    // Start is called before the first frame update
    void Awake()
    {
        Vatter = GameObject.FindGameObjectWithTag("MainCamera");
        gLogic = Vatter.GetComponent<GameLogic>();
    }
    void Start()
    {
        transform.parent = Vatter.transform;
        transform.localPosition = new Vector3(0f, 6.5f, 11f);
    }

    // Update is called once per frame
    void Update()
    {
        if(!gLogic.startPhase && !gLogic.todesPhase)
        {
            if (!jetzt)
            {
                jetzt = true;
                // Zahl zw. 1 und 2
                typ = Random.Range(1, 3);
                if (typ == 1)
                {
                    wer = aster1;
                }
                else
                {
                    wer = aster2;
                }
                zeit = Random.Range(0.5f, 2.5f);
                StartCoroutine(Raus(wer, zeit));
            }

            if (nachlinks)
            {
                if (transform.localPosition.x > links)
                {
                    transform.Translate(-Vector3.right * Time.deltaTime * speed);
                }
                else
                {
                    nachlinks = false;
                }
            }
            else
            {
                if (transform.localPosition.x < rechts)
                {
                    transform.Translate(Vector3.right * Time.deltaTime * speed);
                }
                else
                {
                    nachlinks = true;
                }
            }
        }
        
    }
        IEnumerator Raus(GameObject go, float z)
        {
            // erst wenn z Sekunden abgelaufen
            yield return (new WaitForSeconds(z));
            // wird das abgearbeitet
            Instantiate(go, transform.position, Quaternion.identity);
            jetzt = false;
        }
    
}
