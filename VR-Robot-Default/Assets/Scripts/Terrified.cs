using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrified : MonoBehaviour {

    public float maxSpeed = 5;
    public float terrifiedDist;
	public SteamVR_TrackedObject player;
	private float counter;

    private Animator anim;

    void Start(){
        anim = GetComponent<Animator>();
		counter = 0.0f;
    }

    void FixedUpdate () { 

		/* execute terrified script every second to avoid recheck 
		 * the velocity while moving backwards and retrigger the 
		 * terrified animation */
		counter += Time.deltaTime;

		if (counter > 1) {
			checkTerrfied ();
		}
    }

	private void checkTerrfied() {
		Vector3 botPos = transform.position;
		Vector3 playerPos = player.transform.position;

		float dist = Vector3.Distance(playerPos, botPos);

		Vector3 velocity = Camera.velocity;

		//Debug.Log ("velocity: " + velocity.magnitude);
		//Debug.Log ("distance: " + dist);


		if(dist <= terrifiedDist)
		{
			//if(velocity.x > maxSpeed ||velocity.y > maxSpeed || velocity.z > maxSpeed)
			if(velocity.magnitude > maxSpeed)
			{
				anim.SetTrigger("TERRIFIEDTRIGGER");
				counter = 0.0f;
			}
		}
	}

	private SteamVR_Controller.Device Camera
	{
		get {
			return SteamVR_Controller.Input ((int)player.index);
		}
	}
}
