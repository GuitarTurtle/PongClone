using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Paraphernalia.Components;

public class BallController : MonoBehaviour {

	public int winScore = 7;
	public Text scoreText;
    public Text winText;
	public float initialSpeed = 4;
	public float speedIncrease = 0.1f;
	public float maxSpeed = 20;
	public float maxStretch = 0.3f;
    public float maxTrailSize = 3;
    public TrailRenderer trail;
    public Color leftColor = Color.blue;
    public Color rightColor = Color.red;
    public Color centerColor = Color.yellow;

	private int leftScore;
	private int rightScore;
	private Rigidbody body;
	private float speed;
	private bool gameOver = false;
    private float initialScale;

	void Start () {
		UpdateColor();
		winText.gameObject.SetActive(false );
        initialScale = transform.localScale.x;
		speed = initialSpeed;
		body = GetComponent<Rigidbody> ();
		body.velocity = new Vector3(1, 0.1f,0) * speed;
		SetScoreUI();
	}
	
	void Update () {
		if (gameOver) {
			return;
		}

		if (Mathf.Abs(body.velocity.y) > Mathf.Abs(body.velocity.x)) {
			body.velocity = new Vector3(Mathf.Sign(body.velocity.x), Mathf.Sign(body.velocity.y),0);
		}
		body.velocity = body.velocity.normalized * speed;
        UpdateSquashAndStretch();

		Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
		if (viewportPos.x < 0) {
			speed = initialSpeed;
			rightScore ++;
            if (rightScore < winScore) AudioManager.PlayEffect("ScoreRight", "Scoring");
			transform.position = Vector3.zero;
			body.velocity = new Vector3(1, 0.1f, 0) * speed;
			SetScoreUI();
		}
		else if (viewportPos.x > 1) {
			speed = initialSpeed;
			leftScore ++;
            if (leftScore < winScore) AudioManager.PlayEffect("ScoreLeft", "Scoring");
			transform.position = Vector3.zero;
			body.velocity = new Vector3(-1, 0.1f, 0) * speed;
			SetScoreUI();
		}
		CheckGameOver();
	}

    void UpdateSquashAndStretch() {
        float ang = Mathf.Atan2(body.velocity.y, body.velocity.x) * Mathf.Rad2Deg;
        float frac = (body.velocity.magnitude - initialSpeed) / (maxSpeed - initialSpeed);
        float amount = maxStretch * frac;
        trail.time = maxTrailSize * frac;
        transform.localScale = new Vector3(1 + amount, 1 - amount, 1) * initialScale;
        transform.localEulerAngles = new Vector3(0, 0, ang);
    }

    void UpdateColor () {
    	Color c = centerColor;
    	if (leftScore > rightScore) c = Color.Lerp(centerColor, leftColor, (float)(leftScore - rightScore) / (float)winScore);
    	else if (rightScore > leftScore) c = Color.Lerp(centerColor, rightColor, (float)(rightScore - leftScore) / (float)winScore);
    	Camera.main.backgroundColor = c;
    }

	void OnCollisionEnter(Collision collision) {
		speed += speedIncrease;
		if (speed > maxSpeed) {
			speed = maxSpeed;
		}

        if (collision.gameObject.tag == "Paddle"){
            AudioManager.PlayEffect("PaddleBounce", null, transform, Random.Range(0.9f, 1.1f), Random.Range(0.9f, 1.1f), 0, 1);
        }

        if (collision.gameObject.tag == "Wall"){
            AudioManager.PlayEffect("WallBounce", null, transform, Random.Range(0.9f, 1.1f), Random.Range(0.9f, 1.1f), 0, 1);
        }
	}

	void CheckGameOver () {
		UpdateColor();
		if (leftScore == winScore) {
			winText.text = "Left Wins";
			gameOver = true;
		}
		else if (rightScore == winScore) {
			winText.text = "Right Wins";
			gameOver = true;
		}

		if (gameOver) {
			winText.gameObject.SetActive(true);
            AudioManager.PlayEffect("Win", "Winning");
			body.velocity = Vector2.zero;
			StartCoroutine("ExitToMenuCoroutine");
		}
	}

	IEnumerator ExitToMenuCoroutine () {
		yield return new WaitForSeconds (5);
		SceneManager.LoadScene("Menu");
	}

	void SetScoreUI () {
		scoreText.text = "" + leftScore + " - " + rightScore;
	}
}
