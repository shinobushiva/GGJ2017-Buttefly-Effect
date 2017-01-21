using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SessionManager : MonoBehaviour
{

	private static SessionManager i;
	public Dictionary<string, object> session = new Dictionary<string, object> ();

	public static SessionManager Get ()
	{
		if (i == null) {
			GameObject g = new GameObject ("SessionManager");
			i = g.AddComponent<SessionManager> ();
			DontDestroyOnLoad (g);
		}
		return i;
	}

	public static Type V<Type> (string key){
		return V<Type> (key, default(Type));
	}

	public static Type V<Type> (string key, Type def)
	{
		if (!Get ().session.ContainsKey (key))
			return def;
		else
			return (Type)Get ().session [key];

	}

	public static void S<Type> (string key, Type v)
	{
		Get ().session [key] = v;
	}

	public static void Clear(){
		Get ().session.Clear ();
	}

	public static void D(string key){
		Get ().session.Remove (key);
	}
}
