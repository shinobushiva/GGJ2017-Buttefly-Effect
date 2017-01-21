using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class i18nSingle
{
	public string lang;
	public List<i18nSingle.Entry> properties = new List<i18nSingle.Entry> ();
		
	[System.Serializable]
	public class Entry
	{
		public string key;
		public object value;
		public DataType type;
		
		public Entry ()
		{
		}
		
		public Entry (string k, object v, DataType t)
		{
			key = k;
			value = v;
			type = t;
		}
	}
	
}
