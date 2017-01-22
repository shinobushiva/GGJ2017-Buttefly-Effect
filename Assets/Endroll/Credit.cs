using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Credit : MonoBehaviour
{

    public string[] texts;
    public Text textLabel;
    public float fadeTime;
    public float duration;

    public string titleScene;
	public Button returnToTitle;

	void Start(){
		textLabel.color = textLabel.color = new Color(1, 1, 1, 0);
		returnToTitle.gameObject.SetActive (false);
	}

	public void ReturnToTitle(){
		SceneManager.LoadSceneAsync (titleScene);
	}

	public void PlayCredit(){
		StartCoroutine(CreditRoutine());
	}

    IEnumerator CreditRoutine()
    {
        int num = 0;

		returnToTitle.gameObject.SetActive (true);

        while (num < texts.Length)
        {

            string[] s = texts[num].Split(',');

			textLabel.text = "<size=24>"+s[0]+"</size>";
            if (s.Length == 2) textLabel.text += '\n' + s[1];

            float targetTime = Time.time + fadeTime;
            while (Time.time < targetTime)
            {
                textLabel.color = new Color(1, 1, 1, 1 - (targetTime - Time.time) / fadeTime);
                yield return null;
            }

            yield return new WaitForSeconds(duration);

			if (num + 1 == texts.Length)
				break;

            targetTime = Time.time + fadeTime;
            while (Time.time < targetTime)
            {
                textLabel.color = new Color(1, 1, 1, (targetTime - Time.time) / fadeTime);
                yield return null;
            }

            num++;

        }

    }



}
