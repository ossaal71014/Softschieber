using System.Collections;
using UnityEngine;

public class makeChild : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        transform.parent = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
