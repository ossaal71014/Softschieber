using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class schild : MonoBehaviour
{
    public bool schildAn; //default false
    private Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (schildAn)
        {
            anim.SetBool("schildAn", true);
            schildAn = false;
            StartCoroutine(MachAus());

        }
    }
    IEnumerator MachAus()
    {
        yield return (new WaitForSeconds(0.1f));
        anim.SetBool("schildAn", false);
    }
}
