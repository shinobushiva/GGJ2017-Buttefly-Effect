//  OnTouchDown.cs
//  Allows "OnMouseDown()" events to work on the iPhone.
//  Attack to the main camera.
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OnTouchDown : MonoBehaviour
{
	
	public static Vector3 pos;
	private bool isTouchDown = false;
	private Transform target;
	
	void Update ()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android) {
			if (Input.touchCount > 0)
				pos = Input.touches [0].position;
		} else {
			pos = Input.mousePosition; 
		}
		
		
		// Code for OnMouseDown in the iPhone. Unquote to test.
		RaycastHit hit = new RaycastHit ();
		for (int i = 0; i < Input.touchCount; ++i) {
			if (Input.GetTouch (i).phase.Equals (TouchPhase.Began)) {
				isTouchDown = true;
				// Construct a ray from the current touch coordinates
				Ray ray = GetComponent<Camera>().ScreenPointToRay (Input.GetTouch (i).position);
				if (Physics.Raycast (ray, out hit)) {
					Debug.Log ("OnTouchDown : " + hit.transform.gameObject);
					if (Input.GetTouch (i).tapCount == 1) {
						Debug.Log ("OnMouseDown Sending");
						hit.transform.gameObject.SendMessage ("OnMouseDown", SendMessageOptions.DontRequireReceiver);
						target = hit.transform;
						
						/*
						if (hit.transform.gameObject.GetComponent<FishController> () != null) {
							SendMessage ("FishDoubleTapped", hit.transform.gameObject.GetComponent<FishController> (), SendMessageOptions.DontRequireReceiver);
						}
						*/
					}
				}
			}
			
			if (Input.GetTouch (i).phase.Equals (TouchPhase.Moved) && isTouchDown) {
				if (target)
					target.gameObject.SendMessage ("OnMouseDrag", SendMessageOptions.DontRequireReceiver);
			}
			
			if (Input.GetTouch (i).phase.Equals (TouchPhase.Ended) && isTouchDown) {
				isTouchDown = false;
				if (target)
					target.gameObject.SendMessage ("OnMouseUp", SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}
