using UnityEngine;
using System.Collections;

public class ObjectMouseHandler : MonoBehaviour {

	public GameObject target;
	
	void OnMouseDown(){
		target.SendMessage("OnMouseDown", SendMessageOptions.DontRequireReceiver);
	}

	void OnMouseEnter(){
		target.SendMessage("OnMouseEnter", SendMessageOptions.DontRequireReceiver);
	}

	void OnMouseExit(){
		target.SendMessage("OnMouseExit", SendMessageOptions.DontRequireReceiver);
	}

	void OnMouseOver(){
		target.SendMessage("OnMouseOver", SendMessageOptions.DontRequireReceiver);
	}
}
