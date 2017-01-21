using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkirtFlipper : MonoBehaviour {

	public Cloth cloth;

	public Transform flipperPrefab;
	public int numFlippers = 1;

	public float radius = 0f;
	public float upRatio = 0;

	private float currentRadius = 0f;

	public float speed = 1f;

	public Vector3 first = Vector3.zero;
	public Vector3 follow = Vector3.zero;

	private Transform[] flippers;
	private ClothSphereColliderPair[] pairs;
	private GameObject flipperGroup;

	public InputField input;

	// Use this for initialization
	void Start () {
		flippers = new Transform[numFlippers];
		pairs = new ClothSphereColliderPair[numFlippers];

		flipperGroup = new GameObject ("FlipperGroup");
		flipperGroup.transform.position = transform.position;

		for (int i = 0; i < numFlippers; i++) {
			flippers [i] = Instantiate (flipperPrefab, transform.position, transform.rotation) as Transform;
			flippers [i].Rotate (Vector3.up * 360f / numFlippers * i);
			flippers [i].transform.SetParent (transform);
			flippers [i].GetComponent<Renderer> ().enabled = false;

			pairs[i] = new ClothSphereColliderPair (flippers [i].GetComponent<SphereCollider> ());
		}
		cloth.sphereColliders = pairs;

	}
	
	// Update is called once per frame
	void Update () {
//		FlipSkirt ();
	}

	public void FlipSkirt(){
		float f = float.Parse (input.text);
		radius = f;
		StopAllCoroutines ();
		StartCoroutine(MoveFlipper(currentRadius, radius));
			
	}

	private IEnumerator MoveFlipper(float f, float t){

		{
			float startTime = Time.time;
			float time = Time.time + speed;
			while (time > Time.time) {

				float r = Mathf.Lerp (f, t, (Time.time - startTime) / speed);
				for (int i = 0; i < numFlippers; i++) {
					flippers [i].transform.position 
				= transform.position + flippers [i].transform.forward * r
					+ first * r;
				}

				yield return new WaitForFixedUpdate ();
			}
		}

		{
			float time = Time.time + speed * 5;
			while (time > Time.time) {
				for (int i = 0; i < numFlippers; i++) {
					flippers [i].transform.position +=  
						follow * Time.deltaTime;
				}

				yield return new WaitForFixedUpdate ();
			}
		}

		cloth.sphereColliders = new ClothSphereColliderPair[0];
		yield return new WaitForFixedUpdate ();

		for (int i = 0; i < numFlippers; i++) {
			flippers [i].transform.position = transform.position;
		}

		yield return new WaitForFixedUpdate ();
		cloth.sphereColliders = pairs;


	}


}
