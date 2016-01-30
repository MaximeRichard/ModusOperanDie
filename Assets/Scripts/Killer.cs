﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Killer : MonoBehaviour {

	private Rigidbody2D _rb;
	private Animator _animator;

	public struct PickUpData {
		public PickUp.PickUpType Type;
		public string Name;
	}

    public string TargetWeapon;
    public string TargetSignature;
    private string TargetVictim;

	public PickUpData InventorySlot1;
	public PickUpData InventorySlot2;

	public float MoveSpeed;
	public int OldDirection = 1;
	public Vector2 CurrentDirection;

	public bool FacingRight = true;
	public bool Grabbing = false;
	public bool Killing = false;
	public bool Stunned = false;
	public bool HasWon = false;

	public GameObject PickUpPrefab;

	public float DropDistance;

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
	}

	public void Attack()
    {

    }

	public void Move(Vector2 MoveDirection)
    {
		CurrentDirection = MoveDirection;
		_rb.velocity = new Vector2(MoveDirection.x * MoveSpeed, MoveDirection.y * MoveSpeed);
    }

	public void Grab(GameObject go)
    {
		if (InventorySlot1.Name == null || InventorySlot2.Name == null) {
			PickUpData pu = CopyPickUpData(go.GetComponent<PickUp>());
			print ("Type : "+pu.Type);
			if (pu.Type == PickUp.PickUpType.Victim && pu.Name == TargetVictim && CanKill ()) {
				//TODO : à remplacer par GameController.OnWin()
				HasWon = true;
				Destroy (go);
			}

			if (InventorySlot1.Name != pu.Name && InventorySlot2.Name != pu.Name) {	
				if (!(pu.Type == PickUp.PickUpType.Victim) && InventorySlot1.Name == null) {
					InventorySlot1 = pu;
					Destroy (go);
				} else if (!(pu.Type == PickUp.PickUpType.Victim) && InventorySlot2.Name == null) {
					InventorySlot2 = pu;
					Destroy (go);
				}
			}
		}
    }

	public bool CanKill(){
		bool hasTargetWeapon = false;
		bool hasTargetSignature = false;

		hasTargetSignature = ((TargetSignature == InventorySlot1.Name) || (TargetSignature == InventorySlot2.Name));
		hasTargetWeapon = ((TargetWeapon == InventorySlot1.Name) || (TargetWeapon == InventorySlot2.Name));

		return (hasTargetSignature && hasTargetWeapon);
	}

	public void Drop(PlayerControl.DropDirection dir)
    {
		switch (dir) {
		case PlayerControl.DropDirection.Left:
			if (InventorySlot1.Name != null)
				RemoveAndInstantiateInventoryItem (0, -1);
			break;
		case PlayerControl.DropDirection.Right:
			if (InventorySlot2.Name != null)
				RemoveAndInstantiateInventoryItem (1, 1);
			break;
		}
    }

	public void RemoveAndInstantiateInventoryItem(int index, int direction){
		GameObject PickUp = (GameObject) Instantiate(PickUpPrefab, new Vector2(transform.position.x+DropDistance*direction, transform.position.y), Quaternion.identity);
		PickUpData pu;
		pu.Name = null;
		pu.Type = (PickUp.PickUpType) 0;
		switch (index) {
		case 0:
			pu = InventorySlot1;
			InventorySlot1.Name = null;
			break;
		case 1:
			pu = InventorySlot2;
			InventorySlot2.Name = null;
			break;
		}
		PickUp.GetComponent<PickUp> ().Name = pu.Name;
		PickUp.GetComponent<PickUp> ().Type = pu.Type;
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
		FacingRight = !FacingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "PickUp") {
			if (Grabbing) {
				Grab (other.gameObject);
			} 
		}
		if (other.tag == "Civil") {
			if (Killing) {
				Grab (other.transform.parent.gameObject);
			}
		}
	}

	PickUpData CopyPickUpData(PickUp pickup){
		PickUpData pu;
		pu.Type = pickup.Type;
		pu.Name = pickup.Name;
		return pu;
	}
}
