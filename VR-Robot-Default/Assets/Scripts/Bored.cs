﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bored : MonoBehaviour {

    Animator anim;

    public float waittime;
    public float resettime = 1.0f;

    private float startTime;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        startTime = Time.time;
    }
    
    void Update()
    {
		
    }



}