using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroid : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        // Animationsgeschwindigkeit, Größe, Anpassung an neue Größe
        anim.speed = Random.Range(0.5f, 1.5f);
        float groesse = Random.Range(1.0f, 1.3f);
        transform.localScale = new Vector3(groesse, groesse, groesse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
