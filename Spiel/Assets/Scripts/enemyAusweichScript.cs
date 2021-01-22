using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAusweichScript : MonoBehaviour
{
    public bool nachlinks;  // nach links ausweichen
    public bool nachrechts;  // nach rechts ausweichen
    public float maxAusweich = 2;  // max Ausweichgeschwindigkeit
    public float istAusweich = 0;  // momentane Ausweichgeschwindigkeit
    private Animator anim;
    public LayerMask layer;

    public float schussrate = 0.2f;  // Schussrate rotes Schiff
    public float ersterSchuss = 1.5f;  // ester Schuss nach 1.5 Sekunden
    public GameObject Schuss;
    private bool jetzt;  // Variable für aktuellen Stand
    private bool tot;  // Variable wenn tod

    /// <summary>
    /// Ruft den Start für des grünen Gegners auf
    /// </summary>
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// Lässt ersten Schuss des Gegners erst nach Zeit "ersterSchuss + varia" zu
    /// </summary>
    void Start()
    {
        float varia = Random.Range(0f, 0.5f);
        StartCoroutine(WarteSchuss(ersterSchuss + varia));
    }

    /// <summary>
    /// Startet das Gegnerschiff mit der festgelegten Schussrate und der Eigenschaft dem Player auszuweichen
    /// </summary>
    void Update()
    {
        if(jetzt && !tot)
        {
            jetzt = false; 

            // Wenn Gegner horizontal innerhalb von -3..3 ist, schießt er:

            if(transform.position.x > -3f && transform.position.x < 3f)
            {
                Instantiate(Schuss, transform.position, Quaternion.identity);
                float varia = Random.Range(0f, 0.2f);
                StartCoroutine(WarteSchuss(1/schussrate + varia));
            }
        }

        //ausweichen in die gegebene Richtung mit einer steigenden Geschwindigkeit, um eine fließende Bewegung darzustellen_

        transform.Translate(-Vector2.up * Time.deltaTime);      
        if (nachrechts)
        {
            transform.Translate(Vector3.right * Time.deltaTime * istAusweich);
        }
        if (nachlinks)
        {
            transform.Translate(Vector3.left * Time.deltaTime * istAusweich);
        }
        if (istAusweich<maxAusweich && (nachlinks || nachrechts))
        {
            istAusweich += (2f * Time.deltaTime);
        }

    }
    /// <summary>
    /// Bereich festlegen bei welchem Abstand (Box) zum Player der Gegner ausweichen sollte.
    /// </summary>
    void FixedUpdate()
    {
        Vector2 size = new Vector2(0.5f, 1f);
        Vector2 box1 = transform.position;
        Vector2 box2 = box1;
        box1.x += 0.25f;
        box2.x -= 0.25f;

        RaycastHit2D hit1 = Physics2D.BoxCast(box1, size, 0f, -Vector2.up, 4f, layer.value);
        RaycastHit2D hit2 = Physics2D.BoxCast(box2, size, 0f, -Vector2.up, 4f, layer.value);
        if(hit1.collider != null)
        {
            if(!nachlinks && !nachrechts && hit1.collider.CompareTag("Player"))
            {
                istAusweich = 0;
                nachlinks = true;
                anim.SetBool("links", true);
            }
        }
        if (hit2.collider != null)
        { 
            if (!nachlinks && !nachrechts && hit2.collider.CompareTag("Player"))
            {
                istAusweich = 0;
                nachrechts = true;
                anim.SetBool("rechts", true);
            }
        }
    }
    /// <summary>
    /// Zeit zwischen den Schüssen
    /// </summary>
    IEnumerator WarteSchuss(float z)
    {
        yield return new WaitForSeconds(z);
        jetzt = true;
    }
    /// <summary>
    /// Tod-Methode
    /// </summary>
    void Tot()
     {
        tot = true;
    }
}
