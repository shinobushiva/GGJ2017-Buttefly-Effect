using UnityEngine;
using System.Collections;

public class SpeedMultiplierMaster : MonoBehaviour {

	public float multiplier;

	public float Multiplier {
		set{
			multiplier = value;
			foreach(SpeedMultiplier sm in muls){
				sm.ChangeSpeed(multiplier);
			}
		}
		get{
			return multiplier;
		}
	}


	private SpeedMultiplier[] muls;

	// Use this for initialization
	void Start () {
		muls = GetComponents<SpeedMultiplier> ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
