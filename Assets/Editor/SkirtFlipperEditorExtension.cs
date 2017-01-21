using UnityEngine;
using UnityEditor;
using System.Collections;

public class SkirtFlipperEditorExtension : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	[MenuItem("SkirtFlipper/AddColliderRigidbody")]
	public static void AddColliderRigidbody(){

		Transform sel = Selection.activeTransform;
		Rigidbody rb = sel.gameObject.AddComponent<Rigidbody> ();
		rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
		rb.isKinematic = true;
		foreach (Transform tt in sel) {
			SphereCollider sc = tt.gameObject.AddComponent<SphereCollider> ();
			sc.radius = 0.1f;
//			sc.height = 0.05f;
//			sc.direction = 0;
			rb = tt.gameObject.AddComponent<Rigidbody> ();
			rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
			rb.interpolation = RigidbodyInterpolation.Interpolate;
//			rb.constraints = RigidbodyConstraints.FreezePosition;
//			rb.constraints = RigidbodyConstraints.FreezeRotationY;
			HingeJoint hj = tt.gameObject.AddComponent<HingeJoint> ();
			hj.axis = Vector3.forward;
			hj.connectedBody = sel.GetComponent<Rigidbody>();
			hj.useLimits = true;
//			JointLimits jl = new JointLimits ();
//			jl.max = 100;
//			jl.bounciness = 1;
//			hj.limits = jl;
//			hj.useSpring = true;
//			JointSpring js = new JointSpring ();
//			js.spring = 5f;
//			js.damper = 5f;
//			hj.spring = js;
			tt.SetLayer(LayerMask.NameToLayer("SkirtCollider"));

			Transform p = tt;
			while (p.childCount > 0) {
				Transform pp = p;
				p = p.GetChild(0);
				sc = p.gameObject.AddComponent<SphereCollider> ();
				sc.radius = 0.2f;
//				sc.height = 0.05f;
//				sc.direction = 0;
				rb = p.gameObject.AddComponent<Rigidbody> ();
				rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
				rb.interpolation = RigidbodyInterpolation.Interpolate;
//				rb.constraints = RigidbodyConstraints.FreezeRotationY;
				hj = p.gameObject.AddComponent<HingeJoint> ();
				hj.axis = Vector3.forward;
				hj.connectedBody = pp.GetComponent<Rigidbody>();
				hj.useLimits = true;
//				jl = new JointLimits ();
//				jl.max = 100;
//				jl.bounciness = 1;
//				hj.limits = jl;
//				hj.useSpring = true;
//				js = new JointSpring ();
//				js.spring = 5f;
//				js.damper = 5f;
//				hj.spring = js;
				pp.SetLayer(LayerMask.NameToLayer("SkirtCollider"));
			}
		}
	}

	[MenuItem("SkirtFlipper/ClearColliderRigidbody")]
	public static void ClearColliderRigidbody(){

		Transform sel = Selection.activeTransform;
		GameObject.DestroyImmediate(sel.gameObject.GetComponent<HingeJoint> ());
		GameObject.DestroyImmediate(sel.gameObject.GetComponent<Rigidbody> ());
		foreach (Transform t in sel) {
			Transform[] ts = t.GetComponentsInChildren<Transform> ();
			foreach (Transform tt in ts) {
				GameObject.DestroyImmediate(tt.gameObject.GetComponent<HingeJoint> ());
				GameObject.DestroyImmediate(tt.gameObject.GetComponent<Rigidbody> ());
				GameObject.DestroyImmediate(tt.gameObject.GetComponent<Collider> ());


			}
		}
	}
}
