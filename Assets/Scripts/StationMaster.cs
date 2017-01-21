using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StationMaster : MonoBehaviour {

	private SkirtFlipMaster flipMaster;
	private PlayingMaster playingMaster;

	public TrainRun trainRun;

	private Button playButton;

	// Use this for initialization
	void Start () {
		flipMaster = FindObjectOfType<SkirtFlipMaster> ();
		playingMaster = FindObjectOfType<PlayingMaster> ();
		
	}

	public void PlayClicked(){
		
		bool flag = false;
		foreach (PickableSuite ps in playingMaster.suites) {
			if (ps.solved != 1) {
				flag = true;
				break;
			}
		}

		if (!flag) {
			StartCoroutine (trainRun.RunThrough (this));
			flipMaster.Flip ();
		} else {
			StartCoroutine (trainRun.BreakAndStop (this));
		}
		
	}

	public void Completed(){
		playingMaster.PlayCompleted ();
	}


}
