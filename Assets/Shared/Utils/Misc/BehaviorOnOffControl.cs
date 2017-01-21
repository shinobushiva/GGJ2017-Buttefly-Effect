using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Shiva.UI;
using System;

public class BehaviorOnOffControl : MonoBehaviour {

	private List<Component> targets;

	private InputModeIcon inputMode;

	void Awake(){
		inputMode = GameObject.FindObjectOfType<InputModeIcon> ();
		if (inputMode)
			inputMode.SetMode ("mousing");
	}

	// Use this for initialization
	void Start () {
		targets = new List<Component> ();

		//MouseLook is no longer a behaviour
		targets.AddRange (GetComponentsInChildren<MouseLookBehaviour> ());


		targets.AddRange (GetComponentsInChildren (Type.GetType("UnityStandardAssets.Characters.FirstPerson.FirstPersonController")));
		targets.AddRange (GetComponentsInChildren(Type.GetType("UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController")));

		Cursor.visible = false;
	}

	bool toggle = true;

	// Update is called once per frame
	void Update () {

		bool b  = Input.GetKey (KeyCode.LeftShift);
		if(b != toggle){
			toggle = b;

			foreach (Component ml in targets) {
				
				((Behaviour)ml).enabled = toggle;
			}

			if (toggle) {
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
				if (inputMode)
					inputMode.SetMode ("walking");
			} else {
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
				if (inputMode)
					inputMode.SetMode ("mousing");
			}
		}
		    
	}
}
