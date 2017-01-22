using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBin : MonoBehaviour {

	public Transform trashBin;
	public Rigidbody trashBinToRollPref;
	private Rigidbody trashBinToRoll;

	public Transform target;

	public Transform binCenter;

	public Brute brute;

	private AudioSource audio;
	public AudioClip roll;

	// Use this for initialization
	void Start () {
		audio = FindObjectOfType<AudioSource> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (!trashBinToRoll)
			return;

		Vector3 dir = (target.position - trashBinToRoll.position);
		dir.y = trashBinToRoll.position.y;
		dir = dir.normalized;
		trashBinToRoll.transform.LookAt (trashBinToRoll.position - dir, Vector3.forward);

		trashBinToRoll.AddRelativeForce (-Vector3.forward * 50f);
		trashBinToRoll.AddRelativeTorque (transform.up * 1000f);


		if (Vector3.Distance (target.position, trashBinToRoll.position) < 10f) {
			Destroy (trashBinToRoll.gameObject);

		}
	}

	public void Torque(){
		GameObject.Destroy (trashBin.gameObject);
		trashBinToRoll = Instantiate (trashBinToRollPref, binCenter.position, transform.rotation);
		trashBinToRoll.AddForce (new Vector3(-2, 0, -1) * 1000f);

		StartCoroutine (Routine ());

		if(roll){
			audio.PlayOneShot (roll);
		}
	}

	public IEnumerator Routine(){

		yield return new WaitForSeconds (2.5f);
		brute.Walk ();
	}

}
