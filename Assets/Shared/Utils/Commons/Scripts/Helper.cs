using UnityEngine;
using System.Collections;
using System.IO;

public static class Helper
{


	public static Bounds GetBoundingBox (GameObject go, bool rotationVariant = false)
	{

		Quaternion rot;
		if (rotationVariant) {
			rot = go.transform.rotation;
			go.transform.rotation = Quaternion.identity;
		}

		Renderer[] rs = go.GetComponentsInChildren<Renderer> ();
		if (rs.Length <= 0) {
			return new Bounds ();
		}

		Bounds b = GetBounds (rs [0]);
		for (int i = 1; i < rs.Length; i++) {
			b.Encapsulate (GetBounds (rs [i]));
		}

		return b;
	}

	public static void SetLayerRecursively (this GameObject obj, int layer)
	{
		obj.layer = layer;
		
		foreach (Transform child in obj.transform) {
			child.gameObject.SetLayerRecursively (layer);
		}
	}

	public static Bounds GetBounds (Renderer r)
	{
		
		if (r is SkinnedMeshRenderer) {
			SkinnedMeshRenderer smr = r as SkinnedMeshRenderer;
			Mesh mesh = smr.sharedMesh;

			Vector3[] vertices = mesh.vertices;
			if (vertices.Length <= 0) {
				return r.bounds;
			}
			int idx = 0;
			Vector3 min, max;
			min = max = r.transform.TransformPoint (vertices [idx++]);

			for (int i = idx; i < vertices.Length; i++) {
				Vector3 v = vertices [i];
				Vector3 V = r.transform.TransformPoint (v);
				for (int n = 0; n < 3; n++) {
					if (V [n] > max [n])
						max [n] = V [n];
					if (V [n] < min [n])
						min [n] = V [n];
				}
			}

			Bounds b = new Bounds ();;
			b.SetMinMax (min, max);
			return b;

		} else {
			return r.bounds;
		}
	}

	public static void OpenInMacFileBrowser(string path)
	{
		bool openInsidesOfFolder = false;

		// try mac
		string macPath = path.Replace("\\", "/"); // mac finder doesn't like backward slashes

		if (Directory.Exists(macPath)) // if path requested is a folder, automatically open insides of that folder
		{
			openInsidesOfFolder = true;
		}

		//Debug.Log("macPath: " + macPath);
		//Debug.Log("openInsidesOfFolder: " + openInsidesOfFolder);

		if (!macPath.StartsWith("\""))
		{
			macPath = "\"" + macPath;
		}
		if (!macPath.EndsWith("\""))
		{
			macPath = macPath + "\"";
		}
		string arguments = (openInsidesOfFolder ? "" : "-R ") + macPath;
		//Debug.Log("arguments: " + arguments);
		try
		{
			System.Diagnostics.Process.Start("open", arguments);
		}
		catch(System.ComponentModel.Win32Exception e)
		{
			// tried to open mac finder in windows
			// just silently skip error
			// we currently have no platform define for the current OS we are in, so we resort to this
			e.HelpLink = ""; // do anything with this variable to silence warning about not using it
		}
	}

	public static void OpenInWinFileBrowser(string path)
	{
		bool openInsidesOfFolder = false;

		// try windows
		string winPath = path.Replace("/", "\\"); // windows explorer doesn't like forward slashes

		if (Directory.Exists(winPath)) // if path requested is a folder, automatically open insides of that folder
		{
			openInsidesOfFolder = true;
		}
		try
		{
			System.Diagnostics.Process.Start("explorer.exe", (openInsidesOfFolder ? "/root," : "/select,") + winPath);
		}
		catch(System.ComponentModel.Win32Exception e)
		{
			// tried to open win explorer in mac
			// just silently skip error
			// we currently have no platform define for the current OS we are in, so we resort to this
			e.HelpLink = ""; // do anything with this variable to silence warning about not using it
		}
	}

	public static void OpenInFileBrowser(string path)
	{
		OpenInWinFileBrowser(path);
		OpenInMacFileBrowser(path);
	}


}
