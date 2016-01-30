using UnityEngine;
using System.Collections.Generic;

public class Killer : MonoBehaviour {

	private Rigidbody2D _rb;

    private string TargetWeapon;
    private string TargetSignature;
    private string TargetVictim;
    private List<PickUp> PickUps;

	public float MoveSpeed;

	void Awake(){
		_rb = this.GetComponent<Rigidbody2D> ();
	}

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Attack()
    {

    }

	public void Move(Vector2 MoveDirection)
    {
		_rb.velocity = new Vector3(MoveDirection.x * MoveSpeed, MoveDirection.y * MoveSpeed);
    }

	public void Grab()
    {

    }

	public void Drop()
    {

    }

}
