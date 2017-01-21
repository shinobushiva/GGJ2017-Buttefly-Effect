using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableSuite : MonoBehaviour {

	private PickableItem item;
	private PlaceTarget[] targets;

	[HideInInspector]
	public int solved = 0; 

	// Use this for initialization
	void Start () {
		item = transform.GetComponentInChildren<PickableItem> ();
		targets = transform.GetComponentsInChildren<PlaceTarget> ();

		foreach (PlaceTarget pt in targets) {
			pt.gameObject.SetActive (false);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void ItemPicked(PickableItem item){
		foreach (PlaceTarget pt in targets) {
			pt.gameObject.SetActive (true);
		}
		
	}

	public void ItemReleased(PickableItem item){
		foreach (PlaceTarget pt in targets) {
			pt.gameObject.SetActive (false);
		}
	}

	public void TargetSelected(PlaceTarget target){
		item.SetAt (target);
		solved = target.solveLevel;

		foreach (PlaceTarget pt in targets) {
			pt.gameObject.SetActive (false);
		}
	}
}
