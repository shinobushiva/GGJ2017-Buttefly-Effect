using UnityEngine;
using System.Collections;

public class FlythroughCameraSpeedMultiplier : SpeedMultiplier {

	public FlyThroughCamera target;

	private float speed;

	// Use this for initialization
	void Start () {
		if (target == null) {
			Destroy (this);
			return;
		}
		speed = target.speed;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	
	public override void ChangeSpeed (float mul)
	{
		target.speed = speed * mul;
	}
}
