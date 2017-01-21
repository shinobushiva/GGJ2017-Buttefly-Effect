using UnityEngine;
using System.Collections;

public class FollowUI : MonoBehaviour {

    public Transform target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
        Vector3 v = Camera.main.WorldToScreenPoint(target.position);
        //v.y = Screen.height - v.y;

        GetComponent<RectTransform>().position = v;
        print(v);
	}
}
