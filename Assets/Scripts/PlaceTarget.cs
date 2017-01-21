using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTarget : MonoBehaviour {

	private PickableSuite suite;

	public int solveLevel = 0;

	// Use this for initialization
	void Start () {
		suite = transform.GetComponentInParent<PickableSuite> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown(){

		suite.TargetSelected (this);
	}
}
