using UnityEngine;
using System.Collections;

public class AnimationSelector : MonoBehaviour {

	public Animator animator;

	
	public int minActionType = 0;
	public int minSitType = 0;

	public int maxActionType = 4;
	public int maxSitType = 4;

	private int sitType;
	private int actionType;

	public float minTimeChangeAction = 1f;
	public float maxTimeChangeAction = 10f;

	// Use this for initialization
	void Start () {
		if (animator == null) {
			animator = GetComponent<Animator> ();
		}
//		if (animator == null) {
//			Debug.Log ("ERROR: No animator found!");
//			Destroy (this);
//		}

		StartCoroutine (Routine ());
		StartCoroutine (Routine2 ());
	}

	public void OnModelChanged(Animator anim){
		animator = anim;
	}
	
	// Update is called once per frame
	IEnumerator Routine () {

		while (true) {
			yield return new WaitForSeconds (Random.Range (0, .8f));

			int type = Random.Range (minActionType, maxActionType);

			if(type != actionType){
				actionType = type;
				if(animator)
					animator.SetInteger ("ActionType", actionType);
			}
			
			yield return new WaitForSeconds (Random.Range (minTimeChangeAction, maxTimeChangeAction));
		}
	
	}

	// Update is called once per frame
	IEnumerator Routine2 () {
		
		while (true) {
			yield return new WaitForSeconds (Random.Range (0, .8f));

			int type = Random.Range (minSitType, maxSitType);
			if(type != sitType){
				sitType = type;
				if(animator)
					animator.SetInteger ("SitType", sitType);
			}
			
			yield return new WaitForSeconds (Random.Range (minTimeChangeAction, maxTimeChangeAction));
		}
		
	}

}
