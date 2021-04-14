using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

	public Text textHighScore, textLastScore;

	void Awake(){
		textHighScore.text = PlayerPrefs.GetInt ("HighScore").ToString();
		textLastScore.text = PlayerPrefs.GetInt ("LastScore").ToString();
	}

	public void Play(){
		SceneManager.LoadScene (1);
		Debug.Log ("Teste");
	}

	public void Exit(){
		Application.Quit ();
	}
}
