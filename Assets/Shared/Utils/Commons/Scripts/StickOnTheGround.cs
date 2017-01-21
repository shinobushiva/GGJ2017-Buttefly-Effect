using UnityEngine;
using System.Collections;

public class StickOnTheGround : MonoBehaviour
{

	public Transform rootTrans;
	public float rayLength = 1;
	public Vector3 footOffset;

	public Vector3 groundPoint;

	void OnDrawGizmosSelected ()
	{
		if (rootTrans == null) {
			return;
		}
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere (rootTrans.position + footOffset, 0.5f);
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (groundPoint, 0.5f);
	}

	void Reset ()
	{
		rootTrans = transform;
	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{

		RaycastHit ray;
		
		var from = rootTrans.TransformPoint (footOffset) + Vector3.up * rayLength;
		var dir = Vector3.down;
		var length = 2 * rayLength;
		Debug.DrawLine (from, from + dir * 10, Color.green);

		int layer = rootTrans.gameObject.layer;
		if (Physics.Raycast (from, dir, out ray, length)) {
			// レイが当たった場所を踵の場所にする
			groundPoint = ray.point;
//			print (ray.transform.gameObject.name);

			rootTrans.position = groundPoint + footOffset;
		}

	}
}
