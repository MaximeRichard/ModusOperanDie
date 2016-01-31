using UnityEngine;
using System.Collections.Generic;

public class Victim : PickUp {

	private Rigidbody2D _rb;

	public float objectSpeed;
    private float xActive, yActive;

	void Awake(){
		_rb = GetComponent<Rigidbody2D>();
	}

    // Use this for initialization
    void Start () {
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
		_rb.velocity = new Vector3(xActive, yActive, 0.0f) * objectSpeed;
		_rb.position = new Vector3(
			Mathf.Clamp(_rb.position.x, -3.5f, 3.5f),
			Mathf.Clamp(_rb.position.y, -1.49f, 2.02f),
			0
		);
    }
}
