using UnityEngine;
using System.Collections;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
	public bool dontDestroyOnLoad = false;
	public bool destroyGameObject = false;

	private static T instance;

	public static T Instance {
		get {
			if (instance == null) {
				instance = (T)FindObjectOfType (typeof(T));
 
				if (instance == null) {
					//Debug.Log (typeof(T) + " is nothing");
				}
			}
 
			return instance;
		}
	}
	
	protected void Awake ()
	{
		CheckInstance ();
		if (dontDestroyOnLoad) {
			DontDestroyOnLoad (gameObject);
		}
	}
	
	protected bool CheckInstance ()
	{
		if (this == Instance) {
			return true;
		}
		if(destroyGameObject)
			Destroy (gameObject);
		else
			Destroy (this);
		
		return false;
	}
}
