using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkirtFlipMaster : MonoBehaviour {

	private ExplosionForce force;

	public float initWait = 3f;
	public float duration = 10f;
	public float minWait = 0.2f;
	public float maxWait = 0.2f;

	public float afterWait = 3f;


	public AudioClip clip;

	// Use this for initialization
	void Start () {
		force = FindObjectOfType<ExplosionForce> ();
	}

	public void Flip(){
		StartCoroutine (FlipSkirt ());
	}

	private IEnumerator FlipSkirt(){
		
		yield return new WaitForSeconds(initWait);

		float t = Time.time + duration;
		while(Time.time < t){

			force.Explosion ();
			yield return new WaitForSeconds(Random.Range(minWait, maxWait));

		}

		yield return new WaitForSeconds(afterWait);

		FindObjectOfType<PlayingMaster> ().PlaySucceed ();

	}
}
