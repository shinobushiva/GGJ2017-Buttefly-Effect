using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainRun : MonoBehaviour {

	public AudioClip breaking;
	public AudioClip running;

	public AudioSource audio;

	public Vector3 initialMove =  Vector3.zero;
	public Vector3 orgPos;

	public float throughTime = 5f;

	// Use this for initialization
	void Start () {
		orgPos = transform.position;



//		StartCoroutine (RunThrough());
//		StartCoroutine (BreakAndStop());

		Init ();
		
	}

	public void Init(){

		transform.position = orgPos + transform.TransformVector(initialMove);
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}

	public IEnumerator RunThrough(MonoBehaviour callee){
		Init ();
		
		audio.clip = running;
		audio.Play ();

		Vector3 start = orgPos + transform.TransformVector (initialMove);
		Vector3 goal = orgPos - transform.TransformVector (initialMove) * 4;

		float t = Time.time;

		while (t + throughTime > Time.time) {
			transform.position = Vector3.Lerp (start, goal, (Time.time - t) / throughTime);

			yield return new WaitForEndOfFrame ();
		}

		callee.SendMessage ("Completed", true, SendMessageOptions.DontRequireReceiver);

	}

	public IEnumerator BreakAndStop(MonoBehaviour callee){
		Init ();

		audio.clip = running;
		audio.Play ();

		yield return new WaitForSeconds (1.5f);

		audio.Stop ();
		audio.clip = breaking;
		audio.Play ();

		Vector3 start = orgPos + transform.TransformVector (initialMove/2);
		Vector3 goal = orgPos;

		float t = Time.time;

		while (t + throughTime > Time.time) {
			float tt = (Time.time - t) / throughTime;
			transform.position = Vector3.Lerp (start, goal, Mathf.Pow(tt, 1f/3f));

			yield return new WaitForEndOfFrame ();
		}

		callee.SendMessage ("Completed", false, SendMessageOptions.DontRequireReceiver);

	}

}
