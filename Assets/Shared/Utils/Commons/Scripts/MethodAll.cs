using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

public class MethodAll : MonoBehaviour
{
     
    protected Canvas CreateCanvasIfNone()
    {

        GameObject go = GameObject.Find("CanvasToAdd");
        Canvas canvas;

        if (go == null)
        {
            GameObject cv = new GameObject("CanvasToAdd");
            cv.AddComponent<RectTransform>();
            canvas = cv.AddComponent<Canvas>();
            canvas.pixelPerfect = true;
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            cv.layer = LayerMask.NameToLayer("UI");
            cv.AddComponent<CanvasScaler>();
            cv.AddComponent<GraphicRaycaster>();
        } else
        {
            canvas = go.GetComponent<Canvas>();
        }

        return canvas;
    }

    public GameObject CreateNewPanel(Sprite PanelSp)
    {
        
        Canvas canvas = CreateCanvasIfNone();
        GameObject panelObj = new GameObject("Create Panel");
        panelObj.layer = canvas.gameObject.layer;
        panelObj.transform.SetParent(canvas.transform);
        RectTransform panelRt = panelObj.AddComponent<RectTransform>();
        panelObj.AddComponent<CanvasRenderer>();
        
        Image img = panelObj.AddComponent<Image>();
        
        img.rectTransform.sizeDelta = new Vector2(Screen.width * 0.7f, Screen.height * 0.65f);
        img.rectTransform.anchoredPosition = Vector3.zero;
        img.sprite = PanelSp;
        panelObj.SetActive(false);
        
        return panelObj;
        
    }

    //ボタン生成関数(string ボタン名,string テキスト内容,vevtor2 サイズ,vector3 ポジション,Color カラー)；返り値 Button
    //-----------------------------------------------------------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------------------------------------------------------


    public Button ButtonCreate(string ButtonTex, string Tex, Vector2 siz, Vector3 posi, Color col, Sprite Sp)
    {

        //ButtonTは生成したボタンの名前。
        Canvas canvas = CreateCanvasIfNone();

        //---------------------------------------------------------------------------------
        //ボタンの生成

        GameObject go = new GameObject(ButtonTex);
        go.layer = canvas.gameObject.layer;
        go.transform.SetParent(canvas.transform);
        go.AddComponent<RectTransform>();
        Button button = go.AddComponent<Button>();

        //---------------------------------------------------------------------------------

        Image image = go.AddComponent<Image>();
        image.rectTransform.sizeDelta = siz;
        image.rectTransform.anchoredPosition = Vector3.zero;
        image.color = col;
        image.sprite = Sp;
        //---------------------------------------------------------------------------------
        
//      // ボタンを押したときの処理
//      button.onClick.AddListener (() => {
//          Destroy(button.gameObject);
//      });

        //---------------------------------------------------------------------------------
        //テキストの生成、中身の編集
        GameObject gt = new GameObject("TextT");
        gt.transform.SetParent(button.transform);
        gt.layer = canvas.gameObject.layer;
        gt.AddComponent<RectTransform>();
        Text text = gt.AddComponent<Text>();
        text.text = Tex;
        text.rectTransform.pivot = Vector2.one * 0.5f;
        text.rectTransform.anchorMin = Vector3.zero;
        text.rectTransform.anchorMax = Vector3.one;

        text.rectTransform.anchoredPosition = Vector3.zero;
        text.rectTransform.sizeDelta = Vector3.zero;

        //text.rectTransform.sizeDelta = new Vector2 (siz.x,siz.y*0.8f);;
        //text.rectTransform.anchoredPosition = new Vector3(0,-siz.y/5,0);
        text.font = Resources.FindObjectsOfTypeAll<Font>() [0];

        text.color = new Color(0, 0, 0, 1);

        button.targetGraphic = image;

        text.fontSize = 20;

        text.alignment = TextAnchor.MiddleCenter;

        button.gameObject.transform.position = posi;
        text.resizeTextForBestFit = true;
        text.resizeTextMaxSize = 40;

        return button;
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------------------------------------------------------






    //レイのHitとBool
    //-----------------------------------------------------------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------------------------------------------------------


    public RaycastHit RayHit(Camera c)
    {
        bool b;
        return RayHit(out b, c);
    }

    public RaycastHit RayHit(out bool b, Camera c, float maxDistance = 1000f)
    {
        Ray ray = c.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        b = Physics.Raycast(ray, out hit, maxDistance);

        return hit;
    }

	public RaycastHit RayHit(out bool b, Camera c, LayerMask mask, GameObject[] ignore,  float maxDistance = 1000f)
	{
//		print (mask);
		if (ignore == null) {
			return RayHit(out b, c, maxDistance);
		}

		Ray ray = c.ScreenPointToRay(Input.mousePosition);
		RaycastHit[] hits = Physics.RaycastAll(ray, maxDistance).OrderBy(h=>h.distance).ToArray();

		foreach (RaycastHit hit in hits) {
			if(!ignore.Contains(hit.transform.gameObject)){
				b = true;
				return hit;
			}
		}

		RaycastHit hh = new RaycastHit();
		b = false;

		return hh;
	}

    [RPC]
    public void SetCollider(GameObject go, bool onOff)
    {
		//print (go);
        Collider[] cs = go.GetComponentsInChildren<Collider>();
		//print (""+cs.Length+","+onOff);

        foreach (Collider c in cs)
        {
            c.enabled = onOff;
        }

        if (Network.isServer || Network.isClient)
        {
            NetworkView nv = go.GetComponent<NetworkView>();
            if (nv)
            {
                nv.RPC("SetCollider", RPCMode.Others, onOff);
            }
        }

    }
}
