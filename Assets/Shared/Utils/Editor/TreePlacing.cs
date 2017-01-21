using UnityEngine;
using UnityEditor;
using System.Collections;

public class TreePlacing : EditorWindow {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	[MenuItem ("Custom/Tree Placing Window")]
	static void ShowWindow () {
		EditorWindow.GetWindow<TreePlacing>();
	}

	private string targetName;

	private GameObject[] trees;

	void OnGUI () {

		GameObject[] gos = Selection.gameObjects;

		GUILayout.Label (""+gos.Length+" selected", EditorStyles.boldLabel);
		targetName = EditorGUILayout.TextField ("Target Name", targetName);
		 

		if (GUILayout.Button ("Place")) {

			GameObject par = new GameObject("Placed Trees");

			GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>() ;
			foreach(GameObject go in allObjects){
				GameObject[] goss = Selection.gameObjects;

				if(go.name.StartsWith(targetName)){
					GameObject g = Instantiate(goss[Random.Range (0, goss.Length)]);
					Vector3 v =  go.transform.position;
					v.y = 0;
					g.transform.position = v;
					g.transform.SetParent(par.transform, true);
				}
			}
		}
		
//		groupEnabled = EditorGUILayout.BeginToggleGroup ("Optional Settings", groupEnabled);
//		myBool = EditorGUILayout.Toggle ("Toggle", myBool);
//		myFloat = EditorGUILayout.Slider ("Slider", myFloat, -3, 3);
//		EditorGUILayout.EndToggleGroup ();
	}
}
