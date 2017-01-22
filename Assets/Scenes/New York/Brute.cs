using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brute : MonoBehaviour {

	private Animator anim;

	private AudioSource audio;
	public AudioClip falling;
	public AudioClip fellDown;

	public GameObject smokePrefab;

	public float waitTime = 2f;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		audio = FindObjectOfType<AudioSource> ();
	}


	public void Walk(){
		anim.SetTrigger ("Walk");
		StartCoroutine (Routine ());
	}

	public IEnumerator Routine(){

		yield return new WaitForSeconds (0.5f);
		if(falling){
			audio.clip = falling;
			audio.Play ();
		}

		yield return new WaitForSeconds (waitTime - 0.5f);


		GameObject.FindObjectOfType<SkirtFlipMaster> ().Flip ();

		yield return new WaitForSeconds (0.5f);

		if (smokePrefab) {
			GameObject.Instantiate (smokePrefab, smokePrefab.transform.position, smokePrefab.transform.rotation);
		}

		if(fellDown){
			audio.clip = fellDown;
			audio.Play ();
		}


	}
}
