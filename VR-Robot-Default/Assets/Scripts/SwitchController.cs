using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour {

    public GameObject[] objects;

    private int counter;

	void Start () {
        counter = 0;
	}
	
	void Update () {
        if (Input.GetButtonDown("switch"))
        {
            switchBots();
        }
    
	}

    private void switchBots()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(false);
        }

        objects[counter].SetActive(true);

        counter++;

        if(counter >= objects.Length)
        {
            counter = 0;
        }
    }
}
