using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class splash : MonoBehaviour {

	// Use this for initialization
	void Start () {
    }
	
	void Update() {

        if (GamepadInput.Instance.gamepads[0].GetButton(GamepadButton.Action4))
        {
            Application.LoadLevel("Credits");
        }

        if (GamepadInput.Instance.gamepads[0].GetButton(GamepadButton.Start) ) {
            Application.LoadLevel ("arena");
		}
	}
}
