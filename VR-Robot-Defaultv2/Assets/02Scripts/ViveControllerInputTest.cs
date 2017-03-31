using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViveControllerInputTest : MonoBehaviour {

	private SteamVR_TrackedObject trackedObj;

    public ushort pulseIntensity;


    private SteamVR_Controller.Device Controller
	{
		get { return SteamVR_Controller.Input((int)trackedObj.index); }
	}

	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	void Update () {
		
			
		if (Controller.GetHairTriggerDown())
		{
			Debug.Log(gameObject.name + " Trigger Press");
		}
			
		if (Controller.GetHairTriggerUp())
		{
			Debug.Log(gameObject.name + " Trigger Release");
            Controller.TriggerHapticPulse(1000);
            Debug.Log("Pulse3!!!!");
        }
			
		if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
		{
			Debug.Log(gameObject.name + " Grip Press");
		}
			
		if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
		{
			Debug.Log(gameObject.name + " Grip Release");
		}

        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Debug.Log(gameObject.name + " Touchpad Button Press");
        }

        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.System))
        {
            Debug.Log(gameObject.name + " SystemButton Press");
        }

        //if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        //{
        //    Debug.Log(gameObject.name + " TriggerButton Press");
        //    Controller.TriggerHapticPulse(700);
        //    Debug.Log("Pulse2!!!!");
        //}

        if (Controller.GetAxis() != Vector2.zero)
        {
            Debug.Log(gameObject.name + Controller.GetAxis());
            Controller.TriggerHapticPulse(pulseIntensity);
            Debug.Log(pulseIntensity + "Pulseintesity");  // max value ist 3999
        }

        //TODO Marcel: unklar was passiert, wird bei drücken des Touchpad angezeigt
        //if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Axis0))
        //{
        //    Debug.Log(gameObject.name + " Axis0 Press");
        //}
    }
}
