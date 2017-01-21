using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunoichi : MonoBehaviour {

	public Animator anim;

	public TrashBin trashBin;

	public Transform runTarget;


	private AudioSource audio;
	public AudioClip fellDown;
	public AudioClip dash;

	// Use this for initialization
	void Start () {
//		anim.enabled = false;	

		audio = FindObjectOfType<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Play(){
		anim.enabled = true;
	}

	public void Run(){
		StartCoroutine (RunRoutine ());
	}

	private IEnumerator RunRoutine(){
		if (dash) {
			audio.clip = dash;
			audio.Play ();
		}

		Vector3 pos = anim.transform.position;
		Vector3 dir = runTarget.transform.position - pos;
		dir.y = pos.y;
		dir = dir.normalized;

		anim.transform.LookAt (pos+dir);
		anim.applyRootMotion = false;
		anim.SetTrigger ("Run");

		float t = Time.time;

		while (true) {
			if (Vector3.Distance (trashBin.transform.position, anim.transform.position) < 10f) {
				break;
			}
			anim.transform.Translate (Vector3.forward * Time.deltaTime * 40f, Space.Self);
			yield return new WaitForEndOfFrame ();

			if (t + 5f < Time.time) {
				GameObject.FindObjectOfType<PlayingMaster> ().Restart ();
			}
		}
		anim.applyRootMotion = true;
		anim.SetTrigger ("Fall");

		if (dash) {
			audio.clip = fellDown;
			audio.Play ();
		}

		trashBin.Torque ();
	}
}
