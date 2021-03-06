﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float cameraSpeed = 1f;
    private float oldSpeed;  // Kamerageschwindigkeit im "Normalbetrieb"
    private float newSpeed;  // Kamerageschwindigkeit in Todesphase
    private GameLogic gLogic;

    // Start is called before the first frame update
    void Start()
    {
        gLogic = gameObject.GetComponent<GameLogic>();
        oldSpeed = cameraSpeed;
        newSpeed = cameraSpeed * 3;
    }
    void Update()
    {
        if(gLogic.todesPhase && cameraSpeed < newSpeed)
        {
            cameraSpeed += Time.deltaTime * 3;
        }
        if (!gLogic.todesPhase)
        {
            cameraSpeed = oldSpeed;
        }
    }

    /// <summary>
    /// Kamera bewegt sich erst nachdem alle anderen "Geupdatet" haben
    /// </summary>
    void LateUpdate()
    {
        transform.Translate(Vector3.up * Time.deltaTime * cameraSpeed, Space.World);
    }
}
