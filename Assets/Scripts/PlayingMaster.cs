﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayingMaster : MonoBehaviour {

	private Button playButton;

	public string nextSceneName;
	public PickableSuite[] suites;
	public UnityEvent onPlay;
	public UnityEvent onSucceed;

	public bool autoNext = true;



	// Use this for initialization
	void Start () {
		playButton = transform.GetComponentInChildren<Button> ();
		
	}
	
	// Update is called once per frame
	public void PlayClicked () {
		playButton.gameObject.SetActive (false);

		onPlay.Invoke ();

		foreach (PickableSuite ps in suites) {
			ps.GetComponentInChildren<PickableItem> ().GetComponent<Collider>().enabled = false;
		}
	}

	public void PlayCompleted(){

		playButton.gameObject.SetActive (true);

		foreach (PickableSuite ps in suites) {
			ps.GetComponentInChildren<PickableItem> ().GetComponent<Collider>().enabled = true;
		}
	}

	public void PlaySucceed(){

		if (autoNext) {
			SceneManager.LoadSceneAsync (nextSceneName);
		} else {
			onSucceed.Invoke ();
		}
	}

	public void Restart(){
		SceneManager.LoadSceneAsync (SceneManager.GetActiveScene().name);
	}
}
