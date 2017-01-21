using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic; 
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System;
using System.Runtime.Serialization;
using System.Globalization;

public class i18nWindow : EditorWindow
{
	
	public GUISkin skin;
	
	[MenuItem ("i18n/i18n Window")]
	static void ShowWindow ()
	{
		EditorWindow.GetWindow<i18nWindow> ("Localization");
		
	}
	
	[System.Serializable]
	public class Entry
	{
		public DataType t = DataType.StringType;
		public string property;
		public object[] values = new object[0];
	}
	
	//
	private object flag;
	
	//
	public string[] headers;
	public List<Entry>  entries ;
	public Dictionary<Rect, int> cellNumRect = new Dictionary<Rect, int> ();
	
	//
	public List<Rect> controlRects = new List<Rect> ();
	
	//
	private const int CELL_WIDTH = 120;
	//
	private int focusedControl = -1;
	private int columnCount = 0;
	
	//
	private Vector2 scrollPosition;
	
	private int GetControlNum (Vector2 pos)
	{
		pos += scrollPosition;
		int i = 0;
		foreach (Rect r in controlRects) {
			if (r.Contains (pos))
				return i;
			i++;
		}
		return -1;
	}

	void OnGUI ()
	{
		
		if (flag == null) {
			
			//it's not used now
			skin = (GUISkin)AssetDatabase.LoadAssetAtPath ("Assets/i18n/Editor/i18nWindowSkin.guiskin", typeof(GUISkin));
			Debug.Log (skin);  
			
			flag = "flag";
			Load ();
		}
		
		
		//GUI.skin = skin;
		
		EditorGUILayout.BeginVertical ();//Headers
		
		controlRects.Clear ();
		cellNumRect.Clear ();
		//Draw headers
		EditorGUILayout.BeginHorizontal ();
		for (int i=0; i<headers.Length; i++) {
			
			EditorGUILayout.BeginHorizontal (GUILayout.Width (CELL_WIDTH));
			if (i > 1 && focusedControl == i) {
				//GUI.SetNextControlName ("Editing:" );
				//headers [i] = GUILayout.TextField (headers [i]);
				i18nLocale i18 = (i18nLocale)Enum.Parse (typeof(i18nLocale), headers [i]);
				headers [i] = EditorGUILayout.EnumPopup (i18).ToString ();
				controlRects.Add (GUILayoutUtility.GetLastRect ());
				//GUI.FocusControl ("Editing:" );
			} else {
				
				if (i > 1) {
					if (GUILayout.Button ("-", GUILayout.Width (20))) {
				
						ArrayUtility.RemoveAt<string> (ref headers, i);
						foreach (Entry e in entries) {
							ArrayUtility.RemoveAt<object> (ref e.values, i - 2);
						}
						focusedControl = -1;
						columnCount = headers.Length;
						Repaint ();
						return;
					}
				}
				
				GUILayout.Label (headers [i], EditorStyles.boldLabel);
				controlRects.Add (GUILayoutUtility.GetLastRect ());
			}
			EditorGUILayout.EndHorizontal ();
		}
		if (GUILayout.Button ("+", GUILayout.Width (CELL_WIDTH))) {
			ArrayUtility.Add<string> (ref headers, i18nLocale.en_US.ToString ());
			foreach (Entry e in entries) {
				if (e.t == DataType.StringType) {
					ArrayUtility.Add<object> (ref e.values, "");
				} else {
					ArrayUtility.Add<object> (ref e.values, null);
				}
			}
			
			focusedControl = -1;
			columnCount = headers.Length;
			Repaint ();
			return;
		}
		EditorGUILayout.EndHorizontal (); //Headers
		Rect offset = GUILayoutUtility.GetLastRect ();
		
		//Draw table 
		scrollPosition = EditorGUILayout.BeginScrollView (scrollPosition);
		int focusedControlCounter = headers.Length;
		
		for (int i=1; i<entries.Count; i++) {
			Entry entry = entries [i];
			
			
			EditorGUILayout.BeginHorizontal ();
			//Draw Propety name
			{
				if (focusedControl == focusedControlCounter) {
					entry.property = GUILayout.TextField (entry.property); 
				} else {
				
					EditorGUILayout.BeginHorizontal (GUILayout.Width (CELL_WIDTH));
					if (GUILayout.Button ("-", GUILayout.Width (20))) {
						entries.RemoveAt (i);
					}
					GUILayout.Label (entry.property, EditorStyles.boldLabel, GUILayout.MaxWidth (80));
					EditorGUILayout.EndHorizontal ();
				}
				Rect r = GUILayoutUtility.GetLastRect ();
				r.y += offset.y + offset.height;
				controlRects.Add (r);
				cellNumRect [r] = focusedControlCounter++;
			}
			//Draw Types
			{
				if (focusedControl == focusedControlCounter) {
					//entry.property = GUILayout.TextField (entry.t.Name); 
					//GUILayout.Label (entry.t.ToStringExt (), EditorStyles.boldLabel, GUILayout.MaxWidth (CELL_WIDTH));
					DataType dt = entry.t;
					entry.t = (DataType)EditorGUILayout.EnumPopup (entry.t, GUILayout.MaxWidth (CELL_WIDTH));
					if (entry.t != dt) {
						entry.values = new object[entry.values.Length];
						if (entry.t == DataType.StringType) {
							for (int k=0; k<entry.values.Length; k++)
								entry.values [k] = "";
						}
					}
				} else {
					GUILayout.Label (entry.t.ToStringExt (), EditorStyles.boldLabel, GUILayout.MaxWidth (CELL_WIDTH));
				}
				
				Rect r = GUILayoutUtility.GetLastRect ();
				r.y += offset.y + offset.height;
				controlRects.Add (r);
				cellNumRect [r] = focusedControlCounter++;
				
			}
			
			//Draw Values
			for (int j=0; j<entry.values.Length; j++) {
				
				if (entry.t == DataType.StringType) {
					if (focusedControl == focusedControlCounter) {
						entry.values [j] = GUILayout.TextField ((string)entry.values [j]); 
					} else {
						GUILayout.Label ((string)entry.values [j], EditorStyles.label, GUILayout.Width (CELL_WIDTH));
					}
				} else if (entry.t == DataType.TextureType) {
					entry.values [j] = (Texture2D)EditorGUILayout.ObjectField ((Texture2D)entry.values [j], typeof(Texture2D), true, GUILayout.Width (CELL_WIDTH));
				} else if (entry.t == DataType.AudioType) {
					entry.values [j] = (AudioClip)EditorGUILayout.ObjectField ((AudioClip)entry.values [j], typeof(AudioClip), true, GUILayout.Width (CELL_WIDTH));
				}
				Rect r = GUILayoutUtility.GetLastRect ();
				r.y += offset.y + offset.height;
				controlRects.Add (r);
				cellNumRect [r] = focusedControlCounter++;
			}
			EditorGUILayout.EndHorizontal ();
		}
		
		
		GUILayout.Space (10);
		if (GUILayout.Button ("+", GUILayout.Width (CELL_WIDTH))) {
			{
				Entry entry = new Entry ();
				entry.t = DataType.StringType;
				entry.property = "new_proerty";
				for (int i=0; i<columnCount-2; i++) {
					
					ArrayUtility.Add<object> (ref entry.values, "");
				}
				entries.Add (entry);
			}
			
			focusedControl = -1;
			Repaint ();
			return;
		}
		EditorGUILayout.EndScrollView ();
		
		EditorGUILayout.EndVertical ();
		
		GUILayout.FlexibleSpace ();
		
		//Save, Load button
		EditorGUILayout.BeginHorizontal ();
		if (GUILayout.Button ("Update")) {
			focusedControl = -1;
			Save ();
			Repaint ();
			
		}
		if (GUILayout.Button ("Load")) {
			focusedControl = -1;
			Load ();
			Repaint ();
		}
		EditorGUILayout.EndHorizontal ();
		
		Event ev = Event.current;
		
		if (ev.type == EventType.MouseUp) {
			//Debug.Log ("" + ev.mousePosition); 
			focusedControl = GetControlNum (ev.mousePosition); 
			//Debug.Log ("FC : " + focusedControl);
			Event.current.Use ();
			Repaint ();
		}
		
		if (ev.type == EventType.KeyUp) {
			
			//Debug.Log (ev.keyCode);
			if (ev.keyCode == KeyCode.Return) {
				if (focusedControl >= 0) {
					focusedControl = -1;
				}
				Event.current.Use ();
				Repaint ();
			}
			
			
			if (ev.keyCode == KeyCode.Tab) { 
				int skipNum = 1;
				if (ev.alt)
					skipNum = columnCount;
					
				int num = columnCount * entries.Count;
				if (ev.shift) {
					focusedControl = (num + focusedControl - skipNum) % num;
				} else {
					focusedControl = (focusedControl + skipNum) % num;
				}
				Event.current.Use ();
			}
		}
		
	}
	
	private void Load ()
	{ 	
		
		
		if (!File.Exists (Application.dataPath + "/i18n/Resources/Messages.xml")) {
			
			Debug.Log ("Initializeing");
			headers = new string[] {"Property","Type", "en_US", "ja_JP"};
			entries.Clear ();
			columnCount = headers.Length;
			{
				Entry entry = new Entry ();
				entry.t = DataType.StringType;
				entry.property = "Property";
				for (int i=1; i<headers.Length; i++) {
					ArrayUtility.Add<object> (ref entry.values, headers [i]);
				}
				entries.Add (entry);
			}
		
			{
				Entry entry = new Entry ();
				entry.t = DataType.StringType;
				entry.property = "new_proerty";
				ArrayUtility.Add<object> (ref entry.values, "test");
				ArrayUtility.Add<object> (ref entry.values, "testA");
				entries.Add (entry);
			}{
				Entry entry = new Entry ();
				entry.t = DataType.StringType;
				entry.property = "new_proerty2";
				ArrayUtility.Add<object> (ref entry.values, "test2");
				ArrayUtility.Add<object> (ref entry.values, "test2A");
				entries.Add (entry);
			}
			return;
		} else {
		
			XmlSerializer r = new XmlSerializer (typeof(i18nDictionary));
		
			XmlTextReader xr = new XmlTextReader ("file://" + Application.dataPath + "/i18n/Resources/Messages.xml");
			
			i18nDictionary dictionary = (i18nDictionary)r.Deserialize (xr);
			xr.Close ();
		
			List<string> headers = new List<string> ();
			headers.Add ("Property");
			headers.Add ("Type");
			
			
		
			Dictionary<string, List<i18nSingle.Entry>> dic = new Dictionary<string, List<i18nSingle.Entry>> ();
			foreach (i18nSingle single in dictionary.list) {
				string lang = single.lang;
				headers.Add (lang);
			
				foreach (i18nSingle.Entry e in single.properties) {
					if (!dic.ContainsKey (e.key)) {
						dic [e.key] = new List<i18nSingle.Entry> ();
					}
					dic [e.key].Add (e);
				}
			}
			entries = new List<Entry> ();
			/*
			{
				Entry entry = new Entry ();
				entry.t = typeof(string);
				entry.property = "Property";
				foreach (string v in headers) {
					entry.values.Add (v);
				}
				entries.Add (entry);
			}
			*/
		
			foreach (string p in dic.Keys) {
				List<i18nSingle.Entry> ents = dic [p];
				Entry e = new Entry ();
				foreach (i18nSingle.Entry entry in ents) {
					e.t = entry.type;
					e.property = entry.key;
					if (e.t == DataType.TextureType) {
						Texture2D t2d = (Texture2D)AssetDatabase.LoadAssetAtPath ((string)entry.value, typeof(Texture2D));
						ArrayUtility.Add<object> (ref e.values, t2d);
					} else if (e.t == DataType.AudioType) {
						AudioClip t2d = (AudioClip)AssetDatabase.LoadAssetAtPath ((string)entry.value, typeof(AudioClip));
						ArrayUtility.Add<object> (ref e.values, t2d);
					} else {
						ArrayUtility.Add<object> (ref e.values, entry.value);
					}
				}
				entries.Add (e);
			}
		
			this.headers = headers.ToArray ();
			columnCount = this.headers.Length;
		}
	}
	
	private void Save ()
	{ 
		i18nDictionary i18n = new i18nDictionary ();
		for (int j=2; j<headers.Length; j++) {
			string lang = headers [j];
			
			i18nSingle single = new i18nSingle ();
			single.lang = lang;
			i18n.list.Add (single);
			
			
			for (int i=0; i<entries.Count; i++) {
				string k = entries [i].property;
				object v = entries [i].values [j - 2];
				DataType t = entries [i].t;
				
				if (v != null && v.GetType () == typeof(Texture2D)) {
					Texture2D t2d = (Texture2D)v;
					string path = AssetDatabase.GetAssetPath (t2d.GetInstanceID ());
					v = path;
				} else if (v != null && v.GetType () == typeof(AudioClip)) {
					AudioClip t2d = (AudioClip)v;
					string path = AssetDatabase.GetAssetPath (t2d.GetInstanceID ());
					v = path;
				}
				
				single.properties.Add (new i18nSingle.Entry (k, v, t));
			}
		}
		 
	
		XmlSerializer serializer = new XmlSerializer (typeof(i18nDictionary));
	
		
		if (!Directory.Exists (Application.dataPath + "/i18n/Resources")) 
			AssetDatabase.CreateFolder ("Assets", "i18n/Resources");
		XmlTextWriter w = new XmlTextWriter ("Assets/i18n/Resources/Messages.xml", Encoding.UTF8);
		serializer.Serialize (w, i18n);
		w.Flush ();
		w.Close ();
		
		AssetDatabase.Refresh ();
		//AssetDatabase.ImportAsset ("Assets/i18n/ Messages.xml");
		
		//new i18nText ();
		CreateClass ();
		
	}
	
	private void CreateClass ()
	{
		StringBuilder classText = new StringBuilder ();
		classText.AppendLine ("using UnityEngine;");
		classText.AppendLine ("public class R : i18n");
		classText.AppendLine ("{");
		

		for (int i=1; i<entries.Count; i++) {
		
			string variableName = entries [i].property;
			string type = entries [i].t.ToStringExt ();
			
			classText.AppendLine ("  public static " + type + " " + "@" + variableName + " {");
			classText.AppendLine ("    get { return i18n.Get<" + type + ">(\"" + variableName + "\"); }");
			classText.AppendLine ("  }");
			classText.AppendLine ("");
		}

		classText.AppendLine ("}");
		
		//Debug.Log (classText.ToString ());
		
		TextWriter w = new StreamWriter ("Assets/i18n/Resources/R.cs");
		w.Write (classText.ToString ());
		w.Flush ();
		w.Close ();
		
		AssetDatabase.Refresh ();
		
	}
}
