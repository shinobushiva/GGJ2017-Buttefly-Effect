using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO; 
using System.Globalization;
using System;

public enum DataType
{
	StringType,
	TextureType,
	AudioType
}

public enum i18nLocale
{
	zh_CHS,
	ar_SA,
	bg_BG,
	ca_ES,
	zh_TW,
	cs_CZ,
	da_DK,
	de_DE,
	el_GR,
	en_US,
	fi_FI,
	fr_FR,
	he_IL,
	hu_HU,
	is_IS,
	it_IT,
	ja_JP,
	ko_KR,
	nl_NL,
	nb_NO,
	pl_PL,
	pt_BR,
	ro_RO,
	ru_RU,
	hr_HR,
	sk_SK,
	sq_AL,
	sv_SE,
	th_TH,
	tr_TR,
	id_ID,
	uk_UA,
	be_BY,
	sl_SI,
	et_EE,
	lv_LV,
	lt_LT,
	fa_IR,
	vi_VN,
	hy_AM,
	eu_ES,
	mk_MK,
	af_ZA,
	ka_GE,
	fo_FO,
	hi_IN,
	sw_KE,
	gu_IN,
	ta_IN,
	te_IN,
	kn_IN,
	mr_IN,
	gl_ES,
	kok_IN,
	ar_IQ,
	zh_CN,
	de_CH,
	en_GB,
	es_MX,
	fr_BE,
	it_CH,
	nl_BE,
	nn_NO,
	pt_PT,
	sv_FI,
	ar_EG,
	zh_HK,
	de_AT,
	en_AU,
	es_ES,
	fr_CA,
	ar_LY,
	zh_SG,
	de_LU,
	en_CA,
	es_GT,
	fr_CH,
	ar_DZ,
	zh_MO,
	en_NZ,
	es_CR,
	fr_LU,
	ar_MA,
	en_IE,
	es_PA,
	ar_TN,
	en_ZA,
	es_DO,
	ar_OM,
	es_VE,
	ar_YE,
	es_CO,
	ar_SY,
	es_PE,
	ar_JO,
	en_TT,
	es_AR,
	ar_LB,
	en_ZW,
	es_EC,
	ar_KW,
	en_PH,
	es_CL,
	ar_AE,
	es_UY,
	ar_BH,
	es_PY,
	ar_QA,
	es_BO,
	es_SV,
	es_HN,
	es_NI,
	es_PR,
	zh_CHT
}

public static class DataTypeExt
{
	
	public static string ToStringExt (this DataType e)
	{
		switch (e) {
		case DataType.StringType:
			return "string";
		case DataType.TextureType:
			return "Texture2D";
		case DataType.AudioType:
			return "AudioClip";
		default:
			throw new ArgumentOutOfRangeException ("e");
		}
	}
}

public class i18n
{
	private static i18nDictionary dictionary;
	private static Dictionary<string, Dictionary<string, object>> d = new Dictionary<string, Dictionary<string, object>> ();
	public static string locale = CultureInfo.CurrentCulture.Name;
	public static string defaultLang = null;
	
	static i18n ()
	{
		
		//Debug.Log (locale);
		/*
		CultureInfo[] cs = CultureInfo.GetCultures (CultureTypes.AllCultures);
		foreach (CultureInfo c in cs) {
			Debug.Log (c.EnglishName);
			Debug.Log (c.TwoLetterISOLanguageName);
			Debug.Log (c.Name);
			Debug.Log (c.DisplayName);
		}
		*/

		
		XmlSerializer r = new XmlSerializer (typeof(i18nDictionary));

		TextAsset bindata = Resources.Load("Messages") as TextAsset; 
		XmlTextReader xr = new XmlTextReader(new StringReader(bindata.text));
			
		dictionary = (i18nDictionary)r.Deserialize (xr);
		
		foreach (i18nSingle single in dictionary.list) {
			string lang = single.lang;
			if (defaultLang == null)
				defaultLang = lang;
			Dictionary<string, object> entry = new Dictionary<string, object> ();
			d [lang] = entry;
			foreach (i18nSingle.Entry e in single.properties) {
#if UNITY_EDITOR
				if (e.type == DataType.TextureType) {
					Texture2D t2d = (Texture2D)UnityEditor.AssetDatabase.LoadAssetAtPath ((string)e.value, typeof(Texture2D));
					//Debug.Log ("Texture : " + t2d);
					entry [e.key] = t2d;
				} else if (e.type == DataType.AudioType) {
					AudioClip t2d = (AudioClip)UnityEditor.AssetDatabase.LoadAssetAtPath ((string)e.value, typeof(AudioClip));
					entry [e.key] = t2d;
				} else 
#endif
				{
					entry [e.key] = e.value;
				}
			}
		}

	}
	
	private static bool e (object o)
	{
		if (o == null) {
			return false;
		}
		
		if (o.GetType () == typeof(string)) {
			return ((string)o).Length != 0;
		}
		
		return true;
	}
	
	protected static T Get<T> (string key)
	{
		//Debug.Log (key);
		if (d.ContainsKey (locale) && e (d [locale] [key])) {
			return (T)d [locale] [key];
		} else {
			return (T)d [defaultLang] [key];
		}
	
	}
	
	public static T t<T> (string key)
	{
		return Get<T> (key); 
	}
}
