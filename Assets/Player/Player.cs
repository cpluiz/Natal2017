using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour {

	Rigidbody2D body;
	Animator playerAnimator;

	public float moveSpeed = 7f;
	[Header("Jump Configuration")]
	public float jumpSpeed;
	[Header("GroundCheck")]
	public LayerMask groundMask;
	public Transform groundCheck;
	[Range(0.05f, 0.3f)]
	public float checkRadius = 0.2f;
	bool isGrounded;
	[Header("GUI")]
	public Text textPoints;
	public Text lifePoints;

	void Awake(){
		body = GetComponent<Rigidbody2D> ();
		playerAnimator = GetComponentInChildren<Animator> ();
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}

	void Update(){
		isGrounded = Physics2D.OverlapCircle (groundCheck.position, checkRadius, groundMask);
		playerAnimator.SetFloat ("Vspeed", body.velocity.y);
		transform.Translate(Vector2.right * Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime);
		transform.Translate (Vector2.right * Input.acceleration.x * moveSpeed * Time.deltaTime);
		textPoints.text = PointsManager.points.ToString();
		lifePoints.text = PointsManager.lifes.ToString ();
	}

	void FixedUpdate(){
		if (isGrounded && body.velocity.y <= 0) {
			Vector2 velocity = new Vector2 (0, jumpSpeed);
			body.velocity = velocity;
		}
	}

}
