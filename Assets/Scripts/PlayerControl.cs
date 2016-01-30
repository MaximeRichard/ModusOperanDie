using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerControl : MonoBehaviour {

	private Killer _killer;

	private bool GamepadAvailable;
	private Vector2 Move = new Vector2(0, 0);

	void Awake(){
		_killer = this.GetComponent<Killer> ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Z))
			Move = new Vector2 (0, 1);
		else if (Input.GetKey (KeyCode.Q))
			Move = new Vector2 (-1, 0);
		else if (Input.GetKey (KeyCode.S))
			Move = new Vector2 (0, -1);
		else if(Input.GetKey (KeyCode.D))
			Move = new Vector2 (1, 0);
		else
			Move = new Vector2 (0, 0);
	}

	void FixedUpdate (){
		_killer.Move (Move);
	}
}
