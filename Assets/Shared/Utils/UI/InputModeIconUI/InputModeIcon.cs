using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Shiva.UI {
	public class InputModeIcon : MonoBehaviour {

		public Image image;

		private Dictionary<string, Entry> dict = new Dictionary<string, Entry>();

		[System.Serializable]
		public class Entry {
			public string name;
			public Sprite sprite;
		}
		public List<Entry> entries;

		public void SetMode(string mode){
			if(dict.ContainsKey(mode)){
				image.sprite = dict[mode].sprite;
			}
		}

		// Use this for initialization
		void Awake () {
			foreach(Entry e in entries){
				dict.Add(e.name, e);
			}
		}
		
		// Update is called once per frame
		void Update () {
		
		}
	}
}