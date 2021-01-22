using System.Collections;
using UnityEngine;

public class enemyLoopingScript : MonoBehaviour
{
    private bool rolle;  // Variable für Bewegungs-Aktion
    public float gegenschub = 3f;  // Variable Gegenschub
    private float istGegeschub = 0;  // Geschwindugkeit Schub
    private Animator anim;
    public LayerMask layer;

    public float schussrate = 0.8f;  // Schussrate
    public float ersterSchuss = 1.5f;  // erster Schuss
    public GameObject Schuss;
    private bool jetzt;  // Varible für aktuellen Status
    private bool tot;  // Variable für tod sein


    /// <summary>
    /// Ruft den Start für des roten Gegners auf
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
        if (transform.parent != null)
        {
            transform.Translate((-Vector2.up + new Vector2(0f, istGegeschub)) * Time.deltaTime * 3);
        }
        if (jetzt && !tot)
        {
            jetzt = false;
            if (transform.localPosition.y < 5.5f && transform.localPosition.y > -5.5f)
            {
                Instantiate(Schuss, transform.position, Quaternion.identity);
                float varia = Random.Range(0f, 0.2f);
                StartCoroutine(WarteSchuss(1 / schussrate + varia));
            }
        }
        if (rolle && istGegeschub < gegenschub)
        {
            istGegeschub += Time.deltaTime * 1.5f;
        }
    }
    /// <summary>
    /// Bewegung der Rolle wird ausgeführt wenn der Player getroffen wird
    /// </summary>
    private void FixedUpdate()
    {
        Vector2 size = new Vector2(0.5f, 1f);
        Vector2 box1 = transform.position;

        RaycastHit2D hit1 = Physics2D.BoxCast(box1, size, 0f, -Vector2.up, 4f, layer.value);

        if (hit1.collider != null)
        {
            if (rolle && hit1.collider.CompareTag("Player"))
            {
                rolle = true;
                anim.SetBool("rolle", true);
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
    /// <summary>
    /// Tod-Methode
    /// </summary>
    void Vaterlos()
    {
        transform.parent = null;
    }
}