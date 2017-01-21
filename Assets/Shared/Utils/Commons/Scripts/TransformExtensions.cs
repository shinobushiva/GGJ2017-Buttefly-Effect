using UnityEngine;
using System.Collections;

public static class TransformExtensions
{
	public static void SetLayer(this Transform trans, int layer) 
	{
		trans.gameObject.layer = layer;
		foreach(Transform child in trans)
			child.SetLayer( layer);
	}

	public static void SetCollision(this Transform trans, bool col) 
	{
		Collider[] cs = trans.GetComponentsInChildren<Collider> ();
		foreach (Collider c in cs)
			c.enabled = col;

		Collider cc = trans.GetComponent<Collider>();
		if(cc)
			cc.enabled = col;
	}

	public static void CopyTo(this Transform trans, Transform dest)
	{
		dest.position = trans.position;
		dest.rotation = trans.rotation;
		dest.localScale = trans.localScale;
	}


	public static Vector2 Rotate(this Vector2 v, float degrees) {
		float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
		float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

		float tx = v.x;
		float ty = v.y;
		v.x = (cos * tx) - (sin * ty);
		v.y = (sin * tx) + (cos * ty);
		return v;
	}
}
