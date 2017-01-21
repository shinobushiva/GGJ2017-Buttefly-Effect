using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using System.Text;
using System.Xml.Serialization;


namespace DataSaveLoad{
	public class LoadDataUI : MonoBehaviour {

		private string folder;

		public RectTransform scrollContent;
		public DataEntry prefab;

		private DataSaveLoadMaster manager;
		public DataSaveLoadMaster Manager{
			get{
				if(!manager){
					manager = GameObject.FindObjectOfType<DataSaveLoadMaster> ();
					if (!manager) {
						print ("CameraSaveLoadManager is missing!");
						Destroy(this);
					}
				}
				return manager;
			}
		}
//		private ConfirmDialogUI confirmDialogUI;


		private System.Type type;

		// Use this for initialization
		void Awake () {
			
//			confirmDialogUI = GameObject.FindObjectOfType<ConfirmDialogUI> ();
		}

		public void UpdateView(){
			DataEntry[] cdes = scrollContent.GetComponentsInChildren<DataEntry> (true);
			foreach (DataEntry cde in cdes) {
				DestroyImmediate(cde.gameObject);
			}
			
			string folderPath = Manager.GetFolderPath(folder);
			if (!Directory.Exists (folderPath)) {
				Directory.CreateDirectory(folderPath);
			}
			string[] files = Directory.GetFiles (folderPath);
			
			foreach (string f in files) {
				print (f);
				DataEntry cde = GameObject.Instantiate(prefab) as DataEntry;
				cde.transform.SetParent(scrollContent.transform, false);
				cde.Set(new FileInfo(f));
				cde.loadDataUI = this;
			}
		}

		public void Load(FileInfo fi){
			Manager.Load (fi, type);
		}

		public void ShowDialog(System.Type t, string folder){
			this.type = t;
			this.folder = folder;

			gameObject.SetActive (true);
			UpdateView ();
		}
		
		// Update is called once per frame
		void Update () {
		
		}
	}
}