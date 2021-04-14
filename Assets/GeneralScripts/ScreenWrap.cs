using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Tem forma melhor de categorizar e construir os scripts existentes aqui, mas pela restrição de tempo, fazendo de qualquer forma mesmo

public class ScreenWrap : MonoBehaviour {

	Camera cam;
	Bounds cameraBounds;
	Bounds objectBounds;
	bool isWraping = false;
	bool isSpawning = false;
	public float spawnOffset = 0.5f;
	public float offset = 0.1f;
	public float verticalOffset = 0.4f;
	public Vector3 targetPosition;

	float top, down, left, right;

	public GameObject[] platforms;
	public GameObject cookie;

	void Start(){
		cam = Camera.main;
	}

	void LateUpdate(){
		var lowerLeft = cam.ScreenToWorldPoint (Vector3.zero);
		var uperRight = cam.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0));
		var midde = cam.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height/2, 0));
		top = uperRight.y; down = lowerLeft.y; left = lowerLeft.x; right = uperRight.x;

		Vector3 newPosition = transform.position;

		if (transform.position.x + offset < lowerLeft.x)
			newPosition.x = uperRight.x + offset;
		else if (transform.position.x - offset > uperRight.x)
			newPosition.x = lowerLeft.x - offset;

		transform.position = newPosition;

		Vector3 camPosition = cam.transform.position;

		if ((transform.position.y > camPosition.y + verticalOffset) && (!isWraping || transform.position.y > targetPosition.y)) {
			camPosition.y = transform.position.y;
			targetPosition = camPosition;
			if (!isSpawning) {
				SpawnPlatform ();
			}
			isWraping = true;
		}

		if (isWraping) {
			cam.transform.position = Vector3.Lerp (cam.transform.position, targetPosition, Time.deltaTime);
			isWraping = cam.transform.position != targetPosition;
		}

		if (transform.position.y - offset < lowerLeft.y) {
			PointsManager.LoseLife ();
		}
	}

	void SpawnPlatform(){
		isSpawning = true;
		Instantiate (platforms [Random.Range (0, platforms.Length)], new Vector3 (Random.Range(left, right), top, 0), Quaternion.identity);
		if(Random.Range(0,101) <= 30){
			Instantiate(cookie, new Vector3 (Random.Range(left, right), top, 0), Quaternion.identity);
		}
		Invoke ("StopSpawn", spawnOffset);
	}

	void StopSpawn(){
		isSpawning = false;
	}

}


