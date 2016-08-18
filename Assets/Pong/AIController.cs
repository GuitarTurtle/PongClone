using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {

	public BallController ball;
	public float speed = 10;

	void Update () {
		float targetVelocity = 0;
		Vector3 pos = transform.position;
		if (pos.y > ball.transform.position.y + 0.5f) {
			targetVelocity = -speed;
		}
		else if (pos.y < ball.transform.position.y - 0.5f) {
			targetVelocity = speed;
		}
		pos.y += targetVelocity * Time.deltaTime;
		transform.position = pos;
	}
}
