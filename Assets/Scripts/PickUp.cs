using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {

	public enum PickUpType
	{
		Weapon,
		Signature,
		Victim
	};

	public PickUpType Type;
	public string Name;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Hover()
    {

    }
}
