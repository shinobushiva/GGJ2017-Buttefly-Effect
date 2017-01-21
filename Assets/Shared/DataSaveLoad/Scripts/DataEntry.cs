using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

namespace DataSaveLoad{
	public class DataEntry : MonoBehaviour {

		private FileInfo file;
		public Text filename;
		private GameObject callback;

		public LoadDataUI loadDataUI;

		// Use this for initialization
		void Start () {
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		public void Dispose(){
			file.Delete ();
			loadDataUI.UpdateView ();
		}

		public void Load(){
			loadDataUI.Load (file);
		}

		public void Set(FileInfo f){
			file = f;
 			filename.text = f.Name.Replace(".txt",  "");
		}
	}
}