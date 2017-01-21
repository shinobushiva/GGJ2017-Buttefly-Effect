using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSound : MonoBehaviour {

	public AudioClip clip;
	private AudioSource audio;

	// Use this for initialization
	void Start () {
		audio = FindObjectOfType<AudioSource> ();
	}
	
	// Update is called once per frame
	void OnCollisionEnter (Collision c) {
		if (c.gameObject.name == "street") {
			audio.clip = clip;
			audio.Play ();
		}
		
	}
}
