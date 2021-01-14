using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAusweichScript : MonoBehaviour
{
    public bool nachlinks;
    public bool nachrechts;
    public float maxAusweich = 2;   //max Ausweichgeschwindigkeit
    public float istAusweich = 0;   //momentane Ausweichgeschwindigkeit
    private Animator anim;
    public LayerMask layer;

    public float schussrate = 0.2f;
    public float ersterSchuss = 1.5f;
    public GameObject Schuss;
    private bool jetzt;
    private bool tot;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        float varia = Random.Range(0f, 0.5f);
        StartCoroutine(WarteSchuss(ersterSchuss + varia));
    }

    // Update is called once per frame
    void Update()
    {
        if(jetzt && !tot)
        {
            jetzt = false; 
            if(transform.position.x > -3f && transform.position.x < 3f)
            {
                Instantiate(Schuss, transform.position, Quaternion.identity);
                float varia = Random.Range(0f, 0.2f);
                StartCoroutine(WarteSchuss(1/schussrate + varia));
            }
        }
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

    void FixedUpdate()
    {
        //Vector 2dwn = transform.TransformDirection(Vector3.down);
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

    IEnumerator WarteSchuss(float z)
    {
        yield return new WaitForSeconds(z);
        jetzt = true;
    }
    void Tot()
     {
        tot = true;
    }
}
