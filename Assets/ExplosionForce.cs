using UnityEngine;
using System.Collections;

public class ExplosionForce : MonoBehaviour {

	public float radius = 10f;
	public float force = 100;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Explosion(){
		Collider[] cs = Physics.OverlapSphere (transform.position, radius);
		foreach (Collider c in cs) {
			if (c.GetComponent<Rigidbody> () != null) {
				c.GetComponent<Rigidbody> ().AddExplosionForce (force, transform.position, radius); 
			}
		}
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, radius);
	}
}
