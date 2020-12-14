using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class shipController : MonoBehaviour
{
    public float maxSpeed = 2f;
    // instantane Bewegung oder mit Beschleunigung
    public bool analog = true;

    // Rand berührt
    private bool hitleft;
    private bool hitright;
    private bool hitup;
    private bool hitdown;

    private float xAchse = 0f;
    private float yAchse = 0f;

    private Animator anim;

    public ParticleSystem antrieb;

    public int istSchild = 3;                           //Lebenspunkte des Schiffs
    public GameObject explo;                        //Animation bei Zerstörung des Schiffs (Lebenspunkte des Schiffs fallen auf/unter null)

    public schild schildscript;                     //Schildanimation bei Kollision mit Asteroiden 

    // wird vor Startfunktion einmalig ausgeführt
    void Awake()
    {
        // Komponente vom Tyn Animator
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {   
        // Tastatur abfrage
        if(!analog)
        {
            // Raw -> wenn über Nullpunkt hinaus -> 1
            xAchse = Input.GetAxisRaw("Horizontal");
            yAchse = Input.GetAxisRaw("Vertical");
        }
        else
        {
            // Wert auf Achsen
            xAchse = Input.GetAxis("Horizontal");
            yAchse = Input.GetAxis("Vertical");
        }

        // für Animation
        // wenn Input "rechts"
        if (Input.GetAxisRaw("Horizontal") > 0f)
        {
            anim.SetBool("rechts", true); 
        }
        else
        {
            anim.SetBool("rechts", false);
        }

        if (Input.GetAxisRaw("Horizontal") < 0f)
        {
            anim.SetBool("links", true);
        }
        else
        {
            anim.SetBool("links", false);
        }

        // für Antrieb
        // Antrieb-Partikel -> länger sichbar wenn Beschleunigung nach vorne, wenn "rückwärts" -> kürzere Sichbarkeit der Partikel
        if(Input.GetAxisRaw("Vertical") > 0f)
        {
            antrieb.startLifetime = 0.45f;
        }
        else
        {
            antrieb.startLifetime = 0.25f;
        }

        if (Input.GetAxisRaw("Vertical") < 0f)
        {
            antrieb.startLifetime = 0.07f;
        }
       

        // wenn innherhalb der Spielgrenzen
        if (!hitleft && !hitright)
        {
            // deltaTime -> Zeit zwischen letztem Frame und jetzt
            transform.Translate(Vector3.right * Time.deltaTime * maxSpeed * xAchse, Space.World);
        }
        else
        {   
            // Bewegung nach rechts 
            if (hitleft && xAchse > 0f)
            {
                hitleft = false;
            }
            if (hitright && xAchse < 0f)
            {
                hitright = false;
            }
        }

        if (!hitup && !hitdown)
        {
            transform.Translate(Vector3.up * Time.deltaTime * maxSpeed * yAchse, Space.World);
        }
        else
        {
            if (hitup && yAchse < 0f)
            {
                hitup = false;
            }
            if (hitdown && yAchse > 0f)
            {
                hitdown = false;
            }
        }
    }

    //Treffer-Funktion zieht Schiff leben ab bei Kollision mit Gegner 
    //Bei Kollision wird Schild-Animation angezeigt
    void Treffer(int schaden)
    {
        istSchild -= schaden;
        schildscript.schildAn = true; 

        if (istSchild <= 0)
        {
            //Bei tödlichem Schaden soll Explosion an Ort des Schiffes durchgerührt werden und Schiff wird zerstört
            Instantiate(explo, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    // Abfrage ob Collider getroffen (wenn Trigger berührt)
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.CompareTag("right"))
        {
            hitright = true;
        }
        if (other.CompareTag("left"))
        {
            hitleft = true;
        }
        if (other.CompareTag("up"))
        {
            hitup = true;
        }
        if (other.CompareTag("down"))
        {
            hitdown = true;
        }
    }
}
