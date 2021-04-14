using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PointsManager : MonoBehaviour {

	public static int points;
	public static int lifes = 3;

	public static void LoseLife(){
		lifes--;
		if (lifes <= 0) {
			PlayerPrefs.SetInt ("LastScore", points);
			points = 0;
			lifes = 3;
			SceneManager.LoadScene (0);
		} else {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		}
	}

	public static void GetCookie(){
		points++;
		if (points > PlayerPrefs.GetInt ("HighScore")) {
			PlayerPrefs.SetInt ("HighScore", points);
		}
		if (points % 10 == 0) {
			lifes++;
		}
	}
}
