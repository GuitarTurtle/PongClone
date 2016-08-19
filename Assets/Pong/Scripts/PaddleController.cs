using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour {

	public string axis = "Vertical";
	public float speed = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float y = Input.GetAxis(axis);
		Vector3 p = transform.position;
		p.y += y * speed * Time.deltaTime;

		Vector3 view = Camera.main.WorldToViewportPoint(p);
		view.y = Mathf.Clamp(view.y, -0.05f, 1.05f);
		transform.position = Camera.main.ViewportToWorldPoint(view);
	}
}
