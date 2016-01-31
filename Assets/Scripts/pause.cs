using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class pause : MonoBehaviour {
	public Button backText;
	public Toggle soundText;
	public Button exitText;

    private bool display = false;

	void Start () {
		backText = backText.GetComponent<Button>();
		soundText = soundText.GetComponent<Toggle>();
		exitText = exitText.GetComponent<Button>();
        GetComponent<CanvasRenderer>().SetAlpha(0.0f);
    }

	public void Quit (){
		Application.Quit ();
	}

	// Update is called once per frame
	void Update () {
        Debug.Log("Test");
        if (GamepadInput.Instance.gamepads[0].GetButton(GamepadButton.Start))
        {
            Time.timeScale = 0;
            display = !display;
            this.gameObject.SetActive(display);
        }
    }
}