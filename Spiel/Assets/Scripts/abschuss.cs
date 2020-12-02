using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abschuss : MonoBehaviour
{
    public GameObject typ1;                             //schuss1 (Projektil)
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Abfrage: Bei Tastendruck der zugeordneten Aktion "Fire1" (Maustaste Default) soll Projektil abgefeuert werden
        if (Input.GetButtonDown("Fire1"))
        {
            //Schuss wird abgefeuert
            Instantiate(typ1, transform.position, Quaternion.identity);
        }
    }
}
