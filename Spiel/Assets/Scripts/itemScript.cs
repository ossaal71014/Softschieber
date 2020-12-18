using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemScript : MonoBehaviour
{
    public enum itemTyp {shieldAdd, shipAdd, weapon1}; 
    public itemTyp typ = itemTyp.shieldAdd;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("killzone"))
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("Player"))
        {
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            //renderer.enabled = false;
            //collider2D.enabled = false;
            // Zerstöre Obejkt erst nach 2f
            Destroy(gameObject, 2f);
            GetComponent<AudioSource>().Play();

            //Schiff wird benachrichtigt das Schild, Schiff oder Waffe erhalten wurde
            if (typ == itemTyp.shieldAdd)
            {
                other.SendMessage("ShieldAdd", SendMessageOptions.DontRequireReceiver);
            }
            if (typ == itemTyp.shipAdd)
            {
                other.SendMessage("ShipAdd", SendMessageOptions.DontRequireReceiver);
            }
            if (typ == itemTyp.weapon1)
            {
                other.SendMessage("Weapon1", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
