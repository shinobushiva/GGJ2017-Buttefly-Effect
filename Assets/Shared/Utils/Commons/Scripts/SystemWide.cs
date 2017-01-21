using UnityEngine;
using System.Collections;

public class SystemWide : SingletonMonoBehaviour<SystemWide> {

	public Texture2D handCursor;

	[HideInInspector]
	public Camera currentMainCamera;

	public bool CanCameraGetMouseInput(){
		if(Input.GetKey(KeyCode.LeftShift))
			return true;

		if(Input.GetKey(KeyCode.RightShift))
			return true;

		if(Input.touchCount >=2){
			return true;
		}

		return false;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
