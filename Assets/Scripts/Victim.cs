using UnityEngine;
using System.Collections.Generic;

public class Victim : MonoBehaviour {

    public float objectSpeed;
    private Rigidbody2D rb;
    private float xActive, yActive;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        xActive = 1.0f;
        yActive = -1.0f;
        InvokeRepeating("SwitchDirections", 0.5f, 1.0f);
    }
    void SwitchDirections()
    {
        xActive = Random.Range(-1.0f, 1.0f);
        yActive = Random.Range(-1.0f, 1.0f);
    }

    void FixedUpdate()
    {
		MoveAround ();
    }

    void MoveAround()
    {
		rb.velocity = new Vector3(xActive, yActive, 0.0f) * objectSpeed;
		rb.position = new Vector3(
			Mathf.Clamp(rb.position.x, -3.5f, 3.5f),
			Mathf.Clamp(rb.position.y, -3.0f, 4.0f),
			0
		);
    }
}
