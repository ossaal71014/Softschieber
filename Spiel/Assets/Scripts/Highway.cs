using System.Collections;
using UnityEngine;

public class Highway : MonoBehaviour
{
    public bool vonLinks;
    public float speed = 2f;
    public bool jetzt;       
    public bool tot;
    

    // Wenn Gegner von links nach rechts fliegen soll, Bewegung in andere Richtung 
    void Start()
    {
        if (!vonLinks)
        {
            speed *= -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);
    }

    void Tod()
    {
        tot = true;
    }
}
