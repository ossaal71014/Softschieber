using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killaftertime : MonoBehaviour
{
    public float zeit = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, zeit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
