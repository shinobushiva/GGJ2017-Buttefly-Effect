using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewYorkMaster : MonoBehaviour {

	public BlockDropper blockDropper;
	public Kunoichi kunoichi;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayClicked(){
		blockDropper.Play ();
		kunoichi.Play ();
	}
}
