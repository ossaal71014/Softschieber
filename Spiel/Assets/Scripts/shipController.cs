using System.Collections;
using UnityEngine;

public class shipController : MonoBehaviour
{
    public float maxSpeed = 3f;
    public bool analog = true;  // instantane Bewegung oder mit Beschleunigung
    private bool hitleft;
    private bool hitright;
    private bool hitup;
    private bool hitdown;
    private float xAchse = 0f;
    private float yAchse = 0f;
    private Animator anim;
    public ParticleSystem antrieb;
    public int istSchild = 3;  // Lebenspunkte des Schiffs
    public GameObject explo;  // Animation bei Zerstörung des Schiffs (Lebenspunkte des Schiffs fallen auf/unter null)
    public schild schildscript;  // Schildanimation bei Kollision mit Asteroiden 
    private GameObject Vatter;
    private GameLogic gLogic;
    private GUIScript gui;

    // wird vor Startfunktion einmalig ausgeführt
    void Awake()
    {
        anim = GetComponent<Animator>();  // Komponente vom Typ Animator
        Vatter = GameObject.FindGameObjectWithTag("MainCamera");
        gLogic = Vatter.GetComponent<GameLogic>();
        gui = Vatter.GetComponent<GUIScript>();

    }

    // Start is called before the first frame update
    void Start()
    {
        gui.shields = istSchild;
        transform.parent = Vatter.transform;
        transform.localPosition = new Vector3(0f, -6.5f, 10f);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (gLogic.startPhase)
        {
            if (transform.localPosition.y < -4f)
            {
                transform.Translate(Vector3.up * Time.deltaTime * 0.75f, Space.World);
            }
            else
            {
                gLogic.startPhase = false;
                hitdown = false;
            }
        }
        else
        {
            // Tastatur abfrage:

            if (!analog)
            {
                // Raw -> wenn über Nullpunkt hinaus -> 1:

                xAchse = Input.GetAxisRaw("Horizontal");
                yAchse = Input.GetAxisRaw("Vertical");
            }
            else
            {
                // Wert auf Achsen:

                xAchse = Input.GetAxis("Horizontal");
                yAchse = Input.GetAxis("Vertical");
            }

            // für Animation / wenn Input "rechts":

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

            // für Antrieb / Antrieb-Partikel -> länger sichbar wenn Beschleunigung nach vorne, wenn "rückwärts" -> kürzere Sichbarkeit der Partikel:

            if (Input.GetAxisRaw("Vertical") > 0f)
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


            // wenn innherhalb der Spielgrenzen:

            if (!hitleft && !hitright)
            {
                
                transform.Translate(Vector3.right * Time.deltaTime * maxSpeed * xAchse, Space.World);  // deltaTime -> Zeit zwischen letztem Frame und jetzt
            }
            else
            {
                // Bewegung nach rechts:

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


    }

    /// <summary>
    /// Treffer-Funktion zieht Schiff Leben ab bei Kollision mit Gegner 
    /// Bei Kollision wird Schild-Animation angezeigt
    /// </summary>
    /// <param name="schaden"></param>
    void Treffer(int schaden)
    {
        istSchild -= schaden;
        schildscript.schildAn = true;
        gui.shields = istSchild;
        if (istSchild < 0)
        {
            //Bei tödlichem Schaden soll Explosion an Ort des Schiffes durchgerührt werden und Schiff wird zerstört:

            Instantiate(explo, transform.position, Quaternion.identity);
            Destroy(gameObject);
            gui.shields = 0;
            gui.ships--;
            gLogic.todesPhase = true;
            if(gui.ships <= 0)
            {
                gLogic.gameOver = true;
            }
        }
    }
    
    /// <summary>
    /// Eingesammelte Items werden gezählt 
    /// </summary>
    void ShieldAdd()
    {
        istSchild++;
        gui.shields++;
    }
    void ShipAdd()
    {
        gui.ships++;
    }
    void Weapon1()
    {

    }
 
    /// <summary>
    /// Abfrage ob Collider getroffen (wenn Trigger berührt) 
    /// </summary>
    /// <param name="other"></param>
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
