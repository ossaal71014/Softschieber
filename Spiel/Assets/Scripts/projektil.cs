using System.Collections;
using UnityEngine;

public class projektil : MonoBehaviour
{
    public float speed = 2f;  //Variable für Projektilgeschwindigkeit
    public int schaden = 1;  //Schaden den das Projektil verursachen soll
    public GameObject explo;  //Projektil soll Explosion verursachen bei Kollison mit Gegner
    

    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);  //Das Projektil soll sich mit der Geschwindigkeit "speed" aufwärts bewegen.
    }

    /// <summary>
    /// Projektil verursacht eine Explosion bei der Gegnerkollision. Verursacht Schaden am Gegner und das Projektil zerstört sich selbst bei der Kollision.
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
    {
        

        if (other.CompareTag("Enemy"))  //Trifft das Projektil auf den Gegner?
        {

            Instantiate(explo, transform.position, Quaternion.identity);  //Erzeuge eine Explosion.

            Destroy(gameObject);  //Zerstöre dich selbst.

            other.SendMessage("Treffer", schaden, SendMessageOptions.DontRequireReceiver);  //Verursache Schaden am Gegner.
        }
        
        if (other.CompareTag("killzone"))  //Trifft das Projektil auf die Killzone (oberer Rand)?
        {
           
            Destroy(gameObject);  //Zerstöre dich selbst.

        }
    }

 

}
