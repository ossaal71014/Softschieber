using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighwayRLmoveScript : MonoBehaviour
{
    public bool vonLinks;
    public float speed = 2;
    private bool jetzt;
    private bool tot;
    // Start is called before the first frame update
    void Start()
    {
        //Wenn enemy von rechts kommt, dann fliege auch nach links
        if (!vonLinks)
        {
            speed *= -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);       //Bewege Gegner

      
    }

    void Tot()
    {
        tot = true;
    }
}
