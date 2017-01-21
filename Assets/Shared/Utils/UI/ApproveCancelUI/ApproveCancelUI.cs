using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.Events;

public class ApproveCancelUI : MonoBehaviour {

    public Button approveButton;
    public Button cancelButton;
    public Text text;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	} 

    public void Set(string text, string approve, string cancel, UnityAction approveAction, UnityAction cancelAction){
        approveButton.onClick.RemoveAllListeners();
        approveButton.onClick.AddListener(approveAction);
        cancelButton.onClick.RemoveAllListeners();
        cancelButton.onClick.AddListener(cancelAction);

        this.text.text = text;
        approveButton.GetComponentInChildren<Text>().text = approve;
        cancelButton.GetComponentInChildren<Text>().text = cancel;

    }
}
