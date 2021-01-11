using System.Collections;
using UnityEngine;

public class schild : MonoBehaviour
{
    public bool schildAn;   //default-Wert: false
    private Animator anim; //Beschreibung der boolschen Variable
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Schildanimation erstellen mit der Übergabe der schildAn-Variable aus shipController.cs
        if (schildAn)
        {
            anim.SetBool("schildAn", true);
            schildAn = false; 
            StartCoroutine (MachAus()); //SchildAn im Animator auf false setzen, damit es nicht wiederholt wird
        }
    }
    IEnumerator MachAus()
    {
        yield return (new WaitForSeconds(0.1f));
        anim.SetBool("schildAn", false);
    }
}
