using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float cameraSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // LateUpdate -> Kamera bewegt sich erst nachdem alle anderen "Geupdatet" haben
    void LateUpdate()
    {
        transform.Translate(Vector3.up * Time.deltaTime * cameraSpeed, Space.World);
    }
}
