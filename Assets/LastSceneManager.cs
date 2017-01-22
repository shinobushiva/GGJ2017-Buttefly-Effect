using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Cameras;

public class LastSceneManager : MonoBehaviour {

	public Camera camera;
	public Camera subCamera;

	public Transform rocket;

	public Transform rocketTarget;

	public int state = 0;

	public GameObject explosion;

	public AudioClip rocketSound;
	private AudioSource audio;

	public AudioClip ending;

	// Use this for initialization
	void Start () {
		audio = FindObjectOfType<AudioSource>();
	}


	// Update is called once per frame
	void Update () {
		if (state == 1) {
			SmoothLookAt sla = camera.gameObject.AddComponent<SmoothLookAt> ();
			sla.target = rocket;
			sla.damping = 1;
			state = 2;

			rocket.gameObject.GetComponent<SmoothLookAt> ().enabled = true;

			audio.clip = rocketSound;
			audio.Play ();
		}

		if (state == 2) {

			float stopDist = 250f;

			float d = Vector3.Distance (rocket.position, rocketTarget.position);
			rocket.Translate (Vector3.forward * Time.deltaTime * Mathf.Min(500f, (d-stopDist+2f)));

			if (d < stopDist) {

				audio.Stop ();
				subCamera.enabled = true;


				SkirtFlipMaster sfm = FindObjectOfType<SkirtFlipMaster> ();
				sfm.duration = 600f;
				sfm.minWait = 0.1f;
				sfm.minWait = 2f;
				sfm.Flip ();

				audio.loop = true;
				audio.clip = ending;
				audio.Play ();

				state = 3;
			}
		}
	}

	IEnumerator SmallRoutine(MeshFilter[] ms){

		float t = Time.time;
		float dur = 1f;

		while (t + dur > Time.time) {
			foreach (MeshFilter mf in ms) {
				mf.transform.localScale = Vector3.one * (1 - (Time.time - t));
			}
			yield return new WaitForEndOfFrame ();
		}

	}



	public void Play(){
		if (state == 0) {
			state = 1;
		}
		if (state == 3) {
			
			
		}
	}
}
