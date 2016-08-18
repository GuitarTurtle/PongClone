using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour {

	public Text scoreText;
	public float initialSpeed = 4;
	public float speedIncrease = 0.1f;
	public float maxSpeed = 20;

	private int leftScore;
	private int rightScore;
	private Rigidbody body;
	private float speed;
	private bool gameOver = false;
	void Start () {
		speed = initialSpeed;
		body = GetComponent<Rigidbody> ();
		body.velocity = new Vector3(1, 0.1f,0) * speed;
		SetScoreUI();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameOver) {
			gameObject.SetActive(false);
			return;
		}

		if (Mathf.Abs(body.velocity.y) > Mathf.Abs(body.velocity.x)) {
			body.velocity = new Vector3(Mathf.Sign(body.velocity.x), Mathf.Sign(body.velocity.y),0);
		}
		body.velocity = body.velocity.normalized * speed;

		Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
		if (viewportPos.x < 0) {
			speed = initialSpeed;
			rightScore ++;
			transform.position = Vector3.zero;
			body.velocity = new Vector3(1, 0.1f,0) * speed;
			SetScoreUI();
		}
		else if (viewportPos.x > 1) {
			speed = initialSpeed;
			leftScore ++;
			transform.position = Vector3.zero;
			body.velocity = new Vector3(-1, 0.1f,0) * speed;
			SetScoreUI();
		}
		CheckGameOver();
	}

	void OnCollisionEnter(Collision collision) {
		speed += speedIncrease;
		if (speed > maxSpeed) {
			speed = maxSpeed;
		}
	}

	void CheckGameOver () {
		if (leftScore == 7) {
			scoreText.text = "Left Wins";
			gameOver = true;
		}
		else if (rightScore == 7) {
			scoreText.text = "Right Wins";
			gameOver = true;
		}

	}

	void SetScoreUI () {
		scoreText.text = "" + leftScore + " - " + rightScore;
	}
}
