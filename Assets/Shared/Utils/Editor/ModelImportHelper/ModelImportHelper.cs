using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class ModelImportHelper : Editor {

	private static string objectNameToAlign = "Plane001";
//	private static string[] objectNamesToDelete = {"g_3dboard","Plane001"};

	[MenuItem("Custom/Align Models")]
	public static void AlignModels(){
		Debug.Log ("Align Models");

		Transform[] transformes = GameObject.FindObjectsOfType<Transform> ();
		foreach (Transform trans in transformes) {
			if(trans.name == ModelImportHelper.objectNameToAlign){
				Vector3 pos = trans.position;
				Transform root = trans.parent;
				root.Translate(-pos);
			}
//			if(objectNamesToDelete.Contains(trans.name)){
//				DestroyImmediate(trans.gameObject);
//			}
		}

	}

	[MenuItem("Custom/Navmesh/Set Not Walkable")]
	public static void SetNotWalkable(){
		GameObject selected = Selection.activeGameObject;
//		MeshFilter[] mfs = selected.GetComponentsInChildren<MeshFilter> ();
//		foreach (MeshFilter mf in mfs) {
//			if(!mf.gameObject.GetComponent<Collider>())
//				mf.gameObject.AddComponent<MeshCollider>();
//		}
	}

	[MenuItem("Custom/Add Mesh Collider")]
	public static void AddMeshCollider(){
		GameObject selected = Selection.activeGameObject;
		MeshFilter[] mfs = selected.GetComponentsInChildren<MeshFilter> ();
		foreach (MeshFilter mf in mfs) {
			if(!mf.gameObject.GetComponent<Collider>())
				mf.gameObject.AddComponent<MeshCollider>();
		}
	}

	[MenuItem("Custom/Create Bounding Cube")]
	public static void CreateBoundingCube(){
		GameObject selected = Selection.activeGameObject;
		Bounds b1 = Helper.GetBoundingBox (selected);
		Quaternion rot = selected.transform.rotation;
		selected.transform.rotation = Quaternion.identity;
		Bounds b = Helper.GetBoundingBox (selected);
		selected.transform.rotation = rot;

		GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
		cube.transform.localScale = b.extents*2;
		cube.transform.position = b1.center;
		cube.transform.rotation = rot;
		cube.name = selected.name+"_BBox";

		Material m = null;

		string[] ass = AssetDatabase.FindAssets ("Wireframe");
		foreach (string a in ass) {
			Debug.Log(a);
			string path = AssetDatabase.GUIDToAssetPath(a);
			Debug.Log (path);
			if(path.EndsWith(".mat")){
				m = AssetDatabase.LoadAssetAtPath<Material>(path);
				break;
			}
		}
		cube.GetComponent<Renderer> ().sharedMaterial = m;
		if (selected.transform.parent != null) {
			cube.transform.SetParent(selected.transform.parent, true);
		}


	}

	[MenuItem("Custom/Remove Collider")]
	public static void RemoteCollider(){
		GameObject selected = Selection.activeGameObject;
		Collider[] mfs = selected.GetComponentsInChildren<Collider> ();
		foreach (Collider mf in mfs) {
			DestroyImmediate(mf);
		}
	}
}
