﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySchuss : MonoBehaviour
{
    public float speed = 2f;
    public int schaden = 1;
    public GameObject explo;
    private Vector3 richtung;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");        //Spüre Position des Spielers auf
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}