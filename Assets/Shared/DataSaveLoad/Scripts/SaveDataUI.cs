using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;


namespace DataSaveLoad {
	public class SaveDataUI : MonoBehaviour {

		private string folder;

		public InputField fileName;

		public DataSaveLoadMaster manager;
		private ConfirmDialogUI confirmDialogUI;

		public object data;

		// Use this for initialization
		void Awake () {

			if (!manager) {
				print ("DataSaveLoadManager is missing!");
				Destroy(this);
			}

			confirmDialogUI = GameObject.FindObjectOfType<ConfirmDialogUI> ();
			if (!confirmDialogUI) {
				print ("Confirm Dialog UI is missing!");
				Destroy(this);
			}
		}

		// Update is called once per frame
		void Update () {
		
		}

		public void ShowDialog(object o, string folder){
			this.data = o;
			this.folder = folder;
			gameObject.SetActive (true);
		}

		public void Approved(){
			Approved (false);
		}

		public void Approved(bool forseOverride){
			Approved (forseOverride, this.folder);
		}

		public void Approved(bool forseOverride, string folder){

			//Application.persistentDataPath
			string folderPath = manager.GetFolderPath (folder);
			if (!Directory.Exists (folderPath)) {
				Directory.CreateDirectory(folderPath);
			}

			string filePath = manager.GetFilePath(fileName.text, folder);
			if (!forseOverride && File.Exists (filePath)) {

				confirmDialogUI.Show ("The record already exists",
			                      "The record will be overridden. Do you really want to do it?",
			                      "Yes, I do", "No, I don't", 
			                      (b) => {
					if (b) {
						print ("overridden");
						manager.WriteFile(filePath, data, data.GetType());
						gameObject.SetActive(false);
					} else {
						print ("wasn't saved"); 
					}
				});
			} else {
				if(manager)
					manager.WriteFile(filePath, data, data.GetType());
				if(gameObject)
					gameObject.SetActive(false);
			}
		}

		void OnDisable(){
			print ("Save Data UI Destroied");
		}


		public void Canceled(){
			data = null;
			gameObject.SetActive (false);
		}
	}
}
