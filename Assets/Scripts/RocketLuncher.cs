using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RocketLuncher : MonoBehaviour {

	public ExplosionForce force;
	public Animator anim;

	public float initWait = 3f;
	public float duration = 100;
	public float minWait = 0.1f;
	public float maxWait = 0.1f;

	public float afterWait = 3f;

	public AudioClip clip;

	public Button button;

	public string nextSceneName;

	public AudioClip intro;


	// Use this for initialization
	void Start () {
		FindObjectOfType<AudioSource> ().clip = intro;
//		FindObjectOfType<AudioSource> ().Play ();
	}
	
	// Update is called once per frame
	public void Launch () {
		FindObjectOfType<AudioSource> ().Stop ();

		button.gameObject.SetActive(false);
		anim.enabled = true;
		StartCoroutine (Routine ());
	}

	private IEnumerator Routine(){

		AudioSource.PlayClipAtPoint (clip, Camera.main.transform.position);

		yield return new WaitForSeconds(initWait);

		float t = Time.time + duration;
		while(Time.time < t){

			force.Explosion ();
			yield return new WaitForSeconds(Random.Range(minWait, maxWait));

		}

		yield return new WaitForSeconds(afterWait);

		SceneManager.LoadSceneAsync(nextSceneName);
		
	}
}
