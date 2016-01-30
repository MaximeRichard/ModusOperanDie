using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerControl : MonoBehaviour {

	public enum DropDirection{
		Left=1,
		Right
	}
		
	private Killer _killer;

	private bool GamepadAvailable;
	public int PlayerNumber;

	private Vector2 Move = new Vector2(0, 0);

	void Awake(){
		_killer = this.GetComponent<Killer> ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GamepadAvailable = GamepadInput.Instance.gamepads.Count >= PlayerNumber ? true : false;

		if(GamepadAvailable){
			float h = GamepadInput.Instance.gamepads [PlayerNumber - 1].GetAxis (GamepadAxis.LeftStickX);
			float v = GamepadInput.Instance.gamepads [PlayerNumber - 1].GetAxis (GamepadAxis.LeftStickY);
			Move = (v * Vector2.up + h * Vector2.right).normalized;

			_killer.Grabbing = GamepadInput.Instance.gamepads [PlayerNumber - 1].GetButtonDown (GamepadButton.Action3);
			_killer.Killing = GamepadInput.Instance.gamepads [PlayerNumber - 1].GetButtonDown (GamepadButton.Action1);

			DropDirection dropDirection = (DropDirection) 0;

			if(GamepadInput.Instance.gamepads [PlayerNumber - 1].GetButtonDown (GamepadButton.Action4))
				print(_killer.CanKill ());

			if ((GamepadInput.Instance.gamepads [PlayerNumber - 1].GetAxis (GamepadAxis.LeftTrigger) > 0.5) == true) {
				dropDirection = DropDirection.Left;
				_killer.Drop (dropDirection);
			}
			else if((GamepadInput.Instance.gamepads[PlayerNumber - 1].GetAxis(GamepadAxis.RightTrigger) > 0.5) == true)
				dropDirection = DropDirection.Right;
				_killer.Drop (dropDirection);
			
		}
	}
		
	void FixedUpdate (){
		ClampPosition ();
		_killer.Move (Move);
	}

	void ClampPosition (){
		Vector2 pos = transform.position;
		pos.x = Mathf.Clamp (transform.position.x, -8.42f, 8.55f);
		pos.y = Mathf.Clamp (transform.position.y, -4.39f, 4.51f);
		transform.position = pos;
	}
}
