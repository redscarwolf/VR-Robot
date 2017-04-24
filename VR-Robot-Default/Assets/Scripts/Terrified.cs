using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrified : MonoBehaviour {

    public float maxSpeed = 5;
    public float terrifiedDist;
    public Camera player;

    private Animator anim;

    void Start(){
        anim = GetComponent<Animator>();

    }

    void FixedUpdate () {
        Vector3 botPos = transform.position;
        Vector3 playerPos = player.transform.position;

        float dist = Vector3.Distance(playerPos, botPos);

        Vector3 velocity = player.velocity;

        if(dist <= terrifiedDist)
        {
            if(velocity.x > maxSpeed ||velocity.y > maxSpeed || velocity.z > maxSpeed)
            {
                anim.SetTrigger("TERRIFIEDTRIGGER");
            }
        }
    }
}
