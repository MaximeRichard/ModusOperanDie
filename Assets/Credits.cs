using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Credits : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (GamepadInput.Instance.gamepads[0].GetButton(GamepadButton.Action2))
        {
            Application.LoadLevel("splash");
        }
    }
}
