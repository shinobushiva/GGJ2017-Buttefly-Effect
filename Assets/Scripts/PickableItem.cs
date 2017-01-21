using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour {

	private PickableSuite suite;

	private Vector3 orgPos;
	private Quaternion orgRot;

	private bool picked;

	public AudioClip clip;

	// Use this for initialization
	void Start () {
		suite = transform.GetComponentInParent<PickableSuite> ();

		orgPos = transform.position;
		orgRot = transform.rotation;
		
	}


	void OnMouseDown(){

		if (!picked) {
			suite.ItemPicked (this);
			picked = true;

			if (clip) {
				AudioSource.PlayClipAtPoint (clip, Camera.main.transform.position);
			}
		}
	}

	public void PutBack(){
		transform.position = orgPos;
		transform.rotation = orgRot;
	}

	public void SetAt(PlaceTarget target){
		transform.position = target.transform.position;
		transform.rotation = target.transform.rotation;

		orgPos = transform.position;
		orgRot = transform.rotation;
		picked = false;
	}
}
