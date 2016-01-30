using UnityEngine;
using System.Collections.Generic;

public class Killer : MonoBehaviour {

	private Rigidbody2D _rb;
	private Animator _animator;

    private string TargetWeapon;
    private string TargetSignature;
    private string TargetVictim;
    private List<PickUp> PickUps;

	public float MoveSpeed;
	public int OldDirection = 1;
	public Vector2 CurrentDirection;
	public bool FacingRight = true;
	public bool Grabbing = false;
	public bool Stunned = false;

	void Awake(){
		_rb = this.GetComponent<Rigidbody2D> ();
		_animator = this.GetComponent<Animator> ();
	}

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Animate ();
		print ("Moving : "+ (_rb.velocity != Vector2.zero ? true : false));
		print ("direction : " + (CurrentDirection.x >= 0 ? 1 : 0));
	}

	public void Attack()
    {

    }

	public void Move(Vector2 MoveDirection)
    {
		CurrentDirection = MoveDirection;
		_rb.velocity = new Vector2(MoveDirection.x * MoveSpeed, MoveDirection.y * MoveSpeed);
    }

	public void Grab(PickUp pickup)
    {
		//ajouter pickup dans PickUps
    }

	public void Drop()
    {

    }

	private void Animate(){
		//moving
		_animator.SetBool("moving", CurrentDirection != Vector2.zero ? true : false);
		//direction
		if (_animator.GetBool ("moving")) {
			_animator.SetInteger("direction", CurrentDirection.x >= 0 ? 1 : 0);
		}
		//stun
		_animator.SetBool("stun", this.Stunned);

		//flip the character if direction has changed
		if (OldDirection != _animator.GetInteger ("direction")) {
			Flip ();
			OldDirection = _animator.GetInteger ("direction");
		}
	}

	void Flip()
	{
		// Switch the way the player is labelled as facing
		FacingRight = !FacingRight;

		// Multiply the player's x local scale by -1
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "PickUp") {
			if (Grabbing) {
				//Grab((PickUp) other)
			}
		}
	}
}
