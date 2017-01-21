using UnityEngine;
using System.Collections;

public class NetworkPosition : MonoBehaviour {

	public Vector3 myPosition;
	public Vector3 myScale;
	public Quaternion myRotate;
	// Use this for initialization
	void Start () {

		if (!Network.isServer && !Network.isClient) {
			Destroy(this);
			Destroy(GetComponent<NetworkView>());
		}

		myPosition = gameObject.transform.position;
		myRotate = gameObject.transform.rotation;
		myScale = gameObject.transform.localScale;
	}
	
	[RPC]
	void PositionChange(Vector3 posi){
		gameObject.transform.position = posi;
		myPosition = posi;
	}
	[RPC]
	void RotateChange(Quaternion Rotate){
		gameObject.transform.rotation = Rotate;
		myRotate = Rotate;
	}
	[RPC]
	void ScaleChange(Vector3 Scale){
		gameObject.transform.localScale = Scale;
		myScale = Scale;
	}
	// Update is called once per frame
	void Update () {

            //最初に自分の場所を記憶しておいてネットワークビュウが自分のものでなくかつ動いていたら自分以外の共有先を同期する
            if (myPosition != gameObject.transform.position)
            { 
                myPosition = gameObject.transform.position;
				
                GetComponent<NetworkView>().RPC("PositionChange", RPCMode.Others, myPosition);
            }
            if (myRotate != gameObject.transform.rotation)
            {
                myRotate = gameObject.transform.rotation;
			
                GetComponent<NetworkView>().RPC("RotateChange", RPCMode.Others, myRotate);
            }
            if (myScale != gameObject.transform.localScale)
            {
                myScale = gameObject.transform.localScale;
			
                GetComponent<NetworkView>().RPC("ScaleChange", RPCMode.Others, myScale);
            }

    }
}
