using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class Killer : MonoBehaviour {

	private Rigidbody2D _rb;
	private Animator _animator;
	public string PlayerName;

	public struct PickUpData {
		public PickUp.PickUpType Type;
		public string Name;
	}

    public string TargetWeapon;
    public string TargetSignature;
    public string TargetVictim;

	public PickUpData InventorySlot1;
	public PickUpData InventorySlot2;

	public float MoveSpeed;
	public int OldDirection = 1;
	public Vector2 CurrentDirection;

	public bool FacingRight = true;
	public bool Grabbing = false;
	public bool Killing = false;
	public bool HasWon = false;

	public bool Stunned = false;
	public float StunTime;
	private float CurrentStunTime = 0;

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

		if (Stunned && StunTime > CurrentStunTime)
			CurrentStunTime += Time.deltaTime;
		else if (Stunned && StunTime <= CurrentStunTime) {
			CurrentStunTime = 0;
			Stunned = false;
			_animator.SetBool ("stun", false);
		}
	}

	public IEnumerator Attack()
    {
		_animator.SetBool ("attacking", true);
		yield return new WaitForSeconds (0.5f);
		_animator.SetBool ("attacking", false);
    }

	public void Move(Vector2 MoveDirection)
    {
		CurrentDirection = MoveDirection;
		_rb.velocity = new Vector2(MoveDirection.x * MoveSpeed, MoveDirection.y * MoveSpeed);
    }

	public void Grab(GameObject go)
    {
		PickUpData pu = CopyPickUpData(go.GetComponent<PickUp>());

		if (InventorySlot1.Name == null || InventorySlot2.Name == null) {

			if (InventorySlot1.Name != pu.Name && InventorySlot2.Name != pu.Name) {	
				if (!(pu.Type == PickUp.PickUpType.Victim) && InventorySlot1.Name == null) {
					InventorySlot1 = pu;
                    GameController.RefreshInventory(this.gameObject, pu);
					Destroy (go);
				} else if (!(pu.Type == PickUp.PickUpType.Victim) && InventorySlot2.Name == null) {
					InventorySlot2 = pu;
                    GameController.RefreshInventory(this.gameObject, pu);
                    Destroy (go);
				}
			}
		}
        
        if (pu.Type == PickUp.PickUpType.Victim && pu.Name == TargetVictim && CanKill ()) {
            GameController.SetWinningPlayer(PlayerName);
            GameController.SetGameState (GameController.GameState.End);
            Destroy (go);
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
				RemoveAndInstantiateInventoryItem (0, -1, InventorySlot1);
                Debug.Log(InventorySlot1.Name);
                GameController.RefreshInventory(this.gameObject, InventorySlot1);
                break;
		case PlayerControl.DropDirection.Right:
			if (InventorySlot2.Name != null)
				RemoveAndInstantiateInventoryItem (1, 1, InventorySlot2);
                GameController.RefreshInventory(this.gameObject, InventorySlot2);
                break;
		}
    }

	public void RemoveAndInstantiateInventoryItem(int index, int direction, PickUpData pud){
		GameObject PickUp = (GameObject) Instantiate(Resources.Load<GameObject>("Prefabs/PickUps/"+pud.Name), new Vector2(transform.position.x+DropDistance*direction, transform.position.y), Quaternion.identity);
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
				Attack ();
				Grab (other.transform.parent.gameObject);
			}
		}

		if (other.tag == "Player") {
			Killer k = other.transform.parent.gameObject.GetComponent<Killer> ();
			if (k.Killing) {
				Stunned = true;
				if (InventorySlot2.Name != null) {
					Drop (PlayerControl.DropDirection.Right);
				} else if (InventorySlot1.Name != null) {
					Drop (PlayerControl.DropDirection.Left);
				}
				_animator.SetBool ("stun", true);
			}
		}

		if (other.tag == "Awareness") {
			Stunned = true;
			if (InventorySlot2.Name != null) {
				Drop (PlayerControl.DropDirection.Right);
			} else if (InventorySlot1.Name != null) {
				Drop (PlayerControl.DropDirection.Left);
			}
			_animator.SetBool ("stun", true);
		}
	}

	PickUpData CopyPickUpData(PickUp pickup){
		PickUpData pu;
		pu.Type = pickup.Type;
		pu.Name = pickup.Name;
		return pu;
	}

    public class HUD
    {
        public Image i1;
    }
}
