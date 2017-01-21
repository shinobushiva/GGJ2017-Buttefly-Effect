using UnityEngine;
using System.Collections;

public class HideRendererAtRuntime : MonoBehaviour
{

	public bool showHide = false;
	public bool disableCollider = false;

	// Use this for initialization
	void Start ()
	{
		Renderer[] rs = GetComponentsInChildren<Renderer> ();
			
		foreach (Renderer r in rs)
			r.enabled = showHide;

		if (disableCollider) {
			Collider[] cs = GetComponentsInChildren<Collider> ();
		
			foreach (Collider c in cs)
				c.enabled = showHide;
		}
	
	}
}
