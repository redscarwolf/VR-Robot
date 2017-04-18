using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Bored : MonoBehaviour {

    Animator anim;

    public float waittime;
    public float resettime = 1.0f;

    private float startTime;
    private bool boredActivated;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        startTime = Time.time;
        boredActivated = false;
    }
    
    void Update()
    {
        float thisTime = Time.time;
        //wie IDLE animation unterbrechen?
        if (thisTime - startTime >= waittime)
        {
            startTime = thisTime;
            boredActivated = true;
        }
        if(thisTime - startTime >= resettime)
        {
            boredActivated = false;
        }
        anim.SetBool("bored", boredActivated);
        anim.SetFloat("time", thisTime);
    }
}