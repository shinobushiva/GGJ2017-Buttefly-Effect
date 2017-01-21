using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour
{
	

	
	public i18nLocale lang = i18nLocale.en_US;

	// Use this for initialization
	void OnGUI ()
	{
		i18n.locale = lang.ToString ();

		//GUILayout.Label (R.hello, GUILayout.Width (Screen.width));
		
		//GUILayout.Label (R.image, GUILayout.Width (Screen.width));
	 
		
		/*
		if (GUILayout.Button (R.hello, GUILayout.Width (Screen.width))) {
			AudioSource.PlayClipAtPoint (R.sound, transform.position);
		}
		*/
		
		
	}
	
}
