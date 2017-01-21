using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class RigidbodyFirstPersonControllerSpeedMultiplier : SpeedMultiplier {

	public RigidbodyFirstPersonController target;

	private float ForwardSpeed;   // Speed when walking forward
	private float BackwardSpeed;  // Speed when walking backwards
	private float StrafeSpeed; 

	private float XSensitivity;
	private float YSensitivity;

	// Use this for initialization
	void Start () {
		if (target == null) {
			Destroy (this);
			return;
		}

		ForwardSpeed = target.movementSettings.ForwardSpeed;
		BackwardSpeed = target.movementSettings.BackwardSpeed;
		StrafeSpeed = target.movementSettings.StrafeSpeed;
		XSensitivity = target.mouseLook.XSensitivity;
		YSensitivity = target.mouseLook.YSensitivity;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void ChangeSpeed (float mul)
	{
		target.movementSettings.ForwardSpeed = ForwardSpeed*mul;
		target.movementSettings.BackwardSpeed = BackwardSpeed*mul;
		target.movementSettings.StrafeSpeed = StrafeSpeed*mul;
		target.mouseLook.XSensitivity = XSensitivity*mul;
		target.mouseLook.YSensitivity = YSensitivity*mul;
	}
}
