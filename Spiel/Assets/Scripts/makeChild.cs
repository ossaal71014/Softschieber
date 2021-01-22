using System.Collections;
using UnityEngine;

public class makeChild : MonoBehaviour
{
    /// <summary>
    /// MainCamera als parent-Object festlegen
    /// </summary>
    void Awake()
    {
        transform.parent = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
