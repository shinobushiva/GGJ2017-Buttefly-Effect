using UnityEngine;
using System.Collections;

public class UISwitcher : MonoBehaviour {

	public GameObject[] targets;
	 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.P)){
			foreach(GameObject target in targets)
				target.SetActive(!target.activeSelf);
		}
	
	}
}
