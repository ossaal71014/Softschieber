using System.Collections;
using UnityEngine;

public class enemyLoopingScript : MonoBehaviour
{
    private bool rolle;
    public float gegenschub = 3f;
    private float istGegeschub = 0;
    private Animator anim;
    public LayerMask layer;

    public float schussrate = 0.8f;
    public float ersterSchuss = 1.5f;
    public GameObject Schuss;
    private bool jetzt;
    private bool tot;


    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        float varia = Random.Range(0f, 0.5f);
        StartCoroutine(WarteSchuss(ersterSchuss + varia));
    }

    // Update is called once per frame
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

    IEnumerator WarteSchuss(float z)
    {
        yield return new WaitForSeconds(z);
        jetzt = true;
    }

    void Tot()
    {
        tot = true;
    }

    void Vaterlos()
    {
        transform.parent = null;
    }
}