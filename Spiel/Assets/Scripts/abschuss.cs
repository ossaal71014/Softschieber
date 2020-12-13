using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abschuss : MonoBehaviour
{
    
    public GameObject typ1;                             //schuss1 (Projektil)
    public float schussrate = 0.1f;                     // Grundwartezeit der Schüsse
    public float nachladezeit = 0.4f;                   // Pause zwischen den Schüssen
    public int schussladung = 2;                        
    public int schussladungMax = 2;
    public bool jetzt = true;                           //Variable zur Abfrage wann man schiessen darf
    private bool gestartet = false;
    private bool amLaden = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Abfrage: Bei Tastendruck der zugeordneten Aktion "Fire1" (Maustaste Default) soll Projektil abgefeuert werden
        if (Input.GetButton("Fire1") && jetzt && schussladung>0)
        {
            //Schuss wird abgefeuert
            Instantiate(typ1, transform.position, Quaternion.identity);
            jetzt = false;
            schussladung--;
        }
        //Abfrage für den Start der Wartezeit-Coroutine
        if(!jetzt && !gestartet)
        {
            StartCoroutine(Wartezeit());
            gestartet = true; 
        }
        //Abfrage Status der Schussladung für den Start der Nachladen-Coroutine
        if(schussladung<schussladungMax && !amLaden)
        {
            StartCoroutine(Nachladen());
            amLaden = true; 
        }
    }
    IEnumerator Wartezeit()
    {
        yield return (new WaitForSeconds(schussrate));
        jetzt = true;
        gestartet = false; 
    }
    IEnumerator Nachladen() 
    {
        yield return (new WaitForSeconds(nachladezeit));
        schussladung++; 
        amLaden = false;
    }

}
