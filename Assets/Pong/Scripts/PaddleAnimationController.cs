using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleAnimationController : MonoBehaviour {

    public float duration = 0.3f;
    public float maxSize = 0.3f;

    private Vector3 initialScale;

    void Start()
    {
        initialScale = transform.localScale;
    }

	void OnCollisionEnter(Collision c)
    {
        StartCoroutine("Expand");
    }

    IEnumerator Expand()
    {
        for(float t = 0; t < duration; t += Time.deltaTime)
        {
            float frac = t / duration;
            float size = Mathf.Sin(Mathf.PI * frac);
            transform.localScale = initialScale + Vector3.one * size * maxSize;
            yield return new WaitForEndOfFrame();
        }
    }
}
