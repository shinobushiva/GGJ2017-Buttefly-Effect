using UnityEngine;
using System.Collections;

public class EscapeToQuit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void Update() { 
		if (Input.GetKey(KeyCode.Escape)) {
			Application.Quit(); 
		}
	}
}
