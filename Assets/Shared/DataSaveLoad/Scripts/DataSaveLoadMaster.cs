using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace DataSaveLoad{
	public class DataSaveLoadMaster : MonoBehaviour {

//		private string folder;

		public SaveDataUI saveDataUI;
		public LoadDataUI loadDataUI;

		public delegate void DataLoadHandler(object data);
//		public event DataLoadHandler<T> dataLoadHandler;
		private Dictionary<System.Type, DataLoadHandler> handlerMap = new Dictionary<System.Type, DataLoadHandler> ();

		public void AddHandler(DataLoadHandler dlh, System.Type t){
			if (!handlerMap.ContainsKey (t)) {
				handlerMap.Add(t, dlh);
			}
		}

		// Use this for initialization
		void Start () {
		
		}


		public void ShowSaveDialog(object data, string folder){
			saveDataUI.ShowDialog (data, folder);
		}

		public void ShowLoadDialog(System.Type t, string folder){
			loadDataUI.ShowDialog (t, folder);
		}

		public string GetFolderPath(string folder){


			return  string.Format("{0}/{1}/{2}", Application.persistentDataPath , folder, 
					UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
		}
		
		public string GetFilePath(string fname, string folder){
			return  string.Format("{0}/{1}", GetFolderPath(folder) , fname);
		}

		public void WriteFile(string path, object obj, System.Type t){
			
			//print (t);
			XmlSerializer ser = new XmlSerializer (t);
			
			//書き込むファイルを開く（UTF-8 BOM無し）
			StringBuilder sb = new StringBuilder ();
			StringWriter sw = new StringWriter(sb);
			ser.Serialize(sw, obj);
			
			print (path);
			File.WriteAllBytes(path, Encoding.UTF8.GetBytes(sw.ToString()));
		}

		public void Load(string folder, string file, System.Type t, DataLoadHandler handler){			
			Load (new FileInfo(GetFilePath (file, folder)), t, handler);
		}

		public void Load(FileInfo fi, System.Type t){
			Load (fi, t, handlerMap [t]);
		}

		public void Load(FileInfo fi, System.Type t, DataLoadHandler handler){
			string fn = fi.FullName;
			print (fn);

			StreamReader sr = new StreamReader(fn, new System.Text.UTF8Encoding(false));
			XmlSerializer ser = new XmlSerializer (t);
			object obj = ser.Deserialize (sr);
			sr.Close ();

			handler (obj);
		}

		public void Save(string file, string folder, object data){
			saveDataUI.fileName.text = file;
			saveDataUI.data = data;
			saveDataUI.Approved (true, folder);
		}

		public void Delete(string file, string folder){
			FileInfo fi = new FileInfo (GetFilePath (file, folder));
			fi.Delete ();
		}

		public string createDatetimedFileName(string prefix = "", string postfix=""){
			return string.Format (@"{0}{1}{2}",prefix, System.DateTime.Now.Ticks, postfix);
		}

	}
}
