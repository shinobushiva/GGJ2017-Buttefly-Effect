using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDropper : MonoBehaviour {

	public Rigidbody drop;

	public Animator anim;

	public Kunoichi kunoichi;

	public Transform target;

	// Use this for initialization
	void Start () {
		drop.isKinematic = true;
		anim.enabled = false;
		
	}

	public void Play(){
		drop.isKinematic = false;
		anim.enabled = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (Vector3.Distance (drop.transform.position, target.transform.position) < 50f) {
			kunoichi.Run ();
			GameObject.Destroy (this);
		}

		if (!drop.isKinematic && drop.IsSleeping()) {
			GameObject.FindObjectOfType<PlayingMaster> ().Restart ();
		}
		
	}

		
}
