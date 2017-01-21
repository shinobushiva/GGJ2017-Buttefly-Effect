using UnityEngine;
using System.Collections;

public class SyncMove : MonoBehaviour {

	public Transform target;
	public Vector3 offset;
	public Vector3 rotation;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = target.position+offset;
		transform.rotation = target.rotation;
		Quaternion q = transform.rotation;
		Vector3 ang = q.eulerAngles;
		ang += rotation;
		q.eulerAngles = ang;
		transform.rotation = q;
	}
}
