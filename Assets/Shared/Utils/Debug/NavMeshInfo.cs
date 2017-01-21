using UnityEngine;
using System.Collections;

public class NavMeshInfo : MonoBehaviour {

	NavMeshAgent nma;

	// Use this for initialization
	void Start () {
		nma = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDrawGizmos(){
		if(!nma)
			return;

		NavMeshPath path = nma.path;
		if(path != null){
			Vector3[] poss = path.corners;
			 
			Gizmos.color = Color.red;
			foreach(Vector3 pos in poss){
				Gizmos.DrawSphere(pos, 0.1f);
			}
		}

	}
}
