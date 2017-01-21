using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SliderText : MonoBehaviour {

    public Text valueText;

    public int floatingAt = 2;

	// Use this for initialization
	void Start () {
			valueText.text = "" + GetComponent<Slider> ().value;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetValue(float f){
       valueText.text = ""+((int)((f * Mathf.Pow(10, 2))) / Mathf.Pow(10, 2));
    }
}
